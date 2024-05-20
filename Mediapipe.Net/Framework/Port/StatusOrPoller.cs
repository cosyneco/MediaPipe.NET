// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.PInvoke;
using Mediapipe.PInvoke.Native;
using System;

namespace Mediapipe.Framework.Port
{
    public class StatusOrPoller<T> : StatusOr<OutputStreamPoller<T>>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public StatusOrPoller(IntPtr ptr) : base(ptr) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        protected override void DeleteMpPtr()
        {
            UnsafeNativeMethods.mp_StatusOrPoller__delete(ptr);
        }

        private Status status;
        public override Status Status
        {
            get
            {
                if (status == null || status.isDisposed)
                {
                    UnsafeNativeMethods.mp_StatusOrPoller__status(mpPtr, out var statusPtr).Assert();

                    GC.KeepAlive(this);
                    status = new Status(statusPtr);
                }
                return status;
            }
        }

        public override bool Ok()
        {
            return SafeNativeMethods.mp_StatusOrPoller__ok(mpPtr);
        }

        public override OutputStreamPoller<T> Value()
        {
            UnsafeNativeMethods.mp_StatusOrPoller__value(mpPtr, out var pollerPtr).Assert();
            Dispose();

            return new OutputStreamPoller<T>(pollerPtr);
        }
    }
}
