// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Framework.Protobuf;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.Packets
{
    public class FrameAnnotationPacket : Packet<FrameAnnotation>
    {
        public FrameAnnotationPacket() : base() { }
        public FrameAnnotationPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public FrameAnnotationPacket? At(Timestamp timestamp) => At<FrameAnnotationPacket>(timestamp);
        public override FrameAnnotation Get()
        {
            UnsafeNativeMethods.mp_Packet__GetFrameAnnotation(MpPtr, out var serializedProtoVectorPtr).Assert();
            GC.KeepAlive(this);

            var frameAnnotation = serializedProtoVectorPtr.Deserialize(FrameAnnotation.Parser);
            serializedProtoVectorPtr.Dispose();

            return frameAnnotation;
        }

        public override StatusOr<FrameAnnotation> Consume()
        {
            throw new NotSupportedException();
        }

        public override Status ValidateAsType()
        {
            throw new NotSupportedException();
        }
    }
}
