// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Runtime.InteropServices;
using Google.Protobuf;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.External
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct SerializedProto
    {
        public void* StrPtr;
        public int Length;

        // TODO: That Dispose() method is looking very sus...
        // Might wanna investigate if it's better as a child of Disposable.
        public void Dispose() => UnsafeNativeMethods.delete_array__PKc(StrPtr);

        public T Deserialize<T>(MessageParser<T> parser) where T : IMessage<T>
        {
            byte[] bytes = new byte[Length];
            Marshal.Copy(StrPtr, bytes, 0, bytes.Length);
            return parser.ParseFrom(bytes);
        }
    }
}
