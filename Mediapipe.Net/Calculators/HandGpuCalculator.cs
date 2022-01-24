// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System.Runtime.Versioning;
using Mediapipe.Net.Framework.Packet;
using Mediapipe.Net.Framework.Protobuf;

namespace Mediapipe.Net.Calculators
{
    [SupportedOSPlatform("Linux"), SupportedOSPlatform("Android")]
    public class HandGpuCalculator : GpuCalculator<NormalizedLandmarkListPacket, NormalizedLandmarkList>
    {
        public HandGpuCalculator() : base(
            graphPath: "mediapipe/graphs/hand_tracking/hand_tracking_desktop_live_gpu.pbtxt",
            secondaryOutputStream: "hand_landmarks")
        {
        }
    }
}
