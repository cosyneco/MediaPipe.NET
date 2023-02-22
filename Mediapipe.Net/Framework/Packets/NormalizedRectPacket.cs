// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Framework.Protobuf;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.Packets
{
    public class NormalizedRectPacket : Packet<NormalizedRect>
    {
        public NormalizedRectPacket() : base() { }
        public NormalizedRectPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public NormalizedRectPacket? At(Timestamp timestamp) => At<NormalizedRectPacket>(timestamp);
        public override NormalizedRect Get()
        {
            UnsafeNativeMethods.mp_Packet__GetNormalizedRect(MpPtr, out var serializedProto).Assert();
            GC.KeepAlive(this);

            var normalizedRect = serializedProto.Deserialize(NormalizedRect.Parser);
            serializedProto.Dispose();

            return normalizedRect;
        }

        public override StatusOr<NormalizedRect> Consume()
        {
            throw new NotSupportedException();
        }

        public override Status ValidateAsType()
        {
            throw new NotSupportedException();
        }
    }
}
