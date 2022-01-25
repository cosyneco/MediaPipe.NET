// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Runtime.InteropServices;

namespace Mediapipe.Net.Native
{
    internal partial class UnsafeNativeMethods : NativeMethods
    {
        [DllImport(MEDIAPIPE_LIBRARY)]
        public static extern void glFlush();

        [DllImport(MEDIAPIPE_LIBRARY)]
        public static extern void glReadPixels(int x, int y, int width, int height, uint glFormat, uint glType, void* pixels);
    }
}
