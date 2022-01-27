// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Collections.Generic;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.Packet
{
    public unsafe class FaceGeometryVectorPacket : Packet<List<FaceGeometry.FaceGeometry>>
    {
        public FaceGeometryVectorPacket() : base() { }
        public FaceGeometryVectorPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public override List<FaceGeometry.FaceGeometry> Get()
        {
            UnsafeNativeMethods.mp_Packet__GetFaceGeometryVector(MpPtr, out var serializedProtoVector).Assert();
            GC.KeepAlive(this);

            var geometries = serializedProtoVector.Deserialize(FaceGeometry.FaceGeometry.Parser);
            serializedProtoVector.Dispose();

            return geometries;
        }

        public override StatusOr<List<FaceGeometry.FaceGeometry>> Consume() => throw new NotSupportedException();
    }
}
