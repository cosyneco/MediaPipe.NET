// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Core;
using Mediapipe.Net.Framework.Packet;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework
{
    public class OutputStreamPoller<T> : MpResourceHandle
    {
        public OutputStreamPoller(IntPtr ptr) : base(ptr) { }

        protected override void DeleteMpPtr()
        {
            UnsafeNativeMethods.mp_OutputStreamPoller__delete(Ptr);
        }

        public bool Next(Packet<T> packet)
        {
            UnsafeNativeMethods.mp_OutputStreamPoller__Next_Ppacket(MpPtr, packet.MpPtr, out var result).Assert();

            GC.KeepAlive(this);
            return result;
        }

        public void Reset() => UnsafeNativeMethods.mp_OutputStreamPoller__Reset(MpPtr).Assert();

        public void SetMaxQueueSize(int queueSize) => UnsafeNativeMethods.mp_OutputStreamPoller__SetMaxQueueSize(MpPtr, queueSize).Assert();

        public int QueueSize
        {
            get
            {
                UnsafeNativeMethods.mp_OutputStreamPoller__QueueSize(MpPtr, out var result).Assert();

                GC.KeepAlive(this);
                return result;
            }
        }
    }
}
