// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.Packets
{
    public class BoolPacket : Packet<bool>
    {
        public BoolPacket() : base() { }
        public BoolPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }
        public BoolPacket(bool value) : base()
        {
            UnsafeNativeMethods.mp__MakeBoolPacket__b(value, out var ptr).Assert();
            Ptr = ptr;
        }
        public BoolPacket(bool value, Timestamp timestamp) : base()
        {
            UnsafeNativeMethods.mp__MakeBoolPacket_At__b_Rt(value, timestamp.MpPtr, out var ptr).Assert();
            GC.KeepAlive(this);

            Ptr = ptr;
        }

        public BoolPacket? At(Timestamp timestamp) => At<BoolPacket>(timestamp);

        public override bool Get()
        {
            UnsafeNativeMethods.mp_Packet__GetBool(MpPtr, out var value).Assert();
            GC.KeepAlive(this);

            return value;
        }

        public override StatusOr<bool> Consume()
        {
            throw new NotSupportedException();
        }

        public override Status ValidateAsType()
        {
            UnsafeNativeMethods.mp_Packet__ValidateAsBool(MpPtr, out var value).Assert();

            GC.KeepAlive(this);
            return new Status(value);
        }
    }
}
