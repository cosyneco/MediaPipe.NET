// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Framework.Protobuf;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.Packet
{
    public class ClassificationListPacket : Packet<ClassificationList>
    {
        public ClassificationListPacket() : base() { }
        public ClassificationListPacket(void* ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public override ClassificationList Get()
        {
            UnsafeNativeMethods.mp_Packet__GetClassificationList(MpPtr, out var serializedProto).Assert();
            GC.KeepAlive(this);

            var classificationList = serializedProto.Deserialize(ClassificationList.Parser);
            serializedProto.Dispose();

            return classificationList;
        }

        public override StatusOr<ClassificationList> Consume() => throw new NotSupportedException();
    }
}
