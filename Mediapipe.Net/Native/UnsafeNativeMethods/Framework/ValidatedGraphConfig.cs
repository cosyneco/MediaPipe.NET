// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Runtime.InteropServices;
using Mediapipe.Net.External;
using Mediapipe.Net.Framework.ValidatedGraphConfig;

namespace Mediapipe.Net.Native
{
    internal unsafe partial class UnsafeNativeMethods
    {
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_ValidatedGraphConfig__(out IntPtr config);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void mp_ValidatedGraphConfig__delete(IntPtr config);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_ValidatedGraphConfig__Initialize__Rcgc(IntPtr config, byte[] serializedConfig, int size, out IntPtr status);

#pragma warning disable CA2101
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true, CharSet = CharSet.Ansi)]
        public static extern MpReturnCode mp_ValidatedGraphConfig__Initialize__PKc(IntPtr config, string graphType, out IntPtr status);
#pragma warning restore CA2101

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_ValidatedGraphConfig__ValidateRequiredSidePackets__Rsp(IntPtr config, IntPtr sidePackets, out IntPtr status);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_ValidatedGraphConfig__Config(IntPtr config, out SerializedProto serializedProto);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_ValidatedGraphConfig__InputStreamInfos(IntPtr config, out EdgeInfoVector edgeInfoVector);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_ValidatedGraphConfig__OutputStreamInfos(IntPtr config, out EdgeInfoVector edgeInfoVector);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_ValidatedGraphConfig__InputSidePacketInfos(IntPtr config, out EdgeInfoVector edgeInfoVector);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_ValidatedGraphConfig__OutputSidePacketInfos(IntPtr config, out EdgeInfoVector edgeInfoVector);

#pragma warning disable CA2101
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true, CharSet = CharSet.Ansi)]
        public static extern MpReturnCode mp_ValidatedGraphConfig__RegisteredSidePacketTypeName(IntPtr config, string name, out IntPtr statusOrString);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true, CharSet = CharSet.Ansi)]
        public static extern MpReturnCode mp_ValidatedGraphConfig__RegisteredStreamTypeName(IntPtr config, string name, out IntPtr statusOrString);
#pragma warning restore CA2101

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_ValidatedGraphConfig__Package(IntPtr config, out IntPtr str);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void mp_api_EdgeInfoArray__delete(IntPtr data, int size);
    }
}
