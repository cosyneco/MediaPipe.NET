// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Framework;
using Mediapipe.Net.Framework.Format;
using Mediapipe.Net.Framework.Packets;

namespace Mediapipe.Net.Calculators
{
    /// <summary>
    /// The base for any CPU calculator.
    /// </summary>
    /// <typeparam name="TPacket">The type of packet the calculator returns the secondary output in.</typeparam>
    /// <typeparam name="T">The type of secondary output.</typeparam>
    public abstract class CpuCalculator<TPacket, T> : Calculator
    {
        private readonly OutputStreamPoller framePoller;

        protected CpuCalculator(string graphPath, string? secondaryOutputStream = null)
            : base(graphPath, secondaryOutputStream)
        {
            framePoller = Graph.AddOutputStreamPoller(OUTPUT_VIDEO_STREAM).Value();
        }

        protected override void SendFrame(ImageFrame frame)
        {
            using Packet packet = PacketFactory.ImageFramePacket(frame, new Timestamp(CurrentFrame));
            Graph.AddPacketToInputStream(INPUT_VIDEO_STREAM, packet).AssertOk();

            Packet outPacket = new Packet();
            if (framePoller.Next(outPacket))
            {
                ImageFrame outFrame = outPacket.GetImageFrame();
                // return outFrame;
            }
            else
            {
                // Dispose the unused outPacket
                outPacket.Dispose();
            }

            // return null;
        }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            framePoller.Dispose();
        }
    }
}
