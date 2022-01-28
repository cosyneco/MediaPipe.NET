// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System.Collections.Generic;
using System.Runtime.Versioning;
using Mediapipe.Net.Framework.Packet;
using Mediapipe.Net.Framework.Protobuf;

namespace Mediapipe.Net.Calculators
{
    [SupportedOSPlatform("Linux"), SupportedOSPlatform("Android")]
    public sealed class FaceMeshGpuCalculator : GpuCalculator<NormalizedLandmarkListVectorPacket, List<NormalizedLandmarkList>>
    {
        public FaceMeshGpuCalculator() : base(
            graphPath: "mediapipe/graphs/face_mesh/face_mesh_desktop_live_gpu.pbtxt",
            secondaryOutputStream: "multi_face_landmarks")
        {
        }
    }
}
