// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using System.Runtime.InteropServices;

namespace Mediapipe.PInvoke.Native
{
  internal static partial class SafeNativeMethods
  {
    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern void mp__SetCustomGlobalResourceProvider__P(
        [MarshalAs(UnmanagedType.FunctionPtr)] ResourceManager.NativeResourceProvider provider);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern void mp__SetCustomGlobalPathResolver__P(
        [MarshalAs(UnmanagedType.FunctionPtr)] ResourceManager.PathResolver resolver);
  }
}
