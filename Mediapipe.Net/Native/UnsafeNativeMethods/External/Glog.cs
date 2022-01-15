// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System.Runtime.InteropServices;

namespace Mediapipe.Net.Native
{
    internal partial class UnsafeNativeMethods : NativeMethods
    {
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern MpReturnCode google_InitGoogleLogging__PKc(string name);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode google_ShutdownGoogleLogging();

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void glog_FLAGS_logtostderr([MarshalAs(UnmanagedType.I1)] bool value);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void glog_FLAGS_stderrthreshold(int threshold);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void glog_FLAGS_minloglevel(int level);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern void glog_FLAGS_log_dir(string dir);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void glog_FLAGS_v(int v);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern void glog_LOG_INFO__PKc(string str);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern void glog_LOG_WARNING__PKc(string str);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern void glog_LOG_ERROR__PKc(string str);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern void glog_LOG_FATAL__PKc(string str);

        // TODO: replace 'int' with 'Glog.Severity'
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void google_FlushLogFiles(int severity);
    }
}
