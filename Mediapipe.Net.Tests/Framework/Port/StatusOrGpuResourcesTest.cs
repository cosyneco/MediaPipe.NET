// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Gpu;
using NUnit.Framework;

namespace Mediapipe.Net.Tests.Framework.Port
{
    public class StatusOrGpuResourcesTest
    {
        #region Status
        [Test, GpuOnly]
        public void Status_ShouldReturnOk_When_StatusIsOk()
        {
            using var statusOrGpuResources = GpuResources.Create();
            Assert.AreEqual(Status.StatusCode.Ok, statusOrGpuResources.Status.Code);
        }
        #endregion

        #region IsDisposed
        [Test, GpuOnly]
        public void IsDisposed_ShouldReturnFalse_When_NotDisposedYet()
        {
            using var statusOrGpuResources = GpuResources.Create();
            Assert.False(statusOrGpuResources.IsDisposed);
        }

        [Test, GpuOnly]
        public void IsDisposed_ShouldReturnTrue_When_AlreadyDisposed()
        {
            var statusOrGpuResources = GpuResources.Create();
            statusOrGpuResources.Dispose();

            Assert.True(statusOrGpuResources.IsDisposed);
        }
        #endregion

        #region Value
        [Test, GpuOnly]
        public void Value_ShouldReturnGpuResources_When_StatusIsOk()
        {
            using var statusOrGpuResources = GpuResources.Create();
            Assert.True(statusOrGpuResources.Ok());

            using var gpuResources = statusOrGpuResources.Value();
            Assert.IsInstanceOf<GpuResources>(gpuResources);
            Assert.True(statusOrGpuResources.IsDisposed);
        }
        #endregion
    }
}
