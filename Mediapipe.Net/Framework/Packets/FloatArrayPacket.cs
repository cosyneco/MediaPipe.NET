// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.Packets
{
    internal class FloatArrayPacket : Packet<float[]>
    {
        private int length = -1;

        public int Length
        {
            get => length;
            set
            {
                if (length >= 0)
                    throw new InvalidOperationException("Length has already been set and cannot be changed.");

                length = value;
            }
        }

        public FloatArrayPacket() : base(true) { }
        public FloatArrayPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public FloatArrayPacket(float[] value) : base()
        {
            UnsafeNativeMethods.mp__MakeFloatArrayPacket__Pf_i(value, value.Length, out var ptr).Assert();
            Ptr = ptr;
            length = value.Length;
        }

        public FloatArrayPacket(float[] value, Timestamp timestamp) : base()
        {
            UnsafeNativeMethods.mp__MakeFloatArrayPacket_At__Pf_i_Rt(value, value.Length, timestamp.MpPtr, out var ptr).Assert();
            GC.KeepAlive(timestamp);

            Ptr = ptr;
            length = value.Length;
        }

        public FloatArrayPacket? At(Timestamp timestamp) => At<FloatArrayPacket>(timestamp);

        public override float[] Get()
        {
            UnsafeNativeMethods.mp_Packet__GetFloatArray_i(MpPtr, length, out var arrayPtr).Assert();
            GC.KeepAlive(this);

            var result = new float[length];
            unsafe
            {
                var src = (float*)arrayPtr;

                for (var i = 0; i < length; i++)
                {
                    result[i] = *src++;
                }
            }

            UnsafeNativeMethods.delete_array__Pf(arrayPtr);
            return result;
        }

        public override StatusOr<float[]> Consume()
        {
            throw new NotSupportedException();
        }

        public override Status ValidateAsType()
        {
            UnsafeNativeMethods.mp_Packet__ValidateAsFloatArray(MpPtr, out var statusPtr).Assert();

            GC.KeepAlive(this);
            return new Status(statusPtr);
        }
    }
}
