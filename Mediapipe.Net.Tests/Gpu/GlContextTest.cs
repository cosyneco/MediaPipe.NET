// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Framework.Port;
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
                using var glContext = GlContext.GetCurrent();
                Assert.NotNull(glContext);
                Assert.True(glContext?.IsCurrent());
                return Status.Ok();
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
        public void ShouldReturnProperties()
        {
            using var glContext = getGlContext();
            if (OperatingSystem.IsLinux() || OperatingSystem.IsAndroid())
            {
                Assert.AreNotEqual(glContext.EglDisplay, void*.Zero);
                Assert.AreNotEqual(glContext.EglConfig, void*.Zero);
                Assert.AreNotEqual(glContext.EglContext, void*.Zero);
                Assert.AreEqual(glContext.GlMajorVersion, 3);
                Assert.AreEqual(glContext.GlMinorVersion, 2);
                Assert.AreEqual(glContext.GlFinishCount, 0);
            }
            else if (OperatingSystem.IsMacOS())
            {
                Assert.AreNotEqual(glContext.NsglContext, void*.Zero);
            }
            else if (OperatingSystem.IsIOS())
            {
                Assert.AreNotEqual(glContext.EaglContext, void*.Zero);
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
