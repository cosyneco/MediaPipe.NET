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
    public unsafe class NormalizedRectVectorPacket : Packet<List<NormalizedRect>>
    {
        public NormalizedRectVectorPacket() : base() { }
        public NormalizedRectVectorPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public override List<NormalizedRect> Get()
        {
            UnsafeNativeMethods.mp_Packet__GetNormalizedRectVector(MpPtr, out var serializedProtoVector).Assert();
            GC.KeepAlive(this);

            var normalizedRects = serializedProtoVector.Deserialize(NormalizedRect.Parser);
            serializedProtoVector.Dispose();

            return normalizedRects;
        }

        public override StatusOr<List<NormalizedRect>> Consume() => throw new NotSupportedException();
    }
}
