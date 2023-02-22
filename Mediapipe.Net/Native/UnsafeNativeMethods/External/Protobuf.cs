// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Runtime.InteropServices;
using Mediapipe.Net.External;

namespace Mediapipe.Net.Native
{
    internal unsafe partial class UnsafeNativeMethods : NativeMethods
    {
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode google_protobuf__SetLogHandler__PF(Protobuf.LogHandler logHandler);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode google_protobuf__ResetLogHandler();

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern void mp_api_SerializedProtoArray__delete(IntPtr serializedProtoVectorData, int size);

        #region MessageProto
        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_Packet__GetClassificationList(IntPtr packet, out SerializedProto serializedProto);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_Packet__GetClassificationListVector(IntPtr packet, out SerializedProtoVector serializedProtoVector);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_Packet__GetDetection(IntPtr packet, out SerializedProto serializedProto);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_Packet__GetDetectionVector(IntPtr packet, out SerializedProtoVector serializedProtoVector);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_Packet__GetFaceGeometry(IntPtr packet, out SerializedProto serializedProto);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_Packet__GetFaceGeometryVector(IntPtr packet, out SerializedProtoVector serializedProtoVector);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_Packet__GetFrameAnnotation(IntPtr packet, out SerializedProto serializedProto);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_Packet__GetLandmarkList(IntPtr packet, out SerializedProto serializedProto);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_Packet__GetLandmarkListVector(IntPtr packet, out SerializedProtoVector serializedProtoVector);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_Packet__GetNormalizedLandmarkList(IntPtr packet, out SerializedProto serializedProto);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_Packet__GetNormalizedLandmarkListVector(IntPtr packet, out SerializedProtoVector serializedProtoVector);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_Packet__GetNormalizedRect(IntPtr packet, out SerializedProto serializedProto);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_Packet__GetNormalizedRectVector(IntPtr packet, out SerializedProtoVector serializedProtoVector);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_Packet__GetRect(IntPtr packet, out SerializedProto serializedProto);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_Packet__GetRectVector(IntPtr packet, out SerializedProtoVector serializedProtoVector);

        [DllImport(MEDIAPIPE_LIBRARY, ExactSpelling = true)]
        public static extern MpReturnCode mp_Packet__GetTimedModelMatrixProtoList(IntPtr packet, out SerializedProto serializedProto);
        #endregion
    }
}
