// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Framework.Format;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.Packets
{
    public class ImageFramePacket : Packet<ImageFrame>
    {
        public ImageFramePacket() : base() { }
        public ImageFramePacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public ImageFramePacket(ImageFrame imageFrame) : base()
        {
            UnsafeNativeMethods.mp__MakeImageFramePacket__Pif(imageFrame.MpPtr, out var ptr).Assert();
            imageFrame.Dispose();

            Ptr = ptr;
        }

        public ImageFramePacket(ImageFrame imageFrame, Timestamp timestamp) : base()
        {
            UnsafeNativeMethods.mp__MakeImageFramePacket_At__Pif_Rt(imageFrame.MpPtr, timestamp.MpPtr, out var ptr).Assert();
            imageFrame.Dispose();

            Ptr = ptr;
        }

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
