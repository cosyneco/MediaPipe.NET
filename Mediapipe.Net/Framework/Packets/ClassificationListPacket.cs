// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Framework.Protobuf;
using Mediapipe.Net.Native;
using System;

namespace Mediapipe.Net.Framework.Packets
{
    public class ClassificationListPacket : Packet<ClassificationList>
    {
        public ClassificationListPacket() : base() { }
        public ClassificationListPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public ClassificationListPacket? At(Timestamp timestamp) => At<ClassificationListPacket>(timestamp);

        public override ClassificationList Get()
        {
            UnsafeNativeMethods.mp_Packet__GetClassificationList(MpPtr, out var serializedProtoVectorPtr).Assert();
            GC.KeepAlive(this);

            var classificationList = serializedProtoVectorPtr.Deserialize(ClassificationList.Parser);
            serializedProtoVectorPtr.Dispose();

            return classificationList;
        }

        public override StatusOr<ClassificationList> Consume()
        {
            throw new NotSupportedException();
        }

        public override Status ValidateAsType()
        {
            throw new NotSupportedException();
        }
    }
}
