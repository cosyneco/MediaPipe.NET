// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Runtime.InteropServices;
using Mediapipe.Net.Framework.Format;

namespace Mediapipe.Net.Native
{
    internal partial class UnsafeNativeMethods : NativeMethods
    {
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_ImageFrame__(out IntPtr imageFrame);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_ImageFrame__ui_i_i_ui(
            ImageFormat format, int width, int height, uint alignmentBoundary, out IntPtr imageFrame);

        // TODO: Make it be a member of ImageFrame
        public delegate void Deleter(IntPtr ptr);
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_ImageFrame__ui_i_i_i_Pui8_PF(
            ImageFormat format, int width, int height, int widthStep, IntPtr pixelData,
            [MarshalAs(UnmanagedType.FunctionPtr)] Deleter deleter, out IntPtr imageFrame);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void mp_ImageFrame__delete(IntPtr imageFrame);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_ImageFrame__SetToZero(IntPtr imageFrame);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_ImageFrame__SetAlignmentPaddingAreas(IntPtr imageFrame);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_ImageFrame__CopyToBuffer__Pui8_i(IntPtr imageFrame, IntPtr buffer, int bufferSize);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_ImageFrame__CopyToBuffer__Pui16_i(IntPtr imageFrame, IntPtr buffer, int bufferSize);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_ImageFrame__CopyToBuffer__Pf_i(IntPtr imageFrame, IntPtr buffer, int bufferSize);

        #region StatusOr
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void mp_StatusOrImageFrame__delete(IntPtr statusOrImageFrame);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_StatusOrImageFrame__status(IntPtr statusOrImageFrame, out IntPtr status);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_StatusOrImageFrame__value(IntPtr statusOrImageFrame, out IntPtr imageFrame);
        #endregion

        #region Packet
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp__MakeImageFramePacket__Pif(IntPtr imageFrame, out IntPtr packet);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp__MakeImageFramePacket_At__Pif_Rt(IntPtr imageFrame, IntPtr timestamp, out IntPtr packet);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_Packet__ConsumeImageFrame(IntPtr packet, out IntPtr statusOrImageFrame);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_Packet__GetImageFrame(IntPtr packet, out IntPtr imageFrame);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_Packet__ValidateAsImageFrame(IntPtr packet, out IntPtr status);
        #endregion
    }
}
