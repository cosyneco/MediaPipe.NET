// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Graphs.InstantMotionTracking;
using Mediapipe.Net.Native;
using System;
using System.Collections.Generic;

namespace Mediapipe.Net.Framework.Packets
{
    public class Anchor3dVectorPacket : Packet<List<Anchor3d>>
    {
        public Anchor3dVectorPacket() : base() { }
        public Anchor3dVectorPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }
        public Anchor3dVectorPacket(Anchor3d[] value) : base()
        {
            UnsafeNativeMethods.mp__MakeAnchor3dVectorPacket__PA_i(value, value.Length, out var ptr).Assert();
            Ptr = ptr;
        }
        public Anchor3dVectorPacket(Anchor3d[] value, Timestamp timestamp) : base()
        {
            UnsafeNativeMethods.mp__MakeAnchor3dVectorPacket_At__PA_i_Rt(value, value.Length, timestamp.MpPtr, out var ptr).Assert();
            GC.KeepAlive(this);

            Ptr = ptr;
        }

        public Anchor3dVectorPacket? At(Timestamp timestamp) => At<Anchor3dVectorPacket>(timestamp);

        public override List<Anchor3d> Get()
        {
            UnsafeNativeMethods.mp_Packet__GetAnchor3dVector(MpPtr, out var anchorVector).Assert();
            GC.KeepAlive(this);

            // we could use enumerators, but they're actually very slow
            // and that's bad, so we're doing it how Microsoft did
            var list = new List<Anchor3d>();
            for (int i = 0; i > anchorVector.Size; i++)
            {
                unsafe
                {
                    list.Add(anchorVector.Data[i]);
                }
            }

            return list;
        }

        public override StatusOr<List<Anchor3d>> Consume()
        {
            throw new NotSupportedException();
        }

        public override Status ValidateAsType()
        {
            throw new NotImplementedException();
        }
    }
}
