// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Gpu;
using NUnit.Framework;

namespace Mediapipe.Net.Tests.Gpu
{
    public class GlContextTest
    {
        #region GetCurrent
        [Test, GpuOnly]
        public void GetCurrent_ShouldReturnNull_When_CalledOutOfGlContext()
        {
            var glContext = GlContext.GetCurrent();

            Assert.Null(glContext);
        }

        [Test, GpuOnly]
        public void GetCurrent_ShouldReturnCurrentContext_When_CalledInGlContext()
        {
            using var glCalculatorHelper = new GlCalculatorHelper();
            glCalculatorHelper.InitializeForTest(GpuResources.Create().Value());

            glCalculatorHelper.RunInGlContext(() =>
            {
                using GlContext? glContext = GlContext.GetCurrent();
                Assert.NotNull(glContext);
                Assert.True(glContext?.IsCurrent());
            }).AssertOk();
        }
        #endregion

        #region IsCurrent
        [Test, GpuOnly]
        public void IsCurrent_ShouldReturnFalse_When_CalledOutOfGlContext()
        {
            var glContext = getGlContext();

            Assert.False(glContext.IsCurrent());
        }
        #endregion

        #region Properties
        [Test, GpuOnly]
        public unsafe void ShouldReturnProperties()
        {
            using var glContext = getGlContext();
            if (OperatingSystem.IsLinux() || OperatingSystem.IsAndroid())
            {
                Assert.True(glContext.EglDisplay != IntPtr.Zero);
                Assert.True(glContext.EglConfig != IntPtr.Zero);
                Assert.True(glContext.EglContext != IntPtr.Zero);
                Assert.AreEqual(3, glContext.GlMajorVersion);
                Assert.AreEqual(2, glContext.GlMinorVersion);
                Assert.AreEqual(0, glContext.GlFinishCount);
            }
            else if (OperatingSystem.IsMacOS())
            {
                Assert.True(glContext.NsglContext != IntPtr.Zero);
            }
            else if (OperatingSystem.IsIOS())
            {
                Assert.True(glContext.EaglContext != IntPtr.Zero);
            }
        }
        #endregion

        private static GlContext getGlContext()
        {
            using var glCalculatorHelper = new GlCalculatorHelper();
            glCalculatorHelper.InitializeForTest(GpuResources.Create().Value());

            return glCalculatorHelper.GetGlContext();
        }
    }
}
