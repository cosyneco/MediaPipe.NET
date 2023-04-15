// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.Packets
{
    public class FaceGeometryPacket : Packet<FaceGeometry.FaceGeometry>
    {
        public FaceGeometryPacket() : base(true) { }
        public FaceGeometryPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public FaceGeometryPacket? At(Timestamp timestamp) => At<FaceGeometryPacket>(timestamp);

        public override FaceGeometry.FaceGeometry Get()
        {
            UnsafeNativeMethods.mp_Packet__GetFaceGeometry(MpPtr, out var serializedProtoVectorPtr).Assert();
            GC.KeepAlive(this);

            var geometry = serializedProtoVectorPtr.Deserialize(FaceGeometry.FaceGeometry.Parser);
            serializedProtoVectorPtr.Dispose();

            return geometry;
        }

        public override StatusOr<FaceGeometry.FaceGeometry> Consume()
        {
            throw new NotSupportedException();
        }

        public override Status ValidateAsType()
        {
            throw new NotSupportedException();
        }
    }
}
