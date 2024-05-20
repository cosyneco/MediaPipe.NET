// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using Mediapipe.External;
using System.Runtime.InteropServices;

namespace Mediapipe.PInvoke.Native
{
  internal unsafe partial class UnsafeNativeMethods
  {
#pragma warning disable CA2101

        [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool mp_api__ConvertFromCalculatorGraphConfigTextFormat(string configText, out SerializedProto serializedProto);
#pragma warning restore CA2101

    }
}
