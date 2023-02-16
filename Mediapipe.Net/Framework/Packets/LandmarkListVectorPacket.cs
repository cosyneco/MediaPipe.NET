// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Framework.Protobuf;
using Mediapipe.Net.Native;
using System;
using System.Collections.Generic;

namespace Mediapipe.Net.Framework.Packets
{
    internal class LandmarkListVectorPacket : Packet<List<LandmarkList>>
    {
        public LandmarkListVectorPacket() : base(true) { }
        public LandmarkListVectorPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }
        public LandmarkListVectorPacket? At(Timestamp timestamp) => At<LandmarkListVectorPacket>(timestamp);

        public override List<LandmarkList> Get()
        {
            UnsafeNativeMethods.mp_Packet__GetLandmarkListVector(MpPtr, out var serializedProtoVector).Assert();
            GC.KeepAlive(this);

            var landmarkList = serializedProtoVector.Deserialize(LandmarkList.Parser);
            serializedProtoVector.Dispose();

            return landmarkList;
        }

        public override StatusOr<List<LandmarkList>> Consume()
        {
            throw new NotSupportedException();
        }

        public override Status ValidateAsType()
        {
            throw new NotImplementedException();
        }
    }
}
