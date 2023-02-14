// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.


using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Native;
using System;
using System.Runtime.InteropServices;

namespace Mediapipe.Net.Framework.Packets
{
    internal class StringPacket : Packet<string>
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
            UnsafeNativeMethods.mp__MakeStringPacket__PKc_i(bytes, bytes.Length, out IntPtr ptr).Assert();
        }

        public StringPacket(string value, Timestamp timestamp) : base()
        {
            UnsafeNativeMethods.mp__MakeStringPacket_At__PKc_Rt(value, timestamp.MpPtr, out var ptr).Assert();
            Ptr = ptr;
        }

        public StringPacket(byte[] bytes, Timestamp timestamp) : base()
        {
            UnsafeNativeMethods.mp__MakeStringPacket_At__PKc_i_Rt(bytes, bytes.Length, timestamp.MpPtr, out IntPtr ptr).Assert();
            GC.KeepAlive(timestamp);
            Ptr = ptr;
        }

        public StringPacket? At(Timestamp timestamp) => At<StringPacket>(timestamp);

        public override string? Get() => MarshalStringFromNative(UnsafeNativeMethods.mp_Packet__GetString);

        public byte[] GetByteArray()
        {
            UnsafeNativeMethods.mp_Packet__GetByteString(MpPtr, out var strPtr, out var size).Assert();
            GC.KeepAlive(this);

            var bytes = new byte[size];
            Marshal.Copy(strPtr, bytes, 0, size);
            UnsafeNativeMethods.delete_array__PKc(strPtr);
            return bytes;
        }

        public override StatusOr<string> Consume()
        {
            UnsafeNativeMethods.mp_Packet__ConsumeString(MpPtr, out var statusOrStringPtr).Assert();

            GC.KeepAlive(this);
            return new StatusOrString(statusOrStringPtr);
        }

        public override Status ValidateAsType()
        {
            UnsafeNativeMethods.mp_Packet__ValidateAsString(MpPtr, out var str).Assert();

            GC.KeepAlive(this);
            return new Status(str);
        }
    }
}
