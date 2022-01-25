// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using Mediapipe.Net.Framework.Format;

namespace Mediapipe.Net.Native
{
    internal partial class SafeNativeMethods : NativeMethods
    {
        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern bool mp_ImageFrame__IsEmpty(IntPtr imageFrame);

        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern bool mp_ImageFrame__IsContiguous(IntPtr imageFrame);

        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_ImageFrame__IsAligned__ui(
            IntPtr imageFrame, uint alignmentBoundary, out bool value);

        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern ImageFormat mp_ImageFrame__Format(IntPtr imageFrame);

        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern int mp_ImageFrame__Width(IntPtr imageFrame);

        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern int mp_ImageFrame__Height(IntPtr imageFrame);

        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_ImageFrame__ChannelSize(IntPtr imageFrame, out int value);

        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_ImageFrame__NumberOfChannels(IntPtr imageFrame, out int value);

        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_ImageFrame__ByteDepth(IntPtr imageFrame, out int value);

        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern int mp_ImageFrame__WidthStep(IntPtr imageFrame);

        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern IntPtr mp_ImageFrame__MutablePixelData(IntPtr imageFrame);

        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern int mp_ImageFrame__PixelDataSize(IntPtr imageFrame);

        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_ImageFrame__PixelDataSizeStoredContiguously(IntPtr imageFrame, out int value);

        #region StatusOr
        [Pure, DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern bool mp_StatusOrImageFrame__ok(IntPtr statusOrImageFrame);
        #endregion
    }
}
