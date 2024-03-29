// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Runtime.InteropServices;
using Mediapipe.Net.Util;

namespace Mediapipe.Net.Native
{
    internal unsafe partial class SafeNativeMethods : NativeMethods
    {
        public delegate bool UnsafeResourceProvider(string path, IntPtr output);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void mp__SetCustomGlobalResourceProvider__P([MarshalAs(UnmanagedType.FunctionPtr)] UnsafeResourceProvider provider);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void mp__SetCustomGlobalPathResolver__P(
            [MarshalAs(UnmanagedType.FunctionPtr)] ResourceManager.PathResolver resolver);
    }
}
