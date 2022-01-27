// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Collections.Generic;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Framework.Protobuf;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.Packet
{
    public unsafe class RectVectorPacket : Packet<List<Rect>>
    {
        public RectVectorPacket() : base() { }
        public RectVectorPacket(void* ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public override List<Rect> Get()
        {
            UnsafeNativeMethods.mp_Packet__GetRectVector(MpPtr, out var serializedProtoVector).Assert();
            GC.KeepAlive(this);

            var rects = serializedProtoVector.Deserialize(Rect.Parser);
            serializedProtoVector.Dispose();

            return rects;
        }

        public override StatusOr<List<Rect>> Consume() => throw new NotSupportedException();
    }
}
