// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Collections.Generic;
using CommandLine;
using Mediapipe.Net.Calculators;
using Mediapipe.Net.External;
using Mediapipe.Net.Framework.Format;
using Mediapipe.Net.Framework.Protobuf;
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
            // Get and parse command line arguments
            Options parsed = Parser.Default.ParseArguments<Options>(args).Value;
            FFmpegManager.SetupFFmpeg("/usr/lib");
            Glog.Initialize("stuff");

            // Get a camera device
            using (CameraManager manager = new CameraManager())
            {
                try
                {
                    camera = manager.GetCamera(parsed.CameraIndex);
                    Console.WriteLine($"Using camera {camera.Info}");
                }
                catch (Exception)
                {
                    Console.Error.WriteLine($"No camera exists at index {parsed.CameraIndex}.");
                    return;
                }
            }
            camera.OnFrame += onFrame;

            calculator = new FaceMeshCpuCalculator();
            calculator.OnResult += handleLandmarks;
            calculator.Run();
            camera.StartCapture();

            Console.CancelKeyPress += (sender, eventArgs) => exit();
            Console.ReadLine();
        }

        private static void handleLandmarks(object? sender, List<NormalizedLandmarkList> landmarks)
        {
            Console.WriteLine($"Got a list of {landmarks[0].Landmark.Count} landmarks at frame {calculator?.CurrentFrame}");
        }

        private static unsafe void onFrame(object? sender, FrameEventArgs e)
        {
            if (calculator == null)
                return;

            var frame = e.Frame;
            converter ??= new FrameConverter(frame, PixelFormat.Rgba);

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

        // Dispose everything on exit
        private static void exit()
        {
            Console.WriteLine("Exiting...");
            camera?.Dispose();
            converter?.Dispose();
            calculator?.Dispose();
        }
    }
}
