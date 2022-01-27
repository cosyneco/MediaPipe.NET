// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Framework.Protobuf;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.Packet
{
    public unsafe class NormalizedLandmarkListPacket : Packet<NormalizedLandmarkList>
    {
        public NormalizedLandmarkListPacket() : base() { }
        public NormalizedLandmarkListPacket(void* ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public override NormalizedLandmarkList Get()
        {
            UnsafeNativeMethods.mp_Packet__GetNormalizedLandmarkList(MpPtr, out var serializedProto).Assert();
            GC.KeepAlive(this);

            var normalizedLandmarkList = serializedProto.Deserialize(NormalizedLandmarkList.Parser);
            serializedProto.Dispose();

            return normalizedLandmarkList;
        }

        public override StatusOr<NormalizedLandmarkList> Consume() => throw new NotSupportedException();
    }
}
