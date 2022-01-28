// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.Packet
{
    public unsafe class FaceGeometryPacket : Packet<FaceGeometry.FaceGeometry>
    {
        public FaceGeometryPacket() : base() { }
        public FaceGeometryPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public override FaceGeometry.FaceGeometry Get()
        {
            UnsafeNativeMethods.mp_Packet__GetFaceGeometry(MpPtr, out var serializedProto).Assert();
            GC.KeepAlive(this);

            var geometry = serializedProto.Deserialize(FaceGeometry.FaceGeometry.Parser);
            serializedProto.Dispose();

            return geometry;
        }

        public override StatusOr<FaceGeometry.FaceGeometry> Consume() => throw new NotSupportedException();
    }
}
