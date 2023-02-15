// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Native;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Mediapipe.Net.Framework.Packets
{
    [StructLayout(LayoutKind.Sequential)]
    internal readonly struct FloatVector : IDisposable
    {
        private readonly IntPtr data;
        private readonly int size;

        public List<float> Copy()
        {
            var dataList = new List<float>();
            unsafe
            {
                float* floatPtr = (float*)data;

                for (var i = 0; i < size; i++)
                {
                    dataList.Add(*floatPtr++);
                }
            }

            return dataList;
        }

        public void Dispose()
        {
            UnsafeNativeMethods.delete_array__Pf(data);
        }
    }

    public class FloatVectorPacket : Packet<List<float>>
    {
        public FloatVectorPacket() : base(true) { }

        public FloatVectorPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public FloatVectorPacket(float[] value) : base()
        {
            UnsafeNativeMethods.mp__MakeFloatArrayPacket__Pf_i(value, value.Length, out var ptr).Assert();
            Ptr = ptr;
        }

        public FloatVectorPacket(List<float> value) : base()
        {
            UnsafeNativeMethods.mp__MakeFloatVectorPacket__Pf_i(value.ToArray(), value.Count, out var ptr).Assert();
            Ptr = ptr;
        }

        public FloatVectorPacket(List<float> value, Timestamp timestamp) : base()
        {
            UnsafeNativeMethods.mp__MakeFloatVectorPacket_At__Pf_i_Rt(value.ToArray(), value.Count, timestamp.MpPtr, out var ptr).Assert();
            Ptr = ptr;
        }

        public FloatVectorPacket? At(Timestamp timestamp) => At<FloatVectorPacket>(timestamp);

        public override List<float> Get()
        {
            UnsafeNativeMethods.mp_Packet__GetFloatVector(MpPtr, out var floatVec).Assert();
            GC.KeepAlive(this);

            var result = floatVec.Copy();
            floatVec.Dispose();
            return result;
        }

        public override StatusOr<List<float>> Consume()
        {
            throw new NotSupportedException();
        }

        public override Status ValidateAsType()
        {
            UnsafeNativeMethods.mp_Packet__ValidateAsFloatVector(MpPtr, out var statusPtr).Assert();

            GC.KeepAlive(this);
            return new Status(statusPtr);
        }
    }
}
