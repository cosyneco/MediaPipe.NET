// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Runtime.InteropServices;

namespace Mediapipe.Net.Native
{
    internal partial class UnsafeNativeMethods : NativeMethods
    {
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void delete_array__PKc(IntPtr str);

        #region String
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void std_string__delete(IntPtr str);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode std_string__PKc_i(byte[] bytes, int size, out IntPtr str);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void std_string__swap__Rstr(IntPtr src, IntPtr dst);
        #endregion
    }
}