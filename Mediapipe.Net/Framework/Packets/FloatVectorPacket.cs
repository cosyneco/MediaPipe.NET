// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Mediapipe.Net.Framework.Packets
{
    [StructLayout(LayoutKind.Sequential)]
    internal readonly struct FloatVector : IDisposable
    {
        private readonly IntPtr data;
        private readonly int size;

        public List<float> Copy()
        {
            var dataList = new List<float>();
            unsafe
            {
                float* floatPtr = (float*)data;

                for (var i = 0; i < size; i++)
                {
                    dataList.Add(*floatPtr++);
                }
            }

            return dataList;
        }

        public void Dispose()
        {
            UnsafeNativeMethods.delete_array__Pf(data);
        }
    }

    public class FloatVectorPacket : Packet<List<float>>
    {

    }
}
