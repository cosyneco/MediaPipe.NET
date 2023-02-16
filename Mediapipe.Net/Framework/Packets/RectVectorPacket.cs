// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Collections.Generic;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Framework.Protobuf;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.Packets
{
    internal class RectVectorPacket : Packet<List<Rect>>
    {
        public RectVectorPacket() : base(true) { }
        public RectVectorPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }
        public RectVectorPacket? At(Timestamp timestamp) => At<RectVectorPacket>(timestamp);

        public override List<Rect> Get()
        {
            UnsafeNativeMethods.mp_Packet__GetRectVector(MpPtr, out var serializedProtoVector).Assert();
            GC.KeepAlive(this);

            var rect = serializedProtoVector.Deserialize(Rect.Parser);
            serializedProtoVector.Dispose();

            return rect;
        }

        public override StatusOr<List<Rect>> Consume()
        {
            throw new NotSupportedException();
        }

        public override Status ValidateAsType()
        {
            throw new NotImplementedException();
        }
    }
    {
    }
}
