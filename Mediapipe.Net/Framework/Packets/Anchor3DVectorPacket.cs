// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Collections.Generic;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Graphs.InstantMotionTracking;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.Packets
{
    public class Anchor3dVectorPacket : Packet<List<Anchor3d>>
    {
        public Anchor3dVectorPacket() : base(true) { }
        public Anchor3dVectorPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }
        public Anchor3dVectorPacket(Anchor3d[] value) : base()
        {
            UnsafeNativeMethods.mp__MakeAnchor3dVectorPacket__PA_i(value, value.Length, out var ptr).Assert();
            Ptr = ptr;
        }
        public Anchor3dVectorPacket(Anchor3d[] value, Timestamp timestamp) : base()
        {
            UnsafeNativeMethods.mp__MakeAnchor3dVectorPacket_At__PA_i_Rt(value, value.Length, timestamp.MpPtr, out var ptr).Assert();
            GC.KeepAlive(this);

            Ptr = ptr;
        }

        public Anchor3dVectorPacket? At(Timestamp timestamp) => At<Anchor3dVectorPacket>(timestamp);

        public override List<Anchor3d> Get()
        {
            UnsafeNativeMethods.mp_Packet__GetAnchor3dVector(MpPtr, out var anchorVector).Assert();
            GC.KeepAlive(this);

            var list = anchorVector.ToList();
            return list;
        }

        public override StatusOr<List<Anchor3d>> Consume()
        {
            throw new NotSupportedException();
        }

        public override Status ValidateAsType()
        {
            throw new NotImplementedException();
        }
    }
}
