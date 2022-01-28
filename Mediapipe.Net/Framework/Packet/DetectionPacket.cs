// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Framework.Protobuf;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.Packet
{
    public unsafe class DetectionPacket : Packet<Detection>
    {
        public DetectionPacket() : base() { }
        public DetectionPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public override Detection Get()
        {
            UnsafeNativeMethods.mp_Packet__GetDetection(MpPtr, out var serializedProto).Assert();
            GC.KeepAlive(this);

            var detection = serializedProto.Deserialize(Detection.Parser);
            serializedProto.Dispose();

            return detection;
        }

        public override StatusOr<Detection> Consume() => throw new NotSupportedException();
    }
}
