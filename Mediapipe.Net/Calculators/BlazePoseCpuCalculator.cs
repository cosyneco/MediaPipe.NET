// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Framework.Packet;
using Mediapipe.Net.Framework.Protobuf;

namespace Mediapipe.Net.Calculators
{
    public class BlazePoseCpuCalculator : CpuCalculator<NormalizedLandmarkListPacket, NormalizedLandmarkList>
    {
        public BlazePoseCpuCalculator() : base(
            graphPath: "mediapipe/graphs/pose_tracking/pose_tracking_cpu.pbtxt",
            secondaryOutputStream: "pose_landmarks")
        {
        }
    }
}
