// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Runtime.Versioning;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Gpu
{
    [SupportedOSPlatform("Linux"), SupportedOSPlatform("Android")]
    public class Egl
    {
        public static IntPtr GetCurrentContext() => SafeNativeMethods.eglGetCurrentContext();
    }
}
