// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System.Collections.Generic;
using System.IO;
using Mediapipe.Net.Core;
using Mediapipe.Net.Framework;
using Mediapipe.Net.Framework.Format;
using Mediapipe.Net.Framework.Packet;
using Mediapipe.Net.Framework.Protobuf;

namespace Mediapipe.Net.Calculators
{
    public class FaceMeshCpuCalculator : Disposable, ICpuCalculator<List<NormalizedLandmarkList>>
    {
        private const string input_stream = "input_video";
        private const string output_stream0 = "output_video";
        private const string output_stream1 = "multi_face_landmarks";

        private const string graph_path = "mediapipe/graphs/face_mesh/face_mesh_desktop_live.pbtxt";

        private readonly CalculatorGraph graph;

        private readonly OutputStreamPoller<ImageFrame> framePoller;

        private readonly OutputStreamPoller<List<NormalizedLandmarkList>> landmarksPoller;

        public FaceMeshCpuCalculator()
        {
            graph = new CalculatorGraph(File.ReadAllText(graph_path));
            framePoller = graph.AddOutputStreamPoller<ImageFrame>(output_stream0).Value();
            landmarksPoller = graph.AddOutputStreamPoller<List<NormalizedLandmarkList>>(output_stream1).Value();
        }

        public void Run() => graph.StartRun().AssertOk();

        public ImageFrame Perform(ImageFrame frame, out List<NormalizedLandmarkList> result)
        {
            using ImageFramePacket packet = new ImageFramePacket(frame, new Timestamp(CurrentFrame++));
            graph.AddPacketToInputStream(input_stream, packet).AssertOk();

            using ImageFramePacket outPacket = new ImageFramePacket();
            framePoller.Next(outPacket);
            ImageFrame outFrame = outPacket.Get();

            NormalizedLandmarkListVectorPacket landmarksPacket = new NormalizedLandmarkListVectorPacket();
            landmarksPoller.Next(landmarksPacket);
            result = landmarksPacket.Get();

            return outFrame;
        }

        public long CurrentFrame { get; private set; } = 0;

        protected override void DisposeManaged()
        {
            framePoller.Dispose();
            landmarksPoller.Dispose();
            graph.Dispose();
        }
    }
}
