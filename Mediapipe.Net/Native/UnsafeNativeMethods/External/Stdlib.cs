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
        public static extern void delete_array__PKc(IntPtr str);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void delete_array__Pf(IntPtr str);

        #region String
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void std_string__delete(IntPtr str);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode std_string__PKc_i(byte[] bytes, int size, out IntPtr str);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void std_string__swap__Rstr(IntPtr src, IntPtr dst);
        #endregion

        #region StatusOrString
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void mp_StatusOrString__delete(IntPtr statusOrString);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_StatusOrString__status(IntPtr statusOrString, out IntPtr status);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_StatusOrString__value(IntPtr statusOrString, out IntPtr value);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_StatusOrString__bytearray(IntPtr statusOrString, out IntPtr value, out int size);
        #endregion
    }
}
