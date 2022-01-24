// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System.Collections.Generic;
using Mediapipe.Net.Framework.Packet;
using Mediapipe.Net.Framework.Protobuf;

namespace Mediapipe.Net.Calculators
{
    public class FaceMeshCpuCalculator : CpuCalculator<NormalizedLandmarkListVectorPacket, List<NormalizedLandmarkList>>
    {
        protected override string GraphPath => "mediapipe/graphs/face_mesh/face_mesh_desktop_live.pbtxt";
        protected override string SecondaryOutputStream => "multi_face_landmarks";
    }
}
