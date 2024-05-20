// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using Mediapipe.External;
using Mediapipe.PInvoke.Native;
using System.Runtime.InteropServices;

namespace Mediapipe.PInvoke.Native
{
  internal static partial class UnsafeNativeMethods
  {
    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode google_InitGoogleLogging__PKc(string name);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode google_ShutdownGoogleLogging();

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern void glog_FLAGS_logtostderr([MarshalAs(UnmanagedType.I1)] bool value);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern void glog_FLAGS_stderrthreshold(int threshold);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern void glog_FLAGS_minloglevel(int level);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern void glog_FLAGS_log_dir(string dir);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern void glog_FLAGS_v(int v);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern void glog_LOG_INFO__PKc(string str);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern void glog_LOG_WARNING__PKc(string str);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern void glog_LOG_ERROR__PKc(string str);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern void glog_LOG_FATAL__PKc(string str);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern void google_FlushLogFiles(Glog.Severity severity);
  }
}
