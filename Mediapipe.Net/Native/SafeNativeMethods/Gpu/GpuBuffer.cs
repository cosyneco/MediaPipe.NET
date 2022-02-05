// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Mediapipe.Net.Gpu;

namespace Mediapipe.Net.Native
{
    internal unsafe partial class SafeNativeMethods : NativeMethods
    {
        [SupportedOSPlatform("Linux"), SupportedOSPlatform("Android")]
        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern IntPtr mp_GpuBuffer__GetGlTextureBufferSharedPtr(IntPtr gpuBuffer);

        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern int mp_GpuBuffer__width(IntPtr gpuBuffer);

        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern int mp_GpuBuffer__height(IntPtr gpuBuffer);

        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern GpuBufferFormat mp_GpuBuffer__format(IntPtr gpuBuffer);

        #region StatusOr
        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern byte mp_StatusOrGpuBuffer__ok(IntPtr statusOrGpuBuffer);
        #endregion
    }
}
