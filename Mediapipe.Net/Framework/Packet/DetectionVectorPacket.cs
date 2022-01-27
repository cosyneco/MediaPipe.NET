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
    public unsafe class DetectionVectorPacket : Packet<List<Detection>>
    {
        public DetectionVectorPacket() : base() { }
        public DetectionVectorPacket(void* ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public override List<Detection> Get()
        {
            UnsafeNativeMethods.mp_Packet__GetDetectionVector(MpPtr, out var serializedProtoVector).Assert();
            GC.KeepAlive(this);

            var detections = serializedProtoVector.Deserialize(Detection.Parser);
            serializedProtoVector.Dispose();

            return detections;
        }

        public override StatusOr<List<Detection>> Consume() => throw new NotSupportedException();
    }
}
