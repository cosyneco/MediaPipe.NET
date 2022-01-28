// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Native;
using Mediapipe.Net.Util;

namespace Mediapipe.Net.Framework.Packet
{
    public unsafe class StringPacket : Packet<string>
    {
        public StringPacket() : base() { }

        public StringPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public StringPacket(string value) : base()
        {
            UnsafeNativeMethods.mp__MakeStringPacket__PKc(value, out var ptr).Assert();
            Ptr = ptr;
        }

        public StringPacket(byte[] bytes) : base()
        {
            UnsafeNativeMethods.mp__MakeStringPacket__PKc_i(bytes, bytes.Length, out var ptr).Assert();
            Ptr = ptr;
        }

        public StringPacket(string value, Timestamp timestamp) : base()
        {
            UnsafeNativeMethods.mp__MakeStringPacket_At__PKc_Rt(value, timestamp.MpPtr, out var ptr).Assert();
            GC.KeepAlive(timestamp);
            Ptr = ptr;
        }

        public StringPacket(byte[] bytes, Timestamp timestamp) : base()
        {
            UnsafeNativeMethods.mp__MakeStringPacket_At__PKc_i_Rt(bytes, bytes.Length, timestamp.MpPtr, out var ptr).Assert();
            GC.KeepAlive(timestamp);
            Ptr = ptr;
        }

        // TODO: review nullability of strings here...
        public override string Get() => MarshalStringFromNative(UnsafeNativeMethods.mp_Packet__GetString) ?? "";

        public byte[] GetByteArray()
        {
            UnsafeNativeMethods.mp_Packet__GetByteString(MpPtr, out var strPtr, out var size).Assert();
            GC.KeepAlive(this);

            byte[] bytes = UnsafeUtil.SafeArrayCopy((byte*)strPtr, size);
            UnsafeNativeMethods.delete_array__PKc(strPtr);

            return bytes;
        }

        public override StatusOr<string> Consume() => throw new NotSupportedException();

        public override Status ValidateAsType()
        {
            UnsafeNativeMethods.mp_Packet__ValidateAsString(MpPtr, out var statusPtr).Assert();

            GC.KeepAlive(this);
            return new Status(statusPtr);
        }
    }
}
