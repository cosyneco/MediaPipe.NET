// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Mediapipe.Net.Native;
using Google.Protobuf;

namespace Mediapipe.Net.External
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct SerializedProtoVector
    {
        public IntPtr Data;
        public int Size;

        // TODO: This is looking just as sus as SerializedProto.Dispose().
        // Should be investigated in the same way.
        public void Dispose() => UnsafeNativeMethods.mp_api_SerializedProtoArray__delete(Data);

        public List<T> Deserialize<T>(MessageParser<T> parser) where T : IMessage<T>
        {
            var protos = new List<T>(Size);

            unsafe
            {
                var protoPtr = (SerializedProto*)Data;

                for (var i = 0; i < Size; i++)
                {
                    var serializedProto = Marshal.PtrToStructure<SerializedProto>((IntPtr)protoPtr++);
                    protos.Add(serializedProto.Deserialize(parser));
                }
            }

            return protos;
        }
    }
}