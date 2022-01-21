// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Collections.Generic;
using Mediapipe.Net.Framework;
using Mediapipe.Net.Framework.Format;
using Mediapipe.Net.Framework.Packet;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Framework.Protobuf;
using Mediapipe.Net.Gpu;

namespace Mediapipe.Net.Calculators
{
    public class BlazePoseGpuCalculator : IGpuCalculator<NormalizedLandmarkList>
    {
        private const string input_stream = "input_video";
        private const string output_stream0 = "output_video";
        private const string output_stream1 = "pose_landmarks";

        private const string graphPath = "mediapipe/graphs/pose_tracking/pose_tracking_gpu.pbtxt";

        private CalculatorGraph graph;

        private OutputStreamPoller<GpuBuffer> framePoller;

        public BlazePoseGpuCalculator()
        {
            graph = new CalculatorGraph(System.IO.File.ReadAllText(graphPath));
            framePoller = graph.AddOutputStreamPoller<GpuBuffer>(output_stream0).Value();
            graph.ObserveOutputStream<NormalizedLandmarkListPacket, NormalizedLandmarkList>(output_stream1, (packet) =>
            {
                NormalizedLandmarkList landmarks = packet.Get();
                OnResult?.Invoke(this, landmarks);
                return Status.Ok();
            }, out var callbackHandle).AssertOk();
        }

        public void Run() => graph.StartRun();

        public GpuBuffer Send(GpuBuffer frame)
        {
            GpuBufferPacket packet = new GpuBufferPacket(frame, new Timestamp(CurrentFrame++));

            graph.AddPacketToInputStream(input_stream, packet).AssertOk();

            GpuBufferPacket outPacket = new GpuBufferPacket();
            framePoller.Next(packet);
            GpuBuffer outBuffer = outPacket.Get();

            return outBuffer;
        }

        public event EventHandler<NormalizedLandmarkList>? OnResult;

        public long CurrentFrame { get; private set; } = 0;

        public void Dispose()
        {
            framePoller.Dispose();
            graph.Dispose();
        }
    }
}
