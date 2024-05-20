// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace Mediapipe.PInvoke.Native
{
  internal static partial class SafeNativeMethods
  {
    [Pure, DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool mp_ValidatedGraphConfig__Initialized(IntPtr config);

    [Pure, DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern int mp_ValidatedGraphConfig__OutputStreamIndex__PKc(IntPtr config, string name);

    [Pure, DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern int mp_ValidatedGraphConfig__OutputSidePacketIndex__PKc(IntPtr config, string name);

    [Pure, DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern int mp_ValidatedGraphConfig__OutputStreamToNode__PKc(IntPtr config, string name);

    [Pure, DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool mp_ValidatedGraphConfig_IsReservedExecutorName(string name);

    [Pure, DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool mp_ValidatedGraphConfig__IsExternalSidePacket__PKc(IntPtr config, string name);
  }
}
