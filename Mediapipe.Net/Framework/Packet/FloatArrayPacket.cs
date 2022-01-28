// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Native;
using Mediapipe.Net.Util;

namespace Mediapipe.Net.Framework.Packet
{
    public unsafe class FloatArrayPacket : Packet<float[]>
    {
        private int length = -1;

        public int Length
        {
            get => length;
            set
            {
                if (length >= 0)
                    throw new InvalidOperationException("Length is already set and cannot be changed");

                length = value;
            }
        }

        public FloatArrayPacket() : base() { }

        public FloatArrayPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public FloatArrayPacket(float[] value) : base()
        {
            UnsafeNativeMethods.mp__MakeFloatArrayPacket__Pf_i(value, value.Length, out var ptr).Assert();
            Ptr = ptr;
            Length = value.Length;
        }

        public FloatArrayPacket(float[] value, Timestamp timestamp) : base()
        {
            UnsafeNativeMethods.mp__MakeFloatArrayPacket_At__Pf_i_Rt(value, value.Length, timestamp.MpPtr, out var ptr).Assert();
            GC.KeepAlive(timestamp);
            Ptr = ptr;
            Length = value.Length;
        }

        public override float[] Get()
        {
            if (Length < 0)
                throw new InvalidOperationException("The array's length is unknown, set Length first");

            return UnsafeUtil.SafeArrayCopy(GetArrayPtr(), Length);
        }

        public float* GetArrayPtr()
        {
            UnsafeNativeMethods.mp_Packet__GetFloatArray(MpPtr, out float* array).Assert();
            GC.KeepAlive(this);
            return array;
        }

        public override StatusOr<float[]> Consume() => throw new NotSupportedException();

        public override Status ValidateAsType()
        {
            UnsafeNativeMethods.mp_Packet__ValidateAsFloatArray(MpPtr, out var statusPtr).Assert();

            GC.KeepAlive(this);
            return new Status(statusPtr);
        }
    }
}
