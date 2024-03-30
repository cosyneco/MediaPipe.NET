// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Mediapipe.Net.Framework.Format;
using Mediapipe.Net.Framework.Packets;
using Mediapipe.Net.Framework.Protobuf;

namespace Mediapipe.Net.Benchmarks
{
    [SimpleJob(RunStrategy.Throughput, launchCount: 50)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public class ImageFramePacketPerformanceBenchmark
    {
        [Benchmark]
        public void InstantiateAndConsumeImageFramePacket()
        {
            using var packet = new ImageFramePacket(new ImageFrame(ImageFormat.Types.Format.Sbgra, 50, 50));
            using var statusOrImageFrame = packet.Consume();

            statusOrImageFrame.Ok();
        }
    }
}
