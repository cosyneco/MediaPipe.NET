// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using Mediapipe.PInvoke;
using Mediapipe.PInvoke.Native;
using System;
using System.Runtime.InteropServices;

namespace Mediapipe.PInvoke.Native
{
  internal static partial class UnsafeNativeMethods
  {
    #region OutputStreamPoller
    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern void mp_OutputStreamPoller__delete(IntPtr poller);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode mp_OutputStreamPoller__Reset(IntPtr poller);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode mp_OutputStreamPoller__Next_Ppacket(IntPtr poller, IntPtr packet, out bool result);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode mp_OutputStreamPoller__SetMaxQueueSize(IntPtr poller, int queueSize);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode mp_OutputStreamPoller__QueueSize(IntPtr poller, out int queueSize);
        #endregion

        #region StatusOrPoller
        [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
        public static extern void mp_StatusOrPoller__delete(IntPtr statusOrPoller);

        [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
        public static extern MpReturnCode mp_StatusOrPoller__status(IntPtr statusOrPoller, out IntPtr status);

        [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
        public static extern MpReturnCode mp_StatusOrPoller__value(IntPtr statusOrPoller, out IntPtr poller);
        #endregion
    }
}
