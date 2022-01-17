// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Runtime.Versioning;
using Mediapipe.Net.Core;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Gpu
{
    public class GpuResources : MpResourceHandle
    {
        private SharedPtrHandle? sharedPtrHandle;

        /// <param name="ptr">Shared pointer of mediapipe::GpuResources</param>
        public GpuResources(IntPtr ptr) : base()
        {
            sharedPtrHandle = new SharedGpuResourcesPtr(ptr);
            Ptr = sharedPtrHandle.Get();
        }

        protected override void DisposeManaged()
        {
            if (sharedPtrHandle != null)
            {
                sharedPtrHandle.Dispose();
                sharedPtrHandle = null;
            }
            base.DisposeManaged();
        }

        protected override void DeleteMpPtr()
        {
            // Do nothing
        }

        public IntPtr SharedPtr => sharedPtrHandle == null ? IntPtr.Zero : sharedPtrHandle.MpPtr;

        public static StatusOrGpuResources Create()
        {
            UnsafeNativeMethods.mp_GpuResources_Create(out var statusOrGpuResourcesPtr).Assert();

            return new StatusOrGpuResources(statusOrGpuResourcesPtr);
        }

        public static StatusOrGpuResources Create(IntPtr externalContext)
        {
            UnsafeNativeMethods.mp_GpuResources_Create__Pv(externalContext, out var statusOrGpuResourcesPtr).Assert();

            return new StatusOrGpuResources(statusOrGpuResourcesPtr);
        }

        [SupportedOSPlatform("IOS")]
        public IntPtr IosGpuData => SafeNativeMethods.mp_GpuResources__ios_gpu_data(MpPtr);

        private class SharedGpuResourcesPtr : SharedPtrHandle
        {
            public SharedGpuResourcesPtr(IntPtr ptr) : base(ptr) { }

            protected override void DeleteMpPtr() => UnsafeNativeMethods.mp_SharedGpuResources__delete(Ptr);

            public override IntPtr Get() => SafeNativeMethods.mp_SharedGpuResources__get(MpPtr);

            public override void Reset() => UnsafeNativeMethods.mp_SharedGpuResources__reset(MpPtr);
        }
    }
}
