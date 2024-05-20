// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using System;
using System.Runtime.InteropServices;

namespace Mediapipe.PInvoke.Native
{
  internal static partial class UnsafeNativeMethods
  {
    #region GlContext
    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern void mp_SharedGlContext__delete(IntPtr sharedGlContext);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern void mp_SharedGlContext__reset(IntPtr sharedGlContext);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode mp_GlContext_GetCurrent(out IntPtr sharedGlContext);
    #endregion

    #region GlSyncToken
    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern void mp_GlSyncToken__delete(IntPtr glSyncToken);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern void mp_GlSyncToken__reset(IntPtr glSyncToken);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode mp_GlSyncPoint__Wait(IntPtr glSyncPoint);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode mp_GlSyncPoint__WaitOnGpu(IntPtr glSyncPoint);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode mp_GlSyncPoint__IsReady(IntPtr glSyncPoint, out bool value);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode mp_GlSyncPoint__GetContext(IntPtr glSyncPoint, out IntPtr sharedGlContext);
    #endregion
  }
}
