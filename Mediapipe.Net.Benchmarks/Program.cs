// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;

namespace Mediapipe.Net.Benchmarks
{
    public class Config : ManualConfig
    {
        public Config()
        {
            AddDiagnoser(MemoryDiagnoser.Default);
            AddLogger(ConsoleLogger.Default);
            AddExporter(CsvMeasurementsExporter.Default);
            AddExporter(PlainExporter.Default);
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            var switcher = new BenchmarkSwitcher(new[]
            {
                typeof(FloatPacketPerformanceBenchmark),
                typeof(ImageFramePacketPerformanceBenchmark),
                typeof(BlazeNetBenchmark)
            });

            switcher.Run(args, new Config());
        }
    }
}
