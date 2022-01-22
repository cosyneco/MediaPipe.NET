// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Framework;
using Mediapipe.Net.Framework.Format;
using Mediapipe.Net.Framework.Packet;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Gpu;

namespace Mediapipe.Net.Calculators
{
    public abstract class GpuCalculator<T> : IGpuCalculator<T>
    {
        protected const string InputStream = "input_video";
        protected const string OutputStream0 = "output_video";

        protected abstract string OutputStream1 { get; }

        protected GpuResources Resources;

        protected GlCalculatorHelper? CalculatorHelper;

        protected CalculatorGraph? Graph;

        protected OutputStreamPoller<GpuBuffer>? FramePoller;

        public GpuCalculator()
        {
            Resources = GpuResources.Create().Value();
            CalculatorHelper = new GlCalculatorHelper();

            CalculatorHelper.InitializeForTest(Resources);
        }

        protected void InitializeGraph()
        {
            FramePoller = Graph.AddOutputStreamPoller<GpuBuffer>(OutputStream0).Value();
            Graph.ObserveOutputStream<Packet<T>, T>(OutputStream1, (packet) =>
            {
                T landmarks = packet.Get();
                OnResult?.Invoke(this, landmarks);
                return Status.Ok();
            }, out var callbackHandle).AssertOk();
        }

        public void Run() => Graph?.StartRun().AssertOk();

        public ImageFrame Send(ImageFrame frame)
        {
            CalculatorHelper.RunInGlContext(delegate
            {
                var texture = CalculatorHelper?.CreateSourceTexture(frame);
                var gpuFrame = texture?.GetGpuBufferFrame();
                var outputFrame =
    
            });
            using GpuBufferPacket packet = new GpuBufferPacket(frame, new Timestamp(CurrentFrame++));

            Graph.AddPacketToInputStream(InputStream, packet).AssertOk();

            GpuBufferPacket outPacket = new GpuBufferPacket();
            FramePoller.Next(packet);
            GpuBuffer outBuffer = outPacket.Get();

            return outBuffer;
        }

        public virtual event EventHandler<T>? OnResult;
        public long CurrentFrame { get; private set; }

        public void Dispose()
        {
            Resources.Dispose();
            Graph?.Dispose();
            CalculatorHelper?.Dispose();
            FramePoller?.Dispose();
        }
    }
}