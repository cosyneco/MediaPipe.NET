// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Core;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.OldPacket
{
    /// <summary>
    /// Map of side packets.
    /// </summary>
    public unsafe class SidePackets : MpResourceHandle
    {
        public SidePackets() : base()
        {
            UnsafeNativeMethods.mp_SidePacket__(out var ptr).Assert();
            Ptr = ptr;
        }

        protected override void DeleteMpPtr() => UnsafeNativeMethods.mp_SidePacket__delete(Ptr);

        public int Size => SafeNativeMethods.mp_SidePacket__size(MpPtr);

        /// TODO: force T to be Packet
        /// <remarks>Make sure that the type of the returned packet value is correct</remarks>
        public T? At<T>(string key)
        {
            UnsafeNativeMethods.mp_SidePacket__at__PKc(MpPtr, key, out var packetPtr).Assert();

            if (packetPtr == null)
                return default; // null

            GC.KeepAlive(this);

            // Oh gosh²... the Activator²...
            return (T?)Activator.CreateInstance(typeof(T), (IntPtr)packetPtr, true);
        }

        public void Emplace<T>(string key, Packet<T> packet)
        {
            UnsafeNativeMethods.mp_SidePacket__emplace__PKc_Rp(MpPtr, key, packet.MpPtr).Assert();
            packet.Dispose(); // respect move semantics
            GC.KeepAlive(this);
        }

        public int Erase(string key)
        {
            UnsafeNativeMethods.mp_SidePacket__erase__PKc(MpPtr, key, out var count).Assert();

            GC.KeepAlive(this);
            return count;
        }

        public void Clear() => SafeNativeMethods.mp_SidePacket__clear(MpPtr);
    }
}
