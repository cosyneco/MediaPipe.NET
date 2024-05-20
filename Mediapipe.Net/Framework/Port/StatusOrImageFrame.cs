// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Framework.Formats;
using Mediapipe.PInvoke;
using Mediapipe.PInvoke.Native;
using System;

namespace Mediapipe.Framework.Port
{
    public unsafe class StatusOrImageFrame : StatusOr<ImageFrame>
    {
        public StatusOrImageFrame(IntPtr ptr) : base(ptr) { }

        protected override void DeleteMpPtr()
        {
            UnsafeNativeMethods.mp_StatusOrImageFrame__delete(ptr);
        }

        private Status? status;
        public override Status Status
        {
            get
            {
                if (status == null || status.isDisposed)
                {
                    UnsafeNativeMethods.mp_StatusOrImageFrame__status(mpPtr, out var statusPtr).Assert();

                    GC.KeepAlive(this);
                    status = new Status(statusPtr);
                }
                return status;
            }
        }

        public override bool Ok() => SafeNativeMethods.mp_StatusOrImageFrame__ok(mpPtr);

        public override ImageFrame Value()
        {
            UnsafeNativeMethods.mp_StatusOrImageFrame__value(mpPtr, out var imageFramePtr).Assert();
            Dispose();

            return new ImageFrame(imageFramePtr);
        }
    }
}
