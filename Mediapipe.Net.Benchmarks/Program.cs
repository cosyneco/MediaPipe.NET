// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using BenchmarkDotNet.Running;

namespace Mediapipe.Net.Benchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var switcher = new BenchmarkSwitcher(new[]
            {
                typeof(FloatPacketPerformanceBenchmark), typeof(ImageFramePacketPerformanceBenchmark)
            });

            switcher.Run(args);
        }
    }
}
