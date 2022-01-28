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
    public unsafe class LandmarkListVectorPacket : Packet<List<LandmarkList>>
    {
        public LandmarkListVectorPacket() : base() { }
        public LandmarkListVectorPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public override List<LandmarkList> Get()
        {
            UnsafeNativeMethods.mp_Packet__GetLandmarkListVector(MpPtr, out var serializedProtoVector).Assert();
            GC.KeepAlive(this);

            var landmarkLists = serializedProtoVector.Deserialize(LandmarkList.Parser);
            serializedProtoVector.Dispose();

            return landmarkLists;
        }

        public override StatusOr<List<LandmarkList>> Consume() => throw new NotSupportedException();
    }
}
