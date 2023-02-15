// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Native;
using System;
using System.Collections.Generic;

namespace Mediapipe.Net.Framework.Packets
{
    internal class FaceGeometryVectorPacket : Packet<List<FaceGeometry.FaceGeometry>>
    {
        public FaceGeometryVectorPacket() : base() { }
        public FaceGeometryVectorPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public FaceGeometryVectorPacket? At(Timestamp timestamp) => At<FaceGeometryVectorPacket>(timestamp);
        public override List<FaceGeometry.FaceGeometry> Get()
        {
            UnsafeNativeMethods.mp_Packet__GetFaceGeometryVector(MpPtr, out var serializedProtoVectorPtr).Assert();
            GC.KeepAlive(this);

            var faceGeometryVector = serializedProtoVectorPtr.Deserialize(FaceGeometry.FaceGeometry.Parser);
            serializedProtoVectorPtr.Dispose();

            return faceGeometryVector;
        }

        public override StatusOr<List<FaceGeometry.FaceGeometry>> Consume()
        {
            throw new NotSupportedException();
        }

        public override Status ValidateAsType()
        {
            throw new NotSupportedException();
        }
    }
}
