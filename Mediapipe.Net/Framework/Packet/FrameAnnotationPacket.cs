// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Framework.Packet;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Framework.Protobuf;
using Mediapipe.Net.Native;

namespace Mediapipe
{
    public class FrameAnnotationPacket : Packet<FrameAnnotation>
    {
        public FrameAnnotationPacket() : base() { }
        public FrameAnnotationPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public override FrameAnnotation Get()
        {
            UnsafeNativeMethods.mp_Packet__GetFrameAnnotation(MpPtr, out var serializedProto).Assert();
            GC.KeepAlive(this);

            var frameAnnotation = serializedProto.Deserialize(FrameAnnotation.Parser);
            serializedProto.Dispose();

            return frameAnnotation;
        }

        public override StatusOr<FrameAnnotation> Consume() => throw new NotSupportedException();
    }
}
