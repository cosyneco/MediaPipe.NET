// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.IO;
using System.Runtime.InteropServices;
using Mediapipe.Net.Core;
using Mediapipe.Net.Framework;
using Mediapipe.Net.Framework.Format;
using Mediapipe.Net.Framework.Packet;
using Mediapipe.Net.Framework.Port;

namespace Mediapipe.Net.Calculators
{
    /// <summary>
    /// This abstract class is the base for any CPU calculator.
    /// </summary>
    /// <typeparam name="TPacket">The type of packet the calculator returns the landmarks in.</typeparam>
    /// <typeparam name="T">The type of landmarks output.</typeparam>
    public abstract class CpuCalculator<TPacket, T> : Disposable, ICalculator<T>
        where TPacket : Packet<T>
    {
        private const string input_video_stream = "input_video";
        private const string output_video_stream = "output_video";

        protected abstract string GraphPath { get; }
        protected abstract string? SecondaryOutputStream { get; }

        private readonly CalculatorGraph graph;
        private readonly OutputStreamPoller<ImageFrame> framePoller;
        private readonly GCHandle observeStreamHandle;

        public event EventHandler<T>? OnResult;

        protected CpuCalculator()
        {
            graph = new CalculatorGraph(File.ReadAllText(GraphPath));
            framePoller = graph.AddOutputStreamPoller<ImageFrame>(output_video_stream).Value();

            if (SecondaryOutputStream != null)
            {
                graph.ObserveOutputStream<TPacket, T>(SecondaryOutputStream, (packet) =>
                {
                    if (packet == null)
                        return Status.Ok();

                    T secondaryOutput = packet.Get();
                    OnResult?.Invoke(this, secondaryOutput);
                    return Status.Ok();
                }, out observeStreamHandle).AssertOk();
            }
        }

        public void Run() => graph.StartRun().AssertOk();

        public ImageFrame Send(ImageFrame frame)
        {
            using ImageFramePacket packet = new ImageFramePacket(frame, new Timestamp(CurrentFrame++));
            graph.AddPacketToInputStream(input_video_stream, packet).AssertOk();

            ImageFramePacket outPacket = new ImageFramePacket();
            framePoller.Next(outPacket);
            ImageFrame outFrame = outPacket.Get();

            return outFrame;
        }

        public long CurrentFrame { get; private set; } = 0;

        protected override void DisposeManaged()
        {
            graph.CloseInputStream(input_video_stream);
            graph.WaitUntilDone();
            graph.Dispose();

            observeStreamHandle.Free();
            framePoller.Dispose();
        }
    }
}
