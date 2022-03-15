// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Framework;
using Mediapipe.Net.Framework.Format;
using Mediapipe.Net.Framework.Packet;

namespace Mediapipe.Net.Calculators
{
    /// <summary>
    /// The base for any CPU calculator.
    /// </summary>
    /// <typeparam name="TPacket">The type of packet the calculator returns the secondary output in.</typeparam>
    /// <typeparam name="T">The type of secondary output.</typeparam>
    public abstract class CpuCalculator<TPacket, T> : Calculator<TPacket, T>
        where TPacket : Packet<T>
    {
        private readonly OutputStreamPoller<ImageFrame> framePoller;

        protected CpuCalculator(string graphPath, string? secondaryOutputStream = null)
            : base(graphPath, secondaryOutputStream)
        {
            framePoller = Graph.AddOutputStreamPoller<ImageFrame>(OUTPUT_VIDEO_STREAM).Value();
        }

        protected override ImageFrame? SendFrame(ImageFrame frame)
        {
            using ImageFramePacket packet = new ImageFramePacket(frame, new Timestamp(CurrentFrame));
            Graph.AddPacketToInputStream(INPUT_VIDEO_STREAM, packet).AssertOk();

            ImageFramePacket outPacket = new ImageFramePacket();
            if (framePoller.Next(outPacket))
            {
                ImageFrame outFrame = outPacket.Get();
                return outFrame;
            }
            else
            {
                // Dispose the unused outPacket
                outPacket.Dispose();
            }

            return null;
        }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            framePoller.Dispose();
        }
    }
}
