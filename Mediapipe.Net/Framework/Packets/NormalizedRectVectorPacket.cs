// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Framework.Protobuf;
using Mediapipe.Net.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediapipe.Net.Framework.Packets
{
    public class NormalizedRectVectorPacket : Packet<List<NormalizedRect>>
    {
        public NormalizedRectVectorPacket() : base(true) { }
        public NormalizedRectVectorPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }
        public NormalizedRectVectorPacket? At(Timestamp timestamp) => At<NormalizedRectVectorPacket>(timestamp);

        public override List<NormalizedRect> Get()
        {
            UnsafeNativeMethods.mp_Packet__GetNormalizedRectVector(MpPtr, out var serializedProtoVector).Assert();
            GC.KeepAlive(this);

            var normalizedRect = serializedProtoVector.Deserialize(NormalizedRect.Parser);
            serializedProtoVector.Dispose();

            return normalizedRect;
        }

        public override StatusOr<List<NormalizedRect>> Consume()
        {
            throw new NotSupportedException();
        }

        public override Status ValidateAsType()
        {
            throw new NotImplementedException();
        }
    }
}
