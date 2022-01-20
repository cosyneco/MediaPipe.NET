// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Mediapipe.Net.Gpu;

namespace Mediapipe.Net.Native
{
    internal partial class UnsafeNativeMethods : NativeMethods
    {
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_GlCalculatorHelper__(out IntPtr glCalculatorHelper);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void mp_GlCalculatorHelper__delete(IntPtr glCalculatorHelper);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_GlCalculatorHelper__InitializeForTest__Pgr(IntPtr glCalculatorHelper, IntPtr gpuResources);

        // TODO: Make it ba a member of GlCalculatorHelper
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_GlCalculatorHelper__RunInGlContext__PF(
            IntPtr glCalculatorHelper, [MarshalAs(UnmanagedType.FunctionPtr)] GlCalculatorHelper.NativeGlStatusFunction glFunc, out IntPtr status);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_GlCalculatorHelper__CreateSourceTexture__Rif(
            IntPtr glCalculatorHelper, IntPtr imageFrame, out IntPtr glTexture);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_GlCalculatorHelper__CreateSourceTexture__Rgb(
            IntPtr glCalculatorHelper, IntPtr gpuBuffer, out IntPtr glTexture);

        [SupportedOSPlatform("IOS")]
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_GlCalculatorHelper__CreateSourceTexture__Rgb_i(
            IntPtr glCalculatorHelper, IntPtr gpuBuffer, int plane, out IntPtr glTexture);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_GlCalculatorHelper__CreateDestinationTexture__i_i_ui(
            IntPtr glCalculatorHelper, int outputWidth, int outputHeight, GpuBufferFormat formatCode, out IntPtr glTexture);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_GlCalculatorHelper__CreateDestinationTexture__Rgb(
            IntPtr glCalculatorHelper, IntPtr gpuBuffer, out IntPtr glTexture);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_GlCalculatorHelper__BindFrameBuffer__Rtexture(IntPtr glCalculatorHelper, IntPtr glTexture);
    }
}
