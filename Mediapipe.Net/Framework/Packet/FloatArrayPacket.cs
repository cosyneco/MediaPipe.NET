// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Native;

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

            var result = new float[Length];

            unsafe
            {
                var src = (float*)GetArrayPtr();

                for (var i = 0; i < result.Length; i++)
                {
                    result[i] = *src++;
                }
            }

            return result;
        }

        public void* GetArrayPtr()
        {
            UnsafeNativeMethods.mp_Packet__GetFloatArray(MpPtr, out var value).Assert();
            GC.KeepAlive(this);
            return value;
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
