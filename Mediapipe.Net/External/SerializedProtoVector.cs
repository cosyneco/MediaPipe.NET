// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Google.Protobuf;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.External
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct SerializedProtoVector
    {
        public readonly IntPtr Data;
        public readonly int Size;

        // The array element freeing loop has been moved to MediaPipe.NET.Runtime.
        public void Dispose() => UnsafeNativeMethods.mp_api_SerializedProtoArray__delete(Data, Size);

        public List<T> Deserialize<T>(MessageParser<T> parser) where T : IMessage<T>
        {
            var protos = new List<T>(Size);
            var protoPtr = (SerializedProto*)Data;
            for (int i = 0; i < Size; i++)
            {
                var serializedProto = Marshal.PtrToStructure<SerializedProto>((IntPtr)protoPtr++);
                protos.Add(serializedProto.Deserialize(parser));
            }

            return protos;
        }
    }
}
