// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Gpu;
using NUnit.Framework;

namespace Mediapipe.Net.Tests.Gpu
{
    public class GpuResourcesTest
    {
        #region Create
        [Test, GpuOnly]
        public void Create_ShouldReturnStatusOrGpuResources()
        {
            using var statusOrGpuResources = GpuResources.Create();
            Assert.True(statusOrGpuResources.Ok());
        }
        #endregion

        #region IsDisposed
        [Test, GpuOnly]
        public void IsDisposed_ShouldReturnFalse_When_NotDisposedYet()
        {
            using var gpuResources = GpuResources.Create().Value();
            Assert.False(gpuResources.IsDisposed);
        }

        [Test, GpuOnly]
        public void IsDisposed_ShouldReturnTrue_When_AlreadyDisposed()
        {
            var gpuResources = GpuResources.Create().Value();
            gpuResources.Dispose();

            Assert.True(gpuResources.IsDisposed);
        }
        #endregion
    }
}
