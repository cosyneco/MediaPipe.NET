// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.OldPacket
{
    public unsafe class IntPacket : Packet<int>
    {
        public IntPacket() : base() { }

        public IntPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public IntPacket(int value) : base()
        {
            UnsafeNativeMethods.mp__MakeIntPacket__i(value, out var ptr).Assert();
            Ptr = ptr;
        }

        public IntPacket(int value, Timestamp timestamp) : base()
        {
            UnsafeNativeMethods.mp__MakeIntPacket_At__i_Rt(value, timestamp.MpPtr, out var ptr).Assert();
            GC.KeepAlive(timestamp);
            Ptr = ptr;
        }

        public override int Get()
        {
            UnsafeNativeMethods.mp_Packet__GetInt(MpPtr, out var value).Assert();

            GC.KeepAlive(this);
            return value;
        }

        public override StatusOr<int> Consume() => throw new NotSupportedException();

        public override Status ValidateAsType()
        {
            UnsafeNativeMethods.mp_Packet__ValidateAsInt(MpPtr, out var statusPtr).Assert();

            GC.KeepAlive(this);
            return new Status(statusPtr);
        }
    }
}
