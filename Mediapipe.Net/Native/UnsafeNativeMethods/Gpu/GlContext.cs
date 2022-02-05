// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace Mediapipe.Net.Native
{
    internal unsafe partial class UnsafeNativeMethods : NativeMethods
    {
        #region GlContext
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void mp_SharedGlContext__delete(IntPtr sharedGlContext);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void mp_SharedGlContext__reset(IntPtr sharedGlContext);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_GlContext_GetCurrent(out IntPtr sharedGlContext);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_GlContext_Create__P_b(bool createThread, out IntPtr statusOrSharedGlContext);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_GlContext_Create__Rgc_b(
            IntPtr shareContext, bool createThread, out IntPtr statusOrSharedGlContext);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_GlContext_Create__ui_b(
            uint shareContext, bool createThread, out IntPtr statusOrSharedGlContext);

        [SupportedOSPlatform("IOS")]
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_GlContext_Create__Pes_b(
            IntPtr sharegroup, bool createThread, out IntPtr statusOrSharedGlContext);
        #endregion

        #region GlSyncToken
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void mp_GlSyncToken__delete(IntPtr glSyncToken);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void mp_GlSyncToken__reset(IntPtr glSyncToken);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_GlSyncPoint__Wait(IntPtr glSyncPoint);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_GlSyncPoint__WaitOnGpu(IntPtr glSyncPoint);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_GlSyncPoint__IsReady(IntPtr glSyncPoint, out bool value);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_GlSyncPoint__GetContext(IntPtr glSyncPoint, out IntPtr sharedGlContext);
        #endregion
    }
}
