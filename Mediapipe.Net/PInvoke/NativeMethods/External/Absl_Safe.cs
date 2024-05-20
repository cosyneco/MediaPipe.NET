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
    public static extern bool absl_Status__ok(IntPtr status);

    [Pure, DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern int absl_Status__raw_code(IntPtr status);
  }
}
