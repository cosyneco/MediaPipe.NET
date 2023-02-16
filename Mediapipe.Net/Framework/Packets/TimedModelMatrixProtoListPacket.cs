// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Framework.Protobuf;
using Mediapipe.Net.Native;
using System;

namespace Mediapipe.Net.Framework.Packets
{
    internal class TimedModelMatrixProtoListPacket : Packet<TimedModelMatrixProtoList>
    {
        public TimedModelMatrixProtoListPacket() : base(true) { }
        public TimedModelMatrixProtoListPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }
        public TimedModelMatrixProtoListPacket? At(Timestamp timestamp) => At<TimedModelMatrixProtoListPacket>(timestamp);

        public override TimedModelMatrixProtoList Get()
        {
            UnsafeNativeMethods.mp_Packet__GetTimedModelMatrixProtoList(MpPtr, out var serializedProtoVector).Assert();
            GC.KeepAlive(this);

            var timedModelMatrixProtoList = serializedProtoVector.Deserialize(TimedModelMatrixProtoList.Parser);
            serializedProtoVector.Dispose();

            return timedModelMatrixProtoList;
        }

        public override StatusOr<TimedModelMatrixProtoList> Consume()
        {
            throw new NotSupportedException();
        }

        public override Status ValidateAsType()
        {
            throw new NotImplementedException();
        }
    }
}
