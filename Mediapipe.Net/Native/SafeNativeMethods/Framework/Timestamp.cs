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
        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern long mp_Timestamp__Value(IntPtr timestamp);

        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern double mp_Timestamp__Seconds(IntPtr timestamp);

        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern long mp_Timestamp__Microseconds(IntPtr timestamp);

        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool mp_Timestamp__IsSpecialValue(IntPtr timestamp);

        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool mp_Timestamp__IsRangeValue(IntPtr timestamp);

        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool mp_Timestamp__IsAllowedInStream(IntPtr timestamp);
    }
}