// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using Mediapipe.External;
using Mediapipe.PInvoke.Native;
using System;
using System.Runtime.InteropServices;

namespace Mediapipe.PInvoke.Native
{
  internal static partial class UnsafeNativeMethods
  {
    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode mp__MakeRectPacket__PKc_i(byte[] serializedData, int size, out IntPtr packet_out);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode mp__MakeRectPacket_At__PKc_i_Rt(byte[] serializedData, int size, IntPtr timestamp, out IntPtr packet_out);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode mp_Packet__GetRect(IntPtr packet, out SerializedProto serializedProto);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode mp_Packet__GetRectVector(IntPtr packet, out SerializedProtoVector serializedProtoVector);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode mp_Packet__ValidateAsRect(IntPtr packet, out IntPtr status);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode mp__MakeNormalizedRectPacket__PKc_i(byte[] serializedData, int size, out IntPtr packet_out);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode mp__MakeNormalizedRectPacket_At__PKc_i_Rt(byte[] serializedData, int size, IntPtr timestamp, out IntPtr packet_out);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode mp_Packet__GetNormalizedRect(IntPtr packet, out SerializedProto serializedProto);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode mp_Packet__GetNormalizedRectVector(IntPtr packet, out SerializedProtoVector serializedProtoVector);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode mp_Packet__ValidateAsNormalizedRect(IntPtr packet, out IntPtr status);
  }
}
