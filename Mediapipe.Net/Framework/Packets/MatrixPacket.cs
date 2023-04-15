// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Google.Protobuf;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Framework.Protobuf;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.Packets
{
    public class MatrixPacket : Packet<MatrixData>
    {
        public MatrixPacket() : base(true) { }
        public MatrixPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public MatrixPacket(MatrixData matrixData) : base()
        {
            var val = matrixData.ToByteArray();
            UnsafeNativeMethods.mp__MakeMatrixPacket__PKc_i(val, val.Length, out var ptr).Assert();

            Ptr = ptr;
        }

        public MatrixPacket(MatrixData matrixData, Timestamp timestamp) : base()
        {
            var val = matrixData.ToByteArray();
            UnsafeNativeMethods.mp__MakeMatrixPacket_At__PKc_i_Rt(val, val.Length, timestamp.MpPtr, out var ptr).Assert();

            GC.KeepAlive(timestamp);
            Ptr = ptr;
        }

        public MatrixPacket? At(Timestamp timestamp) => At<MatrixPacket>(timestamp);
        public override MatrixData Get()
        {
            UnsafeNativeMethods.mp_Packet__GetMatrix(MpPtr, out var serializedProto).Assert();
            GC.KeepAlive(this);

            var matrix = serializedProto.Deserialize(MatrixData.Parser);
            serializedProto.Dispose();

            return matrix;
        }

        public override StatusOr<MatrixData> Consume()
        {
            throw new NotSupportedException();
        }

        public override Status ValidateAsType()
        {
            throw new NotSupportedException();
        }
    }
}
