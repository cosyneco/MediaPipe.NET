// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Framework.Protobuf;
using Mediapipe.Net.Native;
using System;

namespace Mediapipe.Net.Framework.Packets
{
    public class DetectionPacket : Packet<Detection>
    {
        public DetectionPacket() : base() { }
        public DetectionPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public DetectionPacket? At(Timestamp timestamp) => At<DetectionPacket>(timestamp);
        public override Detection Get()
        {
            UnsafeNativeMethods.mp_Packet__GetDetection(MpPtr, out var serializedProtoVectorPtr).Assert();
            GC.KeepAlive(this);

            var detection = serializedProtoVectorPtr.Deserialize(Detection.Parser);
            serializedProtoVectorPtr.Dispose();

            return detection;
        }

        public override StatusOr<Detection> Consume()
        {
            throw new NotSupportedException();
        }

        public override Status ValidateAsType()
        {
            throw new NotSupportedException();
        }
    }
}
