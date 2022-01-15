// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Runtime.InteropServices;

namespace Mediapipe.Net.Native;

internal partial class UnsafeNativeMethods : NativeMethods
{
    [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
    public static extern MpReturnCode mp_Timestamp__l(long value, out IntPtr timestamp);

    [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
    public static extern void mp_Timestamp__delete(IntPtr timestamp);

    [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
    public static extern MpReturnCode mp_Timestamp__DebugString(IntPtr timestamp, out IntPtr str);

    [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
    public static extern MpReturnCode mp_Timestamp__NextAllowedInStream(IntPtr timestamp, out IntPtr nextTimestamp);

    [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
    public static extern MpReturnCode mp_Timestamp__PreviousAllowedInStream(IntPtr timestamp, out IntPtr prevTimestamp);

    [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
    public static extern MpReturnCode mp_Timestamp_FromSeconds__d(double seconds, out IntPtr timestamp);

    [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
    public static extern MpReturnCode mp_Timestamp_Unset(out IntPtr timestamp);

    [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
    public static extern MpReturnCode mp_Timestamp_Unstarted(out IntPtr timestamp);

    [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
    public static extern MpReturnCode mp_Timestamp_PreStream(out IntPtr timestamp);

    [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
    public static extern MpReturnCode mp_Timestamp_Min(out IntPtr timestamp);

    [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
    public static extern MpReturnCode mp_Timestamp_Max(out IntPtr timestamp);

    [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
    public static extern MpReturnCode mp_Timestamp_PostStream(out IntPtr timestamp);

    [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
    public static extern MpReturnCode mp_Timestamp_OneOverPostStream(out IntPtr timestamp);

    [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
    public static extern MpReturnCode mp_Timestamp_Done(out IntPtr timestamp);
}
