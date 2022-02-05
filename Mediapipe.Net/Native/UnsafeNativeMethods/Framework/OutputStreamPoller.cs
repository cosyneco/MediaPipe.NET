// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System.Runtime.InteropServices;

namespace Mediapipe.Net.Native
{
    internal unsafe partial class UnsafeNativeMethods : NativeMethods
    {
        #region OutputStreamPoller
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void mp_OutputStreamPoller__delete(IntPtr poller);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_OutputStreamPoller__Reset(IntPtr poller);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_OutputStreamPoller__Next_Ppacket(IntPtr poller, IntPtr packet, out bool result);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_OutputStreamPoller__SetMaxQueueSize(IntPtr poller, int queueSize);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_OutputStreamPoller__QueueSize(IntPtr poller, out int queueSize);
        #endregion

        #region StatusOrPoller
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void mp_StatusOrPoller__delete(IntPtr statusOrPoller);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_StatusOrPoller__status(IntPtr statusOrPoller, out IntPtr status);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_StatusOrPoller__value(IntPtr statusOrPoller, out IntPtr poller);
        #endregion
    }
}
