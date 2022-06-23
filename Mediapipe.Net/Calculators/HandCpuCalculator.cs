// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Framework.OldPacket;
using Mediapipe.Net.Framework.Protobuf;

namespace Mediapipe.Net.Calculators
{
    public sealed class HandCpuCalculator : CpuCalculator<NormalizedLandmarkListPacket, NormalizedLandmarkList>
    {
        public HandCpuCalculator() : base(
            graphPath: "mediapipe/graphs/hand_tracking/hand_tracking_desktop_live_cpu.pbtxt",
            secondaryOutputStream: "hand_landmarks")
        {
        }
    }
}
