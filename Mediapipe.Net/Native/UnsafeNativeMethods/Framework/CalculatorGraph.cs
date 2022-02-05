// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Runtime.InteropServices;
using Mediapipe.Net.External;
using Mediapipe.Net.Framework;

namespace Mediapipe.Net.Native
{
    internal unsafe partial class UnsafeNativeMethods : NativeMethods
    {
#pragma warning disable CA2101
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_CalculatorGraph__(out IntPtr graph);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true, CharSet = CharSet.Ansi)]
        public static extern MpReturnCode mp_CalculatorGraph__PKc(string textFormatConfig, out IntPtr graph);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_CalculatorGraph__PKc_i(byte[] serializedConfig, int size, out IntPtr graph);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void mp_CalculatorGraph__delete(IntPtr graph);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_CalculatorGraph__Initialize__PKc_i(IntPtr graph, byte[] serializedConfig, int size, out IntPtr status);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_CalculatorGraph__Initialize__PKc_i_Rsp(
            IntPtr graph, byte[] serializedConfig, int size, IntPtr sidePackets, out IntPtr status);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_CalculatorGraph__Config(IntPtr graph, out SerializedProto serializedProto);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true, CharSet = CharSet.Ansi)]
        public static extern MpReturnCode mp_CalculatorGraph__ObserveOutputStream__PKc_PF_b(IntPtr graph, string streamName,
            CalculatorGraph.NativePacketCallback packetCallback,
            bool observeTimestampBounds, out IntPtr status);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true, CharSet = CharSet.Ansi)]
        public static extern MpReturnCode mp_CalculatorGraph__AddOutputStreamPoller__PKc_b(IntPtr graph, string streamName,
            bool observeTimestampBounds, out IntPtr statusOrPoller);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_CalculatorGraph__Run__Rsp(IntPtr graph, IntPtr sidePackets, out IntPtr status);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_CalculatorGraph__StartRun__Rsp(IntPtr graph, IntPtr sidePackets, out IntPtr status);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_CalculatorGraph__WaitUntilIdle(IntPtr graph, out IntPtr status);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_CalculatorGraph__WaitUntilDone(IntPtr graph, out IntPtr status);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true, CharSet = CharSet.Ansi)]
        public static extern MpReturnCode mp_CalculatorGraph__AddPacketToInputStream__PKc_Ppacket(
            IntPtr graph, string streamName, IntPtr packet, out IntPtr status);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true, CharSet = CharSet.Ansi)]
        public static extern MpReturnCode mp_CalculatorGraph__SetInputStreamMaxQueueSize__PKc_i(
            IntPtr graph, string streamName, int maxQueueSize, out IntPtr status);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true, CharSet = CharSet.Ansi)]
        public static extern MpReturnCode mp_CalculatorGraph__CloseInputStream__PKc(IntPtr graph, string streamName, out IntPtr status);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_CalculatorGraph__CloseAllPacketSources(IntPtr graph, out IntPtr status);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_CalculatorGraph__Cancel(IntPtr graph);

        #region GPU
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_CalculatorGraph__GetGpuResources(IntPtr graph, out IntPtr gpuResources);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_CalculatorGraph__SetGpuResources__SPgpu(IntPtr graph, IntPtr gpuResources, out IntPtr status);
        #endregion
#pragma warning restore CA2101
    }
}
