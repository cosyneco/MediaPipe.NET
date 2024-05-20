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
    public static extern IntPtr mp_SharedGpuResources__get(IntPtr gpuResources);
  }
}
