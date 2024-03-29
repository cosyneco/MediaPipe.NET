// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.Port
{
    public class StatusOrPoller<T> : StatusOr<OutputStreamPoller<T>>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public StatusOrPoller(IntPtr ptr) : base(ptr) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        protected override void DeleteMpPtr()
        {
            UnsafeNativeMethods.mp_StatusOrPoller__delete(Ptr);
        }

        private Status status;
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

        public override bool Ok()
        {
            return SafeNativeMethods.mp_StatusOrPoller__ok(MpPtr);
        }

        public override OutputStreamPoller<T> Value()
        {
            UnsafeNativeMethods.mp_StatusOrPoller__value(MpPtr, out var pollerPtr).Assert();
            Dispose();

            return new OutputStreamPoller<T>(pollerPtr);
        }
    }
}
