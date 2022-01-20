// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Core;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.Packet
{
    public abstract class Packet<T> : MpResourceHandle
    {
        public Packet() : base()
        {
            UnsafeNativeMethods.mp_Packet__(out var ptr).Assert();
            Ptr = ptr;
        }

        public Packet(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        /// <exception cref="MediaPipeException">Thrown when the value is not set</exception>
        public abstract T Get();

        public abstract StatusOr<T> Consume();

        /// <remarks>To avoid copying the value, instantiate the packet with timestamp</remarks>
        /// <returns>New packet with the given timestamp and the copied value</returns>
        public Packet<T>? At(Timestamp timestamp)
        {
            UnsafeNativeMethods.mp_Packet__At__Rt(MpPtr, timestamp.MpPtr, out var packetPtr).Assert();

            GC.KeepAlive(timestamp);

            // Oh gosh... the Activator...
            return (Packet<T>?)Activator.CreateInstance(GetType(), packetPtr, true);
        }

        public bool IsEmpty() => SafeNativeMethods.mp_Packet__IsEmpty(MpPtr);

        public Status ValidateAsProtoMessageLite()
        {
            UnsafeNativeMethods.mp_Packet__ValidateAsProtoMessageLite(MpPtr, out var statusPtr).Assert();

            GC.KeepAlive(this);
            return new Status(statusPtr);
        }

        // TODO: (from homuler) declare as abstract
        public virtual Status ValidateAsType() => throw new NotImplementedException();

        public Timestamp Timestamp()
        {
            UnsafeNativeMethods.mp_Packet__Timestamp(MpPtr, out var timestampPtr).Assert();

            GC.KeepAlive(this);
            return new Timestamp(timestampPtr);
        }

        public string? DebugString() => MarshalStringFromNative(UnsafeNativeMethods.mp_Packet__DebugString);

        public string RegisteredTypeName()
        {
            var typeName = MarshalStringFromNative(UnsafeNativeMethods.mp_Packet__RegisteredTypeName);

            return typeName ?? "";
        }

        public string? DebugTypeName() => MarshalStringFromNative(UnsafeNativeMethods.mp_Packet__DebugTypeName);

        protected override void DeleteMpPtr() => UnsafeNativeMethods.mp_Packet__delete(Ptr);
    }
}
