// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.Port
{
    public unsafe class StatusOrPoller : StatusOr<OutputStreamPoller>
    {
        public StatusOrPoller(void* ptr) : base(ptr) { }

        protected override void DeleteMpPtr() => UnsafeNativeMethods.mp_StatusOrPoller__delete(Ptr);

        private Status? status;
        public override Status Status
        {
            get
            {
                if (status == null || status.IsDisposed)
                {
                    UnsafeNativeMethods.mp_StatusOrPoller__status(MpPtr, out var statusPtr).Assert();

                    GC.KeepAlive(this);
                    status = new Status(statusPtr);
                }
                return status;
            }
        }

        public override bool Ok() => SafeNativeMethods.mp_StatusOrPoller__ok(MpPtr) > 0;

        public override OutputStreamPoller Value()
        {
            UnsafeNativeMethods.mp_StatusOrPoller__value(MpPtr, out var pollerPtr).Assert();
            Dispose();

            return new OutputStreamPoller(pollerPtr);
        }
    }
}
