// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace Mediapipe.Net.Native
{
    internal partial class SafeNativeMethods : NativeMethods
    {
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern bool mp_Packet__IsEmpty(IntPtr packet);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void mp_SidePacket__clear(IntPtr sidePacket);

        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern int mp_SidePacket__size(IntPtr sidePacket);
    }
}
