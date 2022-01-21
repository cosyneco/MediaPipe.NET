// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System.Collections.Generic;
using Mediapipe.Net.Framework;
using Mediapipe.Net.Framework.Format;
using Mediapipe.Net.Framework.Packet;
using Mediapipe.Net.Framework.Protobuf;

namespace Mediapipe.Net.Calculators
{
    public class FaceMeshCpuCalculator : ICpuCalculator<List<NormalizedLandmarkList>>
    {
        private const string input_stream = "input_video";
        private const string output_stream0 = "output_video";
        private const string output_stream1 = "multi_face_landmarks";



        private CalculatorGraph graph;

        private OutputStreamPoller<ImageFrame> framePoller;

        private OutputStreamPoller<List<NormalizedLandmarkList>> landmarksPoller;

        public FaceMeshCpuCalculator()
        {
            graph = new CalculatorGraph();
            framePoller = graph.AddOutputStreamPoller<ImageFrame>(output_stream0).Value();
            landmarksPoller = graph.AddOutputStreamPoller<List<NormalizedLandmarkList>>(output_stream1).Value();
        }

        public void Run() => graph.StartRun().AssertOk();

        public ImageFrame Perform(ImageFrame frame, out List<NormalizedLandmarkList> result)
        {
            ImageFramePacket packet = new ImageFramePacket(frame);

            graph.AddPacketToInputStream(input_stream, packet).AssertOk();

            ImageFramePacket outPacket = new ImageFramePacket();
            framePoller.Next(packet);
            ImageFrame outFrame = outPacket.Get();

            NormalizedLandmarkListVectorPacket landmarksPacket = new NormalizedLandmarkListVectorPacket();
            landmarksPoller.Next(landmarksPacket);
            result = landmarksPacket.Get();

            return outFrame;
        }

        public void Dispose()
        {
            framePoller.Dispose();
            landmarksPoller.Dispose();
            graph.Dispose();
        }
    }
}
