// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Framework.Protobuf;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.Packets
{
    public class LandmarkListPacket : Packet<LandmarkList>
    {
        public LandmarkListPacket() : base(true) { }
        public LandmarkListPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public LandmarkListPacket? At(Timestamp timestamp) => At<LandmarkListPacket>(timestamp);
        public override LandmarkList Get()
        {
            UnsafeNativeMethods.mp_Packet__GetLandmarkList(MpPtr, out var serializedProto).Assert();
            GC.KeepAlive(this);

            var landmarkList = serializedProto.Deserialize(LandmarkList.Parser);
            serializedProto.Dispose();

            return landmarkList;
        }

        public override StatusOr<LandmarkList> Consume()
        {
            throw new NotSupportedException();
        }

        public override Status ValidateAsType()
        {
            throw new NotSupportedException();
        }
    }
}
