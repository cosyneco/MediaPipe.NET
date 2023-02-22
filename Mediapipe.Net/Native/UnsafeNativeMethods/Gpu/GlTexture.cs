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
        public static extern MpReturnCode mp_GlTexture__(out IntPtr glTexture);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void mp_GlTexture__delete(IntPtr glTexture);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_GlTexture__Release(IntPtr glTexture);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_GlTexture__GetGpuBufferFrame(IntPtr glTexture, out IntPtr gpuBuffer);
    }
}
