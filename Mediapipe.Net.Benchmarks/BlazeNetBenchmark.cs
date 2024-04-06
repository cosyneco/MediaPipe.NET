// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Mediapipe.Net.Framework;
using Mediapipe.Net.Framework.Packets;
using Mediapipe.Net.Framework.Protobuf;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using ImageFrame = Mediapipe.Net.Framework.Format.ImageFrame;

namespace Mediapipe.Net.Benchmarks
{
    [SimpleJob(RunStrategy.Throughput, launchCount: 20, warmupCount: 10)]
    [MinColumn, MaxColumn, MeanColumn,MedianColumn]
    public class BlazeNetBenchmark
    {
        private readonly ImageFrame referenceFrame;


        public BlazeNetBenchmark()
        {
            var rawImage = Image.Load<Rgba32>("TestData/reference.png");

            // convert to span first before converting to ImageFrame
            var rawImageSpan = new Span<byte>();
            rawImage.CopyPixelDataTo(rawImageSpan);

            // widthStep is a thing from opencv, so we'll need to calculate it ourselves
            // this is calculated as width * bits per pixel, so rgba32 would be 4 bytes per pixel
            var widthStep = rawImage.Width * rawImage.PixelType.BitsPerPixel;

            referenceFrame = new ImageFrame(ImageFormat.Types.Format.Sbgra, rawImage.Width, rawImage.Height, widthStep, rawImageSpan);
        }

        [Benchmark]
        public void BlazeFaceBenchmark()
        {
            // read pbtxt file to a string
            var graphCfg = File.ReadAllText("TestData/face_detection_front_cpu.pbtxt");
            var graph = new CalculatorGraph(graphCfg);
            var poller = graph.AddOutputStreamPoller<ImageFrame>("output_video").Value();

            graph.ObserveOutputStream<NormalizedLandmarkListPacket, NormalizedLandmarkList>("multi_face_landmarks",
                (packet) =>
                {
                    var landmarks = packet.Get();

                    // report the landmarks
                    foreach (var landmark in landmarks.Landmark)
                    {
                        Console.WriteLine($"Landmark: {landmark.X}, {landmark.Y}, {landmark.Z}");
                        // do we have accuracy data too?
                        if (landmark.HasVisibility)
                        {
                            Console.WriteLine($"Visibility: {landmark.Visibility}");
                        }
                    }
                }, out var cbHandle).AssertOk();

            // start the graph
            graph.StartRun().AssertOk();

            // send the image to the graph
            var inputFrame = new ImageFramePacket(referenceFrame);
            graph.AddPacketToInputStream("input_video", inputFrame).AssertOk();

            // wait for the graph to finish
            graph.WaitUntilIdle().AssertOk();

            // close the graph and clean up
            graph.CloseInputStream("input_video").AssertOk();
            graph.WaitUntilDone().AssertOk();
            cbHandle.Free();
        }
    }
}
