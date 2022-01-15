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
        // #if LINUX || ANDROID
        [Pure, DllImport(MEDIAPIPE_LIBRARY)]
        public static extern IntPtr eglGetCurrentContext();
        // #endif
    }
}
