// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Framework.Protobuf;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.Packets
{
    public class NormalizedLandmarkListPacket : Packet<NormalizedLandmarkList>
    {
        public NormalizedLandmarkListPacket() : base() { }
        public NormalizedLandmarkListPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public NormalizedLandmarkListPacket? At(Timestamp timestamp) => At<NormalizedLandmarkListPacket>(timestamp);
        public override NormalizedLandmarkList Get()
        {
            UnsafeNativeMethods.mp_Packet__GetNormalizedLandmarkList(MpPtr, out var serializedProto).Assert();
            GC.KeepAlive(this);

            var normalizedLandmarkList = serializedProto.Deserialize(NormalizedLandmarkList.Parser);
            serializedProto.Dispose();

            return normalizedLandmarkList;
        }

        public override StatusOr<NormalizedLandmarkList> Consume()
        {
            throw new NotSupportedException();
        }

        public override Status ValidateAsType()
        {
            throw new NotSupportedException();
        }
    }
}
