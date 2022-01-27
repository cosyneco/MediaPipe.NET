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
    public unsafe class ClassificationListVectorPacket : Packet<List<ClassificationList>>
    {
        public ClassificationListVectorPacket() : base() { }
        public ClassificationListVectorPacket(void* ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public override List<ClassificationList> Get()
        {
            UnsafeNativeMethods.mp_Packet__GetClassificationListVector(MpPtr, out var serializedProtoVector).Assert();
            GC.KeepAlive(this);

            var classificationLists = serializedProtoVector.Deserialize(ClassificationList.Parser);
            serializedProtoVector.Dispose();

            return classificationLists;
        }

        public override StatusOr<List<ClassificationList>> Consume() => throw new NotSupportedException();
    }
}
