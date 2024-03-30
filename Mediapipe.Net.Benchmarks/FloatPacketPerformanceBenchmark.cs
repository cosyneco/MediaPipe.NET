// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Mediapipe.Net.Framework.Packets;

namespace Mediapipe.Net.Benchmarks
{
    [SimpleJob(RunStrategy.Throughput, launchCount: 50)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public class FloatPacketPerformanceBenchmark
    {

        [Benchmark]
        public void InstantiateFloatPacket()
        {
            var randomSingle = new Random().NextSingle();
            var packet = new FloatPacket(randomSingle);

            packet.ValidateAsType().Ok();
        }

        [Benchmark]
        public void InstantiateFloatArrayPacket()
        {
            var randomArray = new float[10];
            for (var i = 0; i < randomArray.Length; i++)
            {
                randomArray[i] = new Random().NextSingle();
            }

            var packet = new FloatArrayPacket(randomArray);

            packet.ValidateAsType().Ok();
        }

    }
}
