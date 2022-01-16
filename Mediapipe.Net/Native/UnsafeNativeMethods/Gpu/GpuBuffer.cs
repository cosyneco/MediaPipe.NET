// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace Mediapipe.Net.Native
{
    internal partial class UnsafeNativeMethods : NativeMethods
    {
        [SupportedOSPlatform("Linux"), SupportedOSPlatform("Android")]
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_GpuBuffer__PSgtb(IntPtr glTextureBuffer, out IntPtr gpuBuffer);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void mp_GpuBuffer__delete(IntPtr gpuBuffer);

        #region StatusOr
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void mp_StatusOrGpuBuffer__delete(IntPtr statusOrGpuBuffer);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_StatusOrGpuBuffer__status(IntPtr statusOrGpuBuffer, out IntPtr status);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_StatusOrGpuBuffer__value(IntPtr statusOrGpuBuffer, out IntPtr gpuBuffer);
        #endregion

        #region Packet
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp__MakeGpuBufferPacket__Rgb(IntPtr gpuBuffer, out IntPtr packet);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp__MakeGpuBufferPacket_At__Rgb_Rts(IntPtr gpuBuffer, IntPtr timestamp, out IntPtr packet);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_Packet__ConsumeGpuBuffer(IntPtr packet, out IntPtr statusOrGpuBuffer);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_Packet__GetGpuBuffer(IntPtr packet, out IntPtr gpuBuffer);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_Packet__ValidateAsGpuBuffer(IntPtr packet, out IntPtr status);
        #endregion
    }
}
