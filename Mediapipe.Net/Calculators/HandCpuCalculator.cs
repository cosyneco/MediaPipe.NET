// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Framework.Packet;
using Mediapipe.Net.Framework.Protobuf;

namespace Mediapipe.Net.Calculators
{
    public class HandCpuCalculator : CpuCalculator<NormalizedLandmarkListPacket, NormalizedLandmarkList>
    {
        protected override string GraphPath { get; } = "mediapipe/graphs/hand_tracking/hand_tracking_desktop_live_cpu.pbtxt";
        protected override string SecondaryOutputStream => "hand_landmarks";

        public HandCpuCalculator()
        {

        }

        public HandCpuCalculator(string graphPath)
        {
            GraphPath = graphPath;
        }
    }
}
