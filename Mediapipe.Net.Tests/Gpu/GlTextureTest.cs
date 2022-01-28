// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Gpu;
using NUnit.Framework;

namespace Mediapipe.Net.Tests.Gpu
{
    public class GlTextureTest
    {
        #region Constructor
        [Test, GpuOnly]
        public void Ctor_ShouldInstantiateGlTexture_When_CalledWithNoArguments()
        {
            using var glTexture = new GlTexture();
            Assert.AreEqual(glTexture.Width, 0);
            Assert.AreEqual(glTexture.Height, 0);
        }
        #endregion

        #region IsDisposed
        [Test, GpuOnly]
        public void IsDisposed_ShouldReturnFalse_When_NotDisposedYet()
        {
            using var glTexture = new GlTexture();
            Assert.False(glTexture.IsDisposed);
        }

        [Test, GpuOnly]
        public void IsDisposed_ShouldReturnTrue_When_AlreadyDisposed()
        {
            var glTexture = new GlTexture();
            glTexture.Dispose();

            Assert.True(glTexture.IsDisposed);
        }
        #endregion

        #region Target
        [Test, GpuOnly]
        public void Target_ShouldReturnTarget()
        {
            using var glTexture = new GlTexture();
            Assert.AreEqual(glTexture.Target, Gl.GL_TEXTURE_2D);
        }
        #endregion
    }
}
