// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using Mediapipe.PInvoke;
using Mediapipe.PInvoke.Native;
using System;
using System.Runtime.InteropServices;

namespace Mediapipe.PInvoke.Native
{
  internal static partial class UnsafeNativeMethods
  {
    #region Packet
    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode mp_Packet__ValidateAsMatrix(IntPtr packet, out IntPtr status);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode mp_Packet__GetMpMatrix(IntPtr packet, out NativeMatrix matrix);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode mp__MakeColMajorMatrixPacket__Pf_i_i(float[] data, int rows, int cols, out IntPtr packet_out);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode mp__MakeColMajorMatrixPacket_At__Pf_i_i_ll(float[] data, int rows, int cols, long timestampMicrosec, out IntPtr packet_out);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern void mp_api_Matrix__delete(NativeMatrix matrix);
    #endregion
  }
}
