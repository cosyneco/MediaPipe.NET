// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace Mediapipe.Net.Native
{
    internal partial class SafeNativeMethods : NativeMethods
    {
        [SupportedOSPlatform("Linux"), SupportedOSPlatform("Android")]
        [Pure, DllImport(MEDIAPIPE_LIBRARY)]
        public static extern IntPtr eglGetCurrentContext();
    }
}
