// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Runtime.InteropServices;

namespace Mediapipe.Net.Native
{
    internal unsafe partial class UnsafeNativeMethods : NativeMethods
    {
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void delete_array__PKc(sbyte* str);

        #region String
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void std_string__delete(sbyte* str);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode std_string__PKc_i(byte[] bytes, int size, out sbyte* str);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void std_string__swap__Rstr(IntPtr src, IntPtr dst);
        #endregion
    }
}
