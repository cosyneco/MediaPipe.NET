// Copyright (c) 2023 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using System;
using System.Runtime.InteropServices;

namespace Mediapipe.PInvoke.Native
{
  internal static partial class UnsafeNativeMethods
  {
    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode mp_Packet__GetClassificationResult(IntPtr packet, out NativeClassificationResult value);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode mp_Packet__GetClassificationsVector(IntPtr packet, out NativeClassificationResult value);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern void mp_tasks_c_components_containers_CppCloseClassificationResult(NativeClassificationResult data);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode mp_Packet__GetClassificationResultVector(IntPtr packet, out NativeClassificationResultArray value);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern void mp_api_ClassificationResultArray__delete(NativeClassificationResultArray data);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode mp_Packet__GetDetectionResult(IntPtr packet, out NativeDetectionResult value);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern void mp_tasks_c_components_containers_CppCloseDetectionResult(NativeDetectionResult data);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode mp_Packet__GetLandmarksVector(IntPtr packet, out NativeLandmarksArray value);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern void mp_api_LandmarksArray__delete(NativeLandmarksArray data);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern MpReturnCode mp_Packet__GetNormalizedLandmarksVector(IntPtr packet, out NativeNormalizedLandmarksArray value);

    [DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
    public static extern void mp_api_NormalizedLandmarksArray__delete(NativeNormalizedLandmarksArray data);
  }
}
