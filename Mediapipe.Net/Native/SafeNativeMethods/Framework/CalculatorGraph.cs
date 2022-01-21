// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace Mediapipe.Net.Native
{
    internal partial class SafeNativeMethods : NativeMethods
    {
#pragma warning disable CA2101
        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool mp_CalculatorGraph__HasError(IntPtr graph);

        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true, CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool mp_CalculatorGraph__HasInputStream__PKc(IntPtr graph, string name);

        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool mp_CalculatorGraph__GraphInputStreamsClosed(IntPtr graph);

        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool mp_CalculatorGraph__IsNodeThrottled__i(IntPtr graph, int nodeId);

        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool mp_CalculatorGraph__UnthrottleSources(IntPtr graph);
#pragma warning restore CA2101
    }
}
