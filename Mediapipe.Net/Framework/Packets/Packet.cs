// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Core;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.Packets
{
    public abstract class Packet<T> : MpResourceHandle
    {
        protected Packet() : base() { }

        protected Packet(bool isOwner) : base(isOwner)
        {
            if (isOwner)
            {
                unsafe
                {
                    UnsafeNativeMethods.mp_Packet__(out var ptr).Assert();
                    Ptr = ptr;
                }
            }
        }

        protected Packet(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public static TPacket? Create<TPacket>(IntPtr packetPtr, bool isOwner) where TPacket : Packet<T>, new()
        {
            return Activator.CreateInstance(typeof(TPacket), packetPtr, isOwner) as TPacket;
        }

        public void SwitchNativePtr(IntPtr nativePtr)
        {
            if (IsOwner)
                throw new InvalidOperationException("This operation is only permitted when the packet instance is referenced");

            Ptr = nativePtr;
        }

        public abstract T? Get();

        public abstract StatusOr<T> Consume();

        public bool IsEmpty() => SafeNativeMethods.mp_Packet__IsEmpty(MpPtr);

        public Status ValidateAsProtoMessageLite()
        {
            UnsafeNativeMethods.mp_Packet__ValidateAsProtoMessageLite(MpPtr, out var statusPtr).Assert();

            GC.KeepAlive(this);
            return new Status(statusPtr);
        }

        // This was marked as a TODO on MPU.
        // I'm not sure if this is the correct approach
        public abstract Status ValidateAsType();

        public Timestamp Timestamp()
        {
            UnsafeNativeMethods.mp_Packet__Timestamp(MpPtr, out var timestampPtr).Assert();

            GC.KeepAlive(this);
            return new Timestamp(timestampPtr);
        }

        public string? DebugString() => MarshalStringFromNative(UnsafeNativeMethods.mp_Packet__DebugString);

        public string RegisteredTypeName() => MarshalStringFromNative(UnsafeNativeMethods.mp_Packet__RegisteredTypeName) ?? string.Empty;

        public string? DebugTypeName() => MarshalStringFromNative(UnsafeNativeMethods.mp_Packet__DebugTypeName);

        protected override void DeleteMpPtr() => UnsafeNativeMethods.mp_Packet__delete(Ptr);

        protected TPacket? At<TPacket>(Timestamp ts) where TPacket : Packet<T>, new()
        {
            UnsafeNativeMethods.mp_Packet__At__Rt(MpPtr, ts.MpPtr, out var packetPtr).Assert();
            GC.KeepAlive(ts);

            return Create<TPacket>(packetPtr, true);
        }

    }
}
