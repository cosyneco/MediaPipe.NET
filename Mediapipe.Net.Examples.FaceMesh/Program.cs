// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using CommandLine;
using Mediapipe.Net.Calculators;
using Mediapipe.Net.Examples.FaceMesh;
using Mediapipe.Net.External;
using Mediapipe.Net.Framework;
using Mediapipe.Net.Framework.Format;
using Mediapipe.Net.Framework.Packet;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Framework.Protobuf;
using Mediapipe.Net.Gpu;
using SeeShark;
using SeeShark.FFmpeg;

namespace Mediapipe.Net.Examples.FaceMesh
{
    public static class Program
    {
        private static Camera? camera;

        private static FrameConverter? converter;

        private static FaceMeshCpuCalculator? calculator;

        public static void Main(string[] args)
        {
            //get and parse arguments
            var parsed = Parser.Default.ParseArguments<Options>(args).Value;

            SeeShark.FFmpeg.FFmpegManager.SetupFFmpeg("/usr/lib");

            //get a camera device
            CameraManager manager = new CameraManager();
            try
            {
                camera = manager.GetCamera(parsed.CameraIndex);
                Console.WriteLine($"Using camera \"{camera.Info.Name}\", {camera.Info.Path}");
            }
            catch (Exception)
            {
                Console.Error.WriteLine($"No camera exists at index {parsed.CameraIndex}.");
                return;
            }

            //dispose the camera manager as we won't need it
            manager.Dispose();

            //dispose the camera on program exit
            Console.CancelKeyPress += (sender, eventArgs) => camera.Dispose();

            camera.OnFrame += OnFrame;


            Glog.Initialize("stuff");
            calculator = new FaceMeshCpuCalculator();

            calculator.Run();

            camera.StartCapture();

            Console.ReadLine();
        }

        private unsafe static void OnFrame(object? sender, FrameEventArgs e)
        {
            var frame = e.Frame;
            if (converter == null)
            {
                converter = new FrameConverter(frame, PixelFormat.Rgba);
            }

            //dont use a frame if it's not new

            if (e.Status != DecodeStatus.NewFrame)
                return;

            Frame cFrame = converter.Convert(frame);

            ImageFrame imgframe;
            fixed (byte* rawDataPtr = cFrame.RawData)
            {
                imgframe = new ImageFrame(ImageFormat.Srgba, cFrame.Width, cFrame.Height, cFrame.WidthStep,
                    rawDataPtr);
            }

            ImageFrame img = calculator.Perform(imgframe, out var result);
        }
    }
}
