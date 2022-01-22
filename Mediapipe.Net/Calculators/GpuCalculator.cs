// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.IO;
using System.Runtime.Versioning;
using Mediapipe.Net.Core;
using Mediapipe.Net.Framework;
using Mediapipe.Net.Framework.Format;
using Mediapipe.Net.Framework.Packet;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Gpu;

namespace Mediapipe.Net.Calculators
{
    public abstract class GpuCalculator<TPacket, T> : Disposable, ICalculator<T>
        where TPacket : Packet<T>
    {
        private const string input_video_stream = "input_video";
        private const string output_video_stream = "output_video";

        protected abstract string GraphPath { get; }
        protected abstract string? SecondaryOutputStream { get; }

        private readonly CalculatorGraph graph;
        private readonly GpuResources gpuResources;
        private readonly GlCalculatorHelper gpuHelper;
        private readonly OutputStreamPoller<GpuBuffer> framePoller;

        [SupportedOSPlatform("linux"), SupportedOSPlatform("android")]
        protected GpuCalculator()
        {
            graph = new CalculatorGraph(File.ReadAllText(GraphPath));

            gpuResources = GpuResources.Create().Value();
            graph.SetGpuResources(gpuResources);
            gpuHelper = new GlCalculatorHelper();
            gpuHelper.InitializeForTest(graph.GetGpuResources());

            framePoller = graph.AddOutputStreamPoller<GpuBuffer>(output_video_stream).Value();

            if (SecondaryOutputStream != null)
            {
                graph.ObserveOutputStream<TPacket, T>(SecondaryOutputStream, (packet) =>
                {
                    if (packet == null)
                        return Status.Ok();

                    T secondaryOutput = packet.Get();
                    OnResult?.Invoke(this, secondaryOutput);
                    return Status.Ok();
                }, out var callbackHandle).AssertOk();
            }
        }

        public void Run() => graph.StartRun().AssertOk();

        public ImageFrame Send(ImageFrame frame)
        {
            gpuHelper.RunInGlContext(() =>
            {
                GlTexture texture = gpuHelper.CreateSourceTexture(frame);
                GpuBuffer gpuBuffer = texture.GetGpuBufferFrame();
                Gl.Flush();
                texture.Release();

                var packet = new GpuBufferPacket(gpuBuffer, new Timestamp(CurrentFrame++));
                graph.AddPacketToInputStream(input_video_stream, packet);

                return Status.Ok();
            }).AssertOk();

            GpuBufferPacket outPacket = new GpuBufferPacket();
            framePoller.Next(outPacket);

            ImageFrame? outFrame = null;

            gpuHelper.RunInGlContext(() =>
            {
                GpuBuffer outBuffer = outPacket.Get();
                GlTexture texture = gpuHelper.CreateSourceTexture(outBuffer);
                outFrame = new ImageFrame(outBuffer.Format.ImageFormatFor(), outBuffer.Width, outBuffer.Height, ImageFrame.GlDefaultAlignmentBoundary);
                gpuHelper.BindFramebuffer(texture);
                GlTextureInfo info = outBuffer.Format.GlTextureInfoFor(0);
                Gl.ReadPixels(0, 0, texture.Width, texture.Height, info.GlFormat, info.GlType, outFrame.MutablePixelData);
                Gl.Flush();
                texture.Release();

                return Status.Ok();
            }).AssertOk();

            if (outFrame == null)
                throw new MediapipeNetException("!! FATAL - Frame is null on current GL context run!");

            return outFrame;
        }

        public event EventHandler<T>? OnResult;
        public long CurrentFrame { get; private set; }

        protected override void DisposeManaged()
        {
            graph.Dispose();
            gpuResources.Dispose();
            gpuHelper.Dispose();
            framePoller.Dispose();
        }
    }
}
