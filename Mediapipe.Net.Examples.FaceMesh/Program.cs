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
using SeeShark.Device;
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

            (int, int)? videoSize = null;
            if (parsed.Width != null && parsed.Height != null)
                videoSize = ((int)parsed.Width, (int)parsed.Height);
            else if (parsed.Width != null && parsed.Height == null)
                Console.Error.WriteLine("Specifying width requires to specify height");
            else if (parsed.Width == null && parsed.Height != null)
                Console.Error.WriteLine("Specifying height requires to specify width");

            FFmpegManager.SetupFFmpeg("/usr/lib");
            Glog.Initialize("stuff");

            // Get a camera device
            using (CameraManager manager = new CameraManager())
            {
                try
                {
                    camera = manager.GetDevice(parsed.CameraIndex,
                        new VideoInputOptions
                        {
                            InputFormat = parsed.InputFormat,
                            VideoSize = videoSize,
                        });
                    Console.WriteLine($"Using camera {camera.Info}");
                }
                catch (Exception)
                {
                    Console.Error.WriteLine($"No camera exists at index {parsed.CameraIndex}.");
                    return;
                }
            }

            calculator = new FaceMeshCpuCalculator();
            calculator.OnResult += handleLandmarks;
            calculator.Run();

            Console.CancelKeyPress += (sender, eventArgs) => exit();
            while (true)
            {
                var frame = camera.GetFrame();

                converter ??= new FrameConverter(frame, PixelFormat.Rgba);

                Frame cFrame = converter.Convert(frame);

                using ImageFrame imgframe = new ImageFrame(ImageFormat.Srgba,
                    cFrame.Width, cFrame.Height, cFrame.WidthStep, cFrame.RawData);

                using ImageFrame img = calculator.Send(imgframe);
            }
        }

        private static void handleLandmarks(object? sender, List<NormalizedLandmarkList> landmarks)
        {
            Console.WriteLine($"Got a list of {landmarks[0].Landmark.Count} landmarks at frame {calculator?.CurrentFrame}");
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
