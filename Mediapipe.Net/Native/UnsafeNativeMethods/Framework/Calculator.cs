// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System.Runtime.InteropServices;
using Mediapipe.Net.External;

namespace Mediapipe.Net.Native
{
    internal partial class UnsafeNativeMethods : NativeMethods
    {
#pragma warning disable CA2101
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true, CharSet = CharSet.Ansi)]
        public static extern bool mp_api__ConvertFromCalculatorGraphConfigTextFormat(string configText, out SerializedProto serializedProto);
#pragma warning restore CA2101
    }
}
