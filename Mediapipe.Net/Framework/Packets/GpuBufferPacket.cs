// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Gpu;
using Mediapipe.Net.Native;
using System;

namespace Mediapipe.Net.Framework.Packets
{
    internal class GpuBufferPacket : Packet<GpuBuffer>
    {
        public GpuBufferPacket() : base() { }
        public GpuBufferPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public GpuBufferPacket? At(Timestamp timestamp) => At<GpuBufferPacket>(timestamp);
        public override GpuBuffer Get()
        {
            UnsafeNativeMethods.mp_Packet__GetGpuBuffer(MpPtr, out var gpuBufferPtr).Assert();
            GC.KeepAlive(this);

            return new GpuBuffer(gpuBufferPtr);
        }

        public override StatusOr<GpuBuffer> Consume()
        {
            UnsafeNativeMethods.mp_Packet__ConsumeGpuBuffer(MpPtr, out var gpuBufferPtr).Assert();

            GC.KeepAlive(this);
            return new StatusOrGpuBuffer(gpuBufferPtr);
        }

        public override Status ValidateAsType()
        {
            UnsafeNativeMethods.mp_Packet__ValidateAsGpuBuffer(MpPtr, out var statusPtr).Assert();

            GC.KeepAlive(this);
            return new Status(statusPtr);
        }
    }
}
