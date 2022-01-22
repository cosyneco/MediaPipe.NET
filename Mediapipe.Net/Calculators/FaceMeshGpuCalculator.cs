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
    public class FaceMeshGpuCalculator : GpuCalculator<List<NormalizedLandmarkList>>
    {
        protected override string OutputStream1 { get; } = "multi_face_landmarks";

        private const string graph_path = "mediapipe/graphs/face_mesh/face_mesh_desktop_live_gpu.pbtxt";

        public FaceMeshGpuCalculator()
        {
            Graph = new CalculatorGraph(System.IO.File.ReadAllText(graph_path));
        }
    }
}
