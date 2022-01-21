// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.IO;
using Mediapipe.Net.Core;
using Mediapipe.Net.Framework;
using Mediapipe.Net.Framework.Format;
using Mediapipe.Net.Framework.Packet;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Framework.Protobuf;

namespace Mediapipe.Net.Calculators
{
    public class HolisticCpuCalculator : Disposable, ICpuCalculator<List<NormalizedLandmarkList>>
    {
        private const string input_stream = "input_video";
        private const string output_stream0 = "output_video";
        private const string output_stream1 = "landmarks";

        private const string graph_path = "mediapipe/graphs/holistic_tracking/holistic_tracking_cpu.pbtxt";

        private readonly CalculatorGraph graph;

        private readonly OutputStreamPoller<ImageFrame> framePoller;

        public HolisticCpuCalculator()
        {
            graph = new CalculatorGraph(File.ReadAllText(graph_path));
            framePoller = graph.AddOutputStreamPoller<ImageFrame>(output_stream0).Value();
            graph.ObserveOutputStream<NormalizedLandmarkListVectorPacket, List<NormalizedLandmarkList>>(output_stream1, (packet) =>
            {
                List<NormalizedLandmarkList> landmarks = packet.Get();
                OnResult?.Invoke(this, landmarks);
                return Status.Ok();
            }, out var callbackHandle).AssertOk();
        }

        public void Run() => graph.StartRun().AssertOk();

        public ImageFrame Send(ImageFrame frame)
        {
            using ImageFramePacket packet = new ImageFramePacket(frame, new Timestamp(CurrentFrame++));
            graph.AddPacketToInputStream(input_stream, packet).AssertOk();

            ImageFramePacket outPacket = new ImageFramePacket();
            framePoller.Next(outPacket);
            ImageFrame outFrame = outPacket.Get();

            return outFrame;
        }

        public event EventHandler<List<NormalizedLandmarkList>>? OnResult;

        public long CurrentFrame { get; private set; } = 0;

        protected override void DisposeManaged()
        {
            framePoller.Dispose();
            graph.Dispose();
        }
    }
}
