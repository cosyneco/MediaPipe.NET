// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.PInvoke;
using Mediapipe.PInvoke.Native;
using System;

namespace Mediapipe.Framework.Port
{
    public unsafe class StatusOrString : StatusOr<string>
    {
        public StatusOrString(IntPtr ptr) : base(ptr) { }

        protected override void DeleteMpPtr()
        {
            UnsafeNativeMethods.mp_StatusOrString__delete(ptr);
        }

        private Status? status;
        public override Status Status
        {
            get
            {
                if (status == null || status.isDisposed)
                {
                    UnsafeNativeMethods.mp_StatusOrString__status(mpPtr, out var statusPtr).Assert();

                    GC.KeepAlive(this);
                    status = new Status(statusPtr);
                }
                return status;
            }
        }

        public override bool Ok() => SafeNativeMethods.mp_StatusOrString__ok(mpPtr) > 0;

        public override string? Value()
        {
            var str = MarshalStringFromNative(UnsafeNativeMethods.mp_StatusOrString__value);
            Dispose(); // respect move semantics

            return str;
        }

        public byte[] ValueAsByteArray()
        {
            UnsafeNativeMethods.mp_StatusOrString__bytearray(mpPtr, out var strPtr, out var size).Assert();
            GC.KeepAlive(this);

            byte[] bytes = UnsafeUtil.SafeArrayCopy((byte*)strPtr, size);
            UnsafeNativeMethods.delete_array__PKc(strPtr);
            Dispose();

            return bytes;
        }
    }
}
