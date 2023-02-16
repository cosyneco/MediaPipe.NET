// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace Mediapipe.Net.Native
{
    internal unsafe partial class SafeNativeMethods
    {
        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern bool mp_ValidatedGraphConfig__Initialized(IntPtr config);

#pragma warning disable CA2101
        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true, CharSet = CharSet.Ansi)]
        public static extern int mp_ValidatedGraphConfig__OutputStreamIndex__PKc(IntPtr config, string name);

        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true, CharSet = CharSet.Ansi)]
        public static extern int mp_ValidatedGraphConfig__OutputSidePacketIndex__PKc(IntPtr config, string name);

        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true, CharSet = CharSet.Ansi)]
        public static extern int mp_ValidatedGraphConfig__OutputStreamToNode__PKc(IntPtr config, string name);


        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true, CharSet = CharSet.Ansi)]
        public static extern bool mp_ValidatedGraphConfig_IsReservedExecutorName(string name);

        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true, CharSet = CharSet.Ansi)]
        public static extern bool mp_ValidatedGraphConfig__IsExternalSidePacket__PKc(IntPtr config, string name);
#pragma warning restore CA2101
    }
}
