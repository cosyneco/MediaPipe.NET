// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Runtime.InteropServices;

namespace Mediapipe.Net.Native
{
    internal unsafe partial class UnsafeNativeMethods : NativeMethods
    {
#pragma warning disable CA2101
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true, CharSet = CharSet.Ansi)]
        public static extern MpReturnCode absl_Status__i_PKc(int code, string message, out IntPtr status);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void absl_Status__delete(IntPtr status);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode absl_Status__ToString(IntPtr status, out IntPtr str);
#pragma warning restore CA2101
    }
}
