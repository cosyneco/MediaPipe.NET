// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Framework.Protobuf;
using Mediapipe.Net.Native;
using System;
using System.Collections.Generic;

namespace Mediapipe.Net.Framework.Packets
{
    public class DetectionVectorPacket : Packet<List<Detection>>
    {
        public DetectionVectorPacket() : base() { }
        public DetectionVectorPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public DetectionVectorPacket? At(Timestamp timestamp) => At<DetectionVectorPacket>(timestamp);
        public override List<Detection> Get()
        {
            UnsafeNativeMethods.mp_Packet__GetDetectionVector(MpPtr, out var serializedProtoVectorPtr).Assert();
            GC.KeepAlive(this);

            var detectionVector = serializedProtoVectorPtr.Deserialize(Detection.Parser);
            serializedProtoVectorPtr.Dispose();

            return detectionVector;
        }

        public override StatusOr<List<Detection>> Consume()
        {
            throw new NotSupportedException();
        }

        public override Status ValidateAsType()
        {
            throw new NotSupportedException();
        }
    }
}
