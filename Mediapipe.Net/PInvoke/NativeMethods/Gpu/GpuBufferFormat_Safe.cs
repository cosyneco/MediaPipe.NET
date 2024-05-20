// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using Mediapipe.Gpu;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace Mediapipe.PInvoke.Native
{
  internal static partial class SafeNativeMethods
  {
    [Pure, DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern ImageFormat.Types.Format mp__ImageFormatForGpuBufferFormat__ui(GpuBufferFormat format);

    [Pure, DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern ImageFormat.Types.Format mp__GpuBufferFormatForImageFormat__ui(ImageFormat.Types.Format format);
  }
}
