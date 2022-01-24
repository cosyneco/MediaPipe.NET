// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using CommandLine;
using Mediapipe.Net.Calculators;
using Mediapipe.Net.External;
using Mediapipe.Net.Framework.Format;
using Mediapipe.Net.Framework.Protobuf;
using SeeShark;
using SeeShark.FFmpeg;

namespace Mediapipe.Net.Examples.FaceMeshGpu
{
    [SupportedOSPlatform("Linux")]
    public static class Program
    {
        private static Camera? camera;

        private static FrameConverter? converter;

        private static FaceMeshGpuCalculator? calculator;

        public static void Main(string[] args)
        {
            //get and parse arguments
            var parsed = Parser.Default.ParseArguments<Options>(args).Value;

            FFmpegManager.SetupFFmpeg("/usr/lib");

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

            camera.OnFrame += onFrame;


            Glog.Initialize("stuff");
            calculator = new FaceMeshGpuCalculator();

            calculator.OnResult += handleLandmarks;

            calculator.Run();

            camera.StartCapture();

            Console.ReadLine();
        }

        private static void handleLandmarks(object? sender, List<NormalizedLandmarkList> e)
        {
            Console.WriteLine($"Got landmarks at frame {calculator?.CurrentFrame}");
        }

        private static unsafe void onFrame(object? sender, FrameEventArgs e)
        {
            if (calculator == null)
                return;

            var frame = e.Frame;
            if (converter == null)
                converter = new FrameConverter(frame, PixelFormat.Rgba);

            // Don't use a frame if it's not new
            if (e.Status != DecodeStatus.NewFrame)
                return;

            Frame cFrame = converter.Convert(frame);

            ImageFrame imgframe;
            fixed (byte* rawDataPtr = cFrame.RawData)
            {
                imgframe = new ImageFrame(ImageFormat.Srgba,
                    cFrame.Width, cFrame.Height, cFrame.WidthStep, rawDataPtr);
            }
            using ImageFrame img = calculator.Send(imgframe);
            imgframe.Dispose();
        }
    }
}
