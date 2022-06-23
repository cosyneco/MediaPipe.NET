// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Collections.Generic;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Framework.Protobuf;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.OldPacket
{
    public unsafe class NormalizedLandmarkListVectorPacket : Packet<List<NormalizedLandmarkList>>
    {
        public NormalizedLandmarkListVectorPacket() : base() { }
        public NormalizedLandmarkListVectorPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public override List<NormalizedLandmarkList> Get()
        {
            UnsafeNativeMethods.mp_Packet__GetNormalizedLandmarkListVector(MpPtr, out var serializedProtoVector).Assert();
            GC.KeepAlive(this);

            var normalizedLandmarkLists = serializedProtoVector.Deserialize(NormalizedLandmarkList.Parser);
            serializedProtoVector.Dispose();

            return normalizedLandmarkLists;
        }

        public override StatusOr<List<NormalizedLandmarkList>> Consume() => throw new NotSupportedException();
    }
}
