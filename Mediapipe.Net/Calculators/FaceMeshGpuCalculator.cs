// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System.Collections.Generic;
using Mediapipe.Net.Framework;
using Mediapipe.Net.Framework.Format;
using Mediapipe.Net.Framework.Packet;
using Mediapipe.Net.Framework.Protobuf;
using Mediapipe.Net.Gpu;

namespace Mediapipe.Net.Calculators
{
    public class FaceMeshGpuCalculator : IGpuCalculator<List<NormalizedLandmarkList>>
    {
        private const string input_stream = "input_video";
        private const string output_stream0 = "output_video";
        private const string output_stream1 = "multi_face_landmarks";


        private CalculatorGraph graph;

        private OutputStreamPoller<GpuBuffer> framePoller;

        private OutputStreamPoller<List<NormalizedLandmarkList>> landmarksPoller;

        public FaceMeshGpuCalculator()
        {
            graph = new CalculatorGraph();
            framePoller = graph.AddOutputStreamPoller<GpuBuffer>(output_stream0).Value();
            landmarksPoller = graph.AddOutputStreamPoller<List<NormalizedLandmarkList>>(output_stream1).Value();
        }

        public void Run() => graph.StartRun();

        public GpuBuffer Perform(GpuBuffer frame, out List<NormalizedLandmarkList> result)
        {
            GpuBufferPacket packet = new GpuBufferPacket(frame);

            graph.AddPacketToInputStream(input_stream, packet).AssertOk();

            GpuBufferPacket outPacket = new GpuBufferPacket();
            framePoller.Next(packet);
            GpuBuffer outBuffer = outPacket.Get();

            NormalizedLandmarkListVectorPacket landmarksPacket = new NormalizedLandmarkListVectorPacket();
            landmarksPoller.Next(landmarksPacket);
            result = landmarksPacket.Get();

            return outBuffer;
        }

        public void Dispose()
        {
            framePoller.Dispose();
            landmarksPoller.Dispose();
            graph.Dispose();
        }
    }
}
