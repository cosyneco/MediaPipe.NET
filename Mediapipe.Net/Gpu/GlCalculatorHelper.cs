// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Mediapipe.Net.Core;
using Mediapipe.Net.Framework.Format;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Gpu
{
    public unsafe class GlCalculatorHelper : MpResourceHandle
    {
        public delegate void* NativeGlStatusFunction();
        public delegate Status GlStatusFunction();

        public GlCalculatorHelper() : base()
        {
            UnsafeNativeMethods.mp_GlCalculatorHelper__(out var ptr).Assert();
            Ptr = ptr;
        }

        protected override void DeleteMpPtr() => UnsafeNativeMethods.mp_GlCalculatorHelper__delete(Ptr);

        public void InitializeForTest(GpuResources gpuResources)
        {
            UnsafeNativeMethods.mp_GlCalculatorHelper__InitializeForTest__Pgr(MpPtr, gpuResources.MpPtr).Assert();

            GC.KeepAlive(gpuResources);
            GC.KeepAlive(this);
        }

        /// <param name="nativeGlStatusFunction">
        ///   Function that is run in Gl Context.
        ///   Make sure that this function doesn't throw exceptions and won't be GCed.
        /// </param>
        public Status RunInGlContext(NativeGlStatusFunction nativeGlStatusFunction)
        {
            UnsafeNativeMethods.mp_GlCalculatorHelper__RunInGlContext__PF(MpPtr, nativeGlStatusFunction, out var statusPtr).Assert();
            GC.KeepAlive(this);

            return new Status(statusPtr);
        }

        public Status RunInGlContext(GlStatusFunction glStatusFunc)
        {
            Status? tmpStatus = null;

            NativeGlStatusFunction nativeGlStatusFunc = () =>
            {
                try
                {
                    tmpStatus = glStatusFunc();
                }
                catch (Exception e)
                {
                    tmpStatus = Status.FailedPrecondition(e.ToString());
                }
                return tmpStatus.MpPtr;
            };

            // Was previously `GCHandleType.Pinned`. It had to be changed because
            // the `NativeGlStatusFunction` delegate type is non-blittable.
            // Using `GCHandleType.Normal` should be fine as it seems that all we
            // need to do is to make sure that the delegate doesn't get garbage-collected.
            var nativeGlStatusFuncHandle = GCHandle.Alloc(nativeGlStatusFunc, GCHandleType.Normal);
            var status = RunInGlContext(nativeGlStatusFunc);
            nativeGlStatusFuncHandle.Free();

            if (tmpStatus != null)
                tmpStatus.Dispose();
            return status;
        }

        public GlTexture CreateSourceTexture(ImageFrame imageFrame)
        {
            UnsafeNativeMethods.mp_GlCalculatorHelper__CreateSourceTexture__Rif(MpPtr, imageFrame.MpPtr, out var texturePtr).Assert();

            GC.KeepAlive(this);
            GC.KeepAlive(imageFrame);
            return new GlTexture(texturePtr);
        }

        public GlTexture CreateSourceTexture(GpuBuffer gpuBuffer)
        {
            UnsafeNativeMethods.mp_GlCalculatorHelper__CreateSourceTexture__Rgb(MpPtr, gpuBuffer.MpPtr, out var texturePtr).Assert();

            GC.KeepAlive(this);
            GC.KeepAlive(gpuBuffer);
            return new GlTexture(texturePtr);
        }

        [SupportedOSPlatform("IOS")]
        public GlTexture CreateSourceTexture(GpuBuffer gpuBuffer, int plane)
        {
            UnsafeNativeMethods.mp_GlCalculatorHelper__CreateSourceTexture__Rgb_i(MpPtr, gpuBuffer.MpPtr, plane, out var texturePtr).Assert();

            GC.KeepAlive(this);
            GC.KeepAlive(gpuBuffer);
            return new GlTexture(texturePtr);
        }

        public GlTexture CreateDestinationTexture(int width, int height, GpuBufferFormat format)
        {
            UnsafeNativeMethods.mp_GlCalculatorHelper__CreateDestinationTexture__i_i_ui(MpPtr, width, height, format, out var texturePtr).Assert();

            GC.KeepAlive(this);
            return new GlTexture(texturePtr);
        }

        public GlTexture CreateDestinationTexture(GpuBuffer gpuBuffer)
        {
            UnsafeNativeMethods.mp_GlCalculatorHelper__CreateDestinationTexture__Rgb(MpPtr, gpuBuffer.MpPtr, out var texturePtr).Assert();

            GC.KeepAlive(this);
            GC.KeepAlive(gpuBuffer);
            return new GlTexture(texturePtr);
        }

        public uint Framebuffer => SafeNativeMethods.mp_GlCalculatorHelper__framebuffer(MpPtr);

        public void BindFramebuffer(GlTexture glTexture)
        {
            UnsafeNativeMethods.mp_GlCalculatorHelper__BindFrameBuffer__Rtexture(MpPtr, glTexture.MpPtr).Assert();

            GC.KeepAlive(glTexture);
            GC.KeepAlive(this);
        }

        public GlContext GetGlContext()
        {
            var glContextPtr = SafeNativeMethods.mp_GlCalculatorHelper__GetGlContext(MpPtr);

            GC.KeepAlive(this);
            return new GlContext(glContextPtr, false);
        }

        public bool Initialized() => SafeNativeMethods.mp_GlCalculatorHelper__Initialized(MpPtr) > 0;
    }
}
