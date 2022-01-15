// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace Mediapipe.Net.Native;

internal partial class SafeNativeMethods : NativeMethods
{
    [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool mp_StatusOrPoller__ok(IntPtr statusOrPoller);
}
