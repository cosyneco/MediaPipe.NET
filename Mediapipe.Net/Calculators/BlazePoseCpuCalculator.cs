// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Framework.Packet;
using Mediapipe.Net.Framework.Protobuf;

namespace Mediapipe.Net.Calculators
{
    public sealed class BlazePoseCpuCalculator : CpuCalculator<NormalizedLandmarkListPacket, NormalizedLandmarkList>
    {
        public BlazePoseCpuCalculator() : base(
            graphPath: "mediapipe/modules/pose_landmark/pose_landmark_cpu.pbtxt",
            secondaryOutputStream: "pose_landmarks")
        {
        }
    }
}
