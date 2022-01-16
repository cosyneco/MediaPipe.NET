// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Runtime.InteropServices;

namespace Mediapipe.Net.Native
{
    internal partial class SafeNativeMethods : NativeMethods
    {
        // TODO: Make it be a member of ResourceManager
        public delegate bool ResourceProvider(string path, IntPtr output);
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void mp__SetCustomGlobalResourceProvider__P(
            [MarshalAs(UnmanagedType.FunctionPtr)] ResourceProvider provider);

        // TODO: Make it be a member of ResourceManager
        public delegate string PathResolver(string path);
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void mp__SetCustomGlobalPathResolver__P(
            [MarshalAs(UnmanagedType.FunctionPtr)] PathResolver resolver);
    }
}
