// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System.Runtime.Versioning;
using Mediapipe.Net.Core;
using Mediapipe.Net.Framework;
using Mediapipe.Net.Framework.Format;
using Mediapipe.Net.Framework.Packets;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Gpu;

namespace Mediapipe.Net.Calculators
{
    /// <summary>
    /// The base for any GPU calculator.
    /// </summary>
    /// <typeparam name="TPacket">The type of packet the calculator returns the secondary output in.</typeparam>
    /// <typeparam name="T">The type of secondary output.</typeparam>
    [SupportedOSPlatform("Linux"), SupportedOSPlatform("Android")]
    public abstract class GpuCalculator : Calculator
    {
        private readonly GpuResources gpuResources;
        private readonly GlCalculatorHelper gpuHelper;
        private readonly OutputStreamPoller framePoller;

        protected GpuCalculator(string graphPath, string? secondaryOutputStream)
            : base(graphPath, secondaryOutputStream)
        {
            gpuResources = GpuResources.Create().Value();
            Graph.SetGpuResources(gpuResources);
            gpuHelper = new GlCalculatorHelper();
            gpuHelper.InitializeForTest(Graph.GetGpuResources());

            framePoller = Graph.AddOutputStreamPoller(OUTPUT_VIDEO_STREAM).Value();
        }

        protected override void SendFrame(ImageFrame frame)
        {
            gpuHelper.RunInGlContext(() =>
            {
                GlTexture texture = gpuHelper.CreateSourceTexture(frame);
                GpuBuffer gpuBuffer = texture.GetGpuBufferFrame();
                Gl.Flush();
                texture.Release();

                var packet = PacketFactory.GpuBufferPacket(gpuBuffer, new Timestamp(CurrentFrame));
                Graph.AddPacketToInputStream(INPUT_VIDEO_STREAM, packet);

                return Status.Ok();
            }).AssertOk();

            Packet outPacket = new Packet();
            framePoller.Next(outPacket);

            ImageFrame? outFrame = null;

            gpuHelper.RunInGlContext(() =>
            {
                GpuBuffer outBuffer = outPacket.GetGpuBuffer();
                GlTexture texture = gpuHelper.CreateSourceTexture(outBuffer);
                outFrame = new ImageFrame(outBuffer.Format.ImageFormatFor(), outBuffer.Width, outBuffer.Height, ImageFrame.GlDefaultAlignmentBoundary);
                gpuHelper.BindFramebuffer(texture);
                GlTextureInfo info = outBuffer.Format.GlTextureInfoFor(0);
                unsafe
                {
                    Gl.ReadPixels(0, 0, texture.Width, texture.Height, info.GlFormat, info.GlType, outFrame.MutablePixelData);
                }
                Gl.Flush();
                texture.Release();

                return Status.Ok();
            }).AssertOk();

            if (outFrame == null)
                throw new MediapipeNetException("!! FATAL - Frame is null on current GL context run!");

            // return outFrame;
        }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            gpuResources.Dispose();
            gpuHelper.Dispose();
            framePoller.Dispose();
        }
    }
}
