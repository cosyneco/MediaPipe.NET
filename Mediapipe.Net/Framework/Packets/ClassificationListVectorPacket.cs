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
    public class ClassificationListVectorPacket : Packet<List<ClassificationList>>
    {
        public ClassificationListVectorPacket() : base() { }
        public ClassificationListVectorPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public ClassificationListVectorPacket? At(Timestamp timestamp) => At<ClassificationListVectorPacket>(timestamp);
        public override List<ClassificationList> Get()
        {
            UnsafeNativeMethods.mp_Packet__GetClassificationListVector(MpPtr, out var serializedProtoVectorPtr).Assert();
            GC.KeepAlive(this);

            var classificationListVector = serializedProtoVectorPtr.Deserialize(ClassificationList.Parser);
            serializedProtoVectorPtr.Dispose();

            return classificationListVector;
        }

        public override StatusOr<List<ClassificationList>> Consume()
        {
            throw new NotSupportedException();
        }

        public override Status ValidateAsType()
        {
            throw new NotSupportedException();
        }
    }
}
