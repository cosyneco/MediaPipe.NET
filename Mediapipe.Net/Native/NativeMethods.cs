// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Runtime.InteropServices;

namespace Mediapipe.Net.Native
{
    /// <summary>
    /// Contains all the directly bound native methods from Mediapipe.
    /// </summary>
    public class NativeMethods
    {
        /// <summary>
        /// Name of the mediapipe shared library.
        /// </summary>
        internal const string MEDIAPIPE_LIBRARY = "mediapipe_c";

        static NativeMethods()
        {
            mp_api__SetFreeHGlobal(freeHGlobal);
        }

        private delegate void FreeHGlobalDelegate(IntPtr hglobal);

        private static void freeHGlobal(IntPtr hglobal)
        {
            Marshal.FreeHGlobal(hglobal);
        }

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        private static extern void mp_api__SetFreeHGlobal(FreeHGlobalDelegate freeHGlobal);
    }
}
