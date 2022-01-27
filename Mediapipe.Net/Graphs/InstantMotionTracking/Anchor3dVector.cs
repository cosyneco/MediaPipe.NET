// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Graphs.InstantMotionTracking
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct Anchor3dVector : IDisposable
    {
        public void* Data;
        public int Size;

        public void Dispose() => UnsafeNativeMethods.mp_Anchor3dArray__delete(Data);

        public List<Anchor3d> ToList()
        {
            var anchors = new List<Anchor3d>(Size);

            unsafe
            {
                var anchorPtr = (Anchor3d*)Data;

                for (var i = 0; i < Size; i++)
                    anchors.Add(Marshal.PtrToStructure<Anchor3d>((void*)anchorPtr++));
            }

            return anchors;
        }
    }
}
