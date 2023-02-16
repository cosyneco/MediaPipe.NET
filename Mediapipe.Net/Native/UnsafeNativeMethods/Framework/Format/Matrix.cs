// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Runtime.InteropServices;
using Mediapipe.Net.External;

namespace Mediapipe.Net.Native
{
    internal unsafe partial class UnsafeNativeMethods : NativeMethods
    {
        #region Packet

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp__MakeMatrixPacket__PKc_i(byte[] serializedMatrixData, int size, out IntPtr packet_out);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp__MakeMatrixPacket_At__PKc_i_Rt(byte[] serializedMatrixData, int size, IntPtr timestamp, out IntPtr packet_out);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_Packet__ValidateAsMatrix(IntPtr packet, out IntPtr status);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_Packet__GetMatrix(IntPtr packet, out SerializedProto serializedProto);

        #endregion
    }
}
