// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Framework.Protobuf;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.Packets
{
    public class RectPacket : Packet<Rect>
    {
        public RectPacket() : base(true) { }
        public RectPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }
        public RectPacket? At(Timestamp timestamp) => At<RectPacket>(timestamp);

        public override Rect Get()
        {
            UnsafeNativeMethods.mp_Packet__GetRect(MpPtr, out var serializedProto).Assert();
            GC.KeepAlive(this);

            var rect = serializedProto.Deserialize(Rect.Parser);
            serializedProto.Dispose();

            return rect;
        }

        public override StatusOr<Rect> Consume()
        {
            throw new NotSupportedException();
        }

        public override Status ValidateAsType()
        {
            throw new NotImplementedException();
        }
    }
}
