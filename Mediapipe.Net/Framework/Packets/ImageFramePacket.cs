// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Framework.Format;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Native;
using System;

namespace Mediapipe.Net.Framework.Packets
{
    internal class ImageFramePacket : Packet<ImageFrame>
    {
        public ImageFramePacket() : base() { }
        public ImageFramePacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public ImageFramePacket? At(Timestamp timestamp) => At<ImageFramePacket>(timestamp);
        public override ImageFrame Get()
        {
            UnsafeNativeMethods.mp_Packet__GetImageFrame(MpPtr, out var imageFramePtr).Assert();
            GC.KeepAlive(this);

            return new ImageFrame(imageFramePtr);
        }

        public override StatusOr<ImageFrame> Consume()
        {
            UnsafeNativeMethods.mp_Packet__ConsumeImageFrame(MpPtr, out var imageFramePtr).Assert();

            GC.KeepAlive(this);
            return new StatusOrImageFrame(imageFramePtr);
        }

        public override Status ValidateAsType()
        {
            UnsafeNativeMethods.mp_Packet__ValidateAsImageFrame(MpPtr, out var statusPtr).Assert();

            GC.KeepAlive(this);
            return new Status(statusPtr);
        }
    }
}
