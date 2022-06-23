// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Framework.OldPacket;
using Mediapipe.Net.Framework.Protobuf;

namespace Mediapipe.Net.Calculators
{
    public sealed class BlazePoseCpuCalculator : CpuCalculator<NormalizedLandmarkListPacket, NormalizedLandmarkList>
    {
        /// <summary>
        /// Pose tracking calculator.
        /// </summary>
        /// <param name="smoothLandmarks">
        /// Whether to filter landmarks across different input images to reduce jitter.
        /// </param>
        /// <param name="enableSegmentation">
        /// Whether to predict the segmentation mask.
        /// </param>
        /// <param name="smoothSegmentation">
        /// Whether to filter segmentation mask across different input images to reduce jitter.
        /// </param>
        /// <param name="modelComplexity">
        /// Complexity of the pose landmark model: 0, 1 or 2. Landmark accuracy as well as
        /// inference latency generally go up with the model complexity.
        /// </param>
        /// <param name="usePrevLandmarks">
        /// Whether landmarks on the previous image should be used to help localize landmarks
        /// on the current image.
        /// </param>
        public BlazePoseCpuCalculator(
            bool smoothLandmarks = true,
            bool enableSegmentation = false,
            bool smoothSegmentation = true,
            int modelComplexity = 1,
            bool usePrevLandmarks = true
        ) : base(
            graphPath: "mediapipe/graphs/pose_tracking/pose_tracking_cpu.pbtxt",
            secondaryOutputStream: "pose_landmarks")
        {
            SidePackets = new SidePackets();
            SidePackets.Emplace("smooth_landmarks", new BoolPacket(smoothLandmarks));
            SidePackets.Emplace("enable_segmentation", new BoolPacket(enableSegmentation));
            SidePackets.Emplace("smooth_segmentation", new BoolPacket(smoothSegmentation));
            SidePackets.Emplace("model_complexity", new IntPacket(modelComplexity));
            SidePackets.Emplace("use_prev_landmarks", new BoolPacket(usePrevLandmarks));
        }
    }
}
