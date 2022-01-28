// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.Packet
{
    public unsafe class FloatPacket : Packet<float>
    {
        public FloatPacket() : base() { }

        public FloatPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public FloatPacket(float value) : base()
        {
            UnsafeNativeMethods.mp__MakeFloatPacket__f(value, out var ptr).Assert();
            Ptr = ptr;
        }

        public FloatPacket(float value, Timestamp timestamp) : base()
        {
            UnsafeNativeMethods.mp__MakeFloatPacket_At__f_Rt(value, timestamp.MpPtr, out var ptr).Assert();
            GC.KeepAlive(timestamp);
            Ptr = ptr;
        }

        public override float Get()
        {
            UnsafeNativeMethods.mp_Packet__GetFloat(MpPtr, out var value).Assert();

            GC.KeepAlive(this);
            return value;
        }

        public override StatusOr<float> Consume() => throw new NotSupportedException();

        public override Status ValidateAsType()
        {
            UnsafeNativeMethods.mp_Packet__ValidateAsFloat(MpPtr, out var statusPtr).Assert();

            GC.KeepAlive(this);
            return new Status(statusPtr);
        }
    }
}
