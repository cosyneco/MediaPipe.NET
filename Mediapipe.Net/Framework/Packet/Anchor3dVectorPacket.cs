// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Collections.Generic;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Graphs.InstantMotionTracking;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.Packet
{
    public class Anchor3dVectorPacket : Packet<List<Anchor3d>>
    {
        public Anchor3dVectorPacket() : base() { }
        public Anchor3dVectorPacket(void* ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public Anchor3dVectorPacket(Anchor3d[] value) : base()
        {
            UnsafeNativeMethods.mp__MakeAnchor3dVectorPacket__PA_i(value, value.Length, out var ptr).Assert();
            Ptr = ptr;
        }

        public Anchor3dVectorPacket(Anchor3d[] value, Timestamp timestamp) : base()
        {
            UnsafeNativeMethods.mp__MakeAnchor3dVectorPacket_At__PA_i_Rt(value, value.Length, timestamp.MpPtr, out var ptr).Assert();
            GC.KeepAlive(timestamp);
            Ptr = ptr;
        }

        public override List<Anchor3d> Get()
        {
            UnsafeNativeMethods.mp_Packet__GetAnchor3dVector(MpPtr, out var anchorVector).Assert();
            GC.KeepAlive(this);

            var anchors = anchorVector.ToList();
            anchorVector.Dispose();

            return anchors;
        }

        public override StatusOr<List<Anchor3d>> Consume() => throw new NotSupportedException();
    }
}
