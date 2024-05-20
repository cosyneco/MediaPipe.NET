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
    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool mp_Packet__IsEmpty(IntPtr packet);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern long mp_Packet__TimestampMicroseconds(IntPtr packet);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern void mp_PacketMap__clear(IntPtr packetMap);

    [Pure, DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern int mp_PacketMap__size(IntPtr packetMap);
  }
}
