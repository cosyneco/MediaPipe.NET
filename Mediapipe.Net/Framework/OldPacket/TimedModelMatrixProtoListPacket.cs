// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Framework.Protobuf;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.OldPacket
{
    public unsafe class TimedModelMatrixProtoListPacket : Packet<TimedModelMatrixProtoList>
    {
        public TimedModelMatrixProtoListPacket() : base() { }
        public TimedModelMatrixProtoListPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public override TimedModelMatrixProtoList Get()
        {
            UnsafeNativeMethods.mp_Packet__GetTimedModelMatrixProtoList(MpPtr, out var serializedProto).Assert();
            GC.KeepAlive(this);

            var matrixProtoList = serializedProto.Deserialize(TimedModelMatrixProtoList.Parser);
            serializedProto.Dispose();

            return matrixProtoList;
        }

        public override StatusOr<TimedModelMatrixProtoList> Consume() => throw new NotSupportedException();
    }
}
