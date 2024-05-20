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
    public static extern bool mp_CalculatorGraph__HasError(IntPtr graph);

    [Pure, DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool mp_CalculatorGraph__HasInputStream__PKc(IntPtr graph, string name);

    [Pure, DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool mp_CalculatorGraph__GraphInputStreamsClosed(IntPtr graph);

    [Pure, DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool mp_CalculatorGraph__IsNodeThrottled__i(IntPtr graph, int nodeId);

    [Pure, DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool mp_CalculatorGraph__UnthrottleSources(IntPtr graph);
  }
}
