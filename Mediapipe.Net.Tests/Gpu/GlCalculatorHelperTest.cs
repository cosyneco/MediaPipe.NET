// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Framework.Format;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Framework.Protobuf;
using Mediapipe.Net.Gpu;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Mediapipe.Net.Tests.Gpu
{
    public class GlCalculatorHelperTest
    {
        #region Constructor
        [Test, GpuOnly]
        public void Ctor_ShouldInstantiateGlCalculatorHelper()
        {
            using var glCalculatorHelper = new GlCalculatorHelper();
            unsafe
            {
                Assert.True(glCalculatorHelper.MpPtr != null);
            }
        }
        #endregion

        #region IsDisposed
        [Test, GpuOnly]
        public void IsDisposed_ShouldReturnFalse_When_NotDisposedYet()
        {
            using var glCalculatorHelper = new GlCalculatorHelper();
            Assert.False(glCalculatorHelper.IsDisposed);
        }

        [Test, GpuOnly]
        public void IsDisposed_ShouldReturnTrue_When_AlreadyDisposed()
        {
            var glCalculatorHelper = new GlCalculatorHelper();
            glCalculatorHelper.Dispose();

            Assert.True(glCalculatorHelper.IsDisposed);
        }
        #endregion

        #region InitializeForTest
        [Test, GpuOnly]
        public void InitializeForTest_ShouldInitialize()
        {
            using var glCalculatorHelper = new GlCalculatorHelper();
            Assert.False(glCalculatorHelper.Initialized());
            glCalculatorHelper.InitializeForTest(GpuResources.Create().Value());
            Assert.True(glCalculatorHelper.Initialized());
        }
        #endregion

        #region RunInGlContext
        [Test, GpuOnly]
        public void RunInGlContext_ShouldReturnOk_When_FunctionReturnsOk()
        {
            using var glCalculatorHelper = new GlCalculatorHelper();
            glCalculatorHelper.InitializeForTest(GpuResources.Create().Value());

            Status status = glCalculatorHelper.RunInGlContext(() => { });
            Assert.True(status.Ok());
        }

        [Test, GpuOnly]
        public void RunInGlContext_ShouldReturnInternal_When_FunctionThrows()
        {
            using var glCalculatorHelper = new GlCalculatorHelper();
            glCalculatorHelper.InitializeForTest(GpuResources.Create().Value());

            Status status = glCalculatorHelper.RunInGlContext((GlCalculatorHelper.GlFunction)(() => { throw new Exception("Function Throws"); }));
            Assert.AreEqual(Status.StatusCode.Internal, status.Code);
        }
        #endregion

        #region CreateSourceTexture
        [Test, GpuOnly]
        public void CreateSourceTexture_ShouldReturnGlTexture_When_CalledWithImageFrame()
        {
            using var glCalculatorHelper = new GlCalculatorHelper();
            glCalculatorHelper.InitializeForTest(GpuResources.Create().Value());

            using var imageFrame = new ImageFrame(ImageFormat.Types.Format.Srgba, 32, 24);
            var status = glCalculatorHelper.RunInGlContext(() =>
            {
                var texture = glCalculatorHelper.CreateSourceTexture(imageFrame);

                Assert.AreEqual(32, texture.Width);
                Assert.AreEqual(24, texture.Height);

                texture.Dispose();
            });
            Assert.True(status.Ok());

            status.Dispose();
        }

        [Test, GpuOnly]
        [Ignore("Skip because a thread will hang")]
        public void CreateSourceTexture_ShouldFail_When_ImageFrameFormatIsInvalid()
        {
            using var glCalculatorHelper = new GlCalculatorHelper();
            glCalculatorHelper.InitializeForTest(GpuResources.Create().Value());

            using var imageFrame = new ImageFrame(ImageFormat.Types.Format.Sbgra, 32, 24);
            Status status = glCalculatorHelper.RunInGlContext(() =>
            {
                using GlTexture texture = glCalculatorHelper.CreateSourceTexture(imageFrame);
                texture.Release();
            });
            Assert.AreEqual(Status.StatusCode.FailedPrecondition, status.Code);

            status.Dispose();
        }
        #endregion

        #region CreateDestinationTexture
        [Test, GpuOnly]
        public void CreateDestinationTexture_ShouldReturnGlTexture_When_GpuBufferFormatIsValid()
        {
            using var glCalculatorHelper = new GlCalculatorHelper();
            glCalculatorHelper.InitializeForTest(GpuResources.Create().Value());

            Status status = glCalculatorHelper.RunInGlContext(() =>
            {
                GlTexture glTexture = glCalculatorHelper.CreateDestinationTexture(32, 24, GpuBufferFormat.KBgra32);

                Assert.AreEqual(32, glTexture.Width);
                Assert.AreEqual(24, glTexture.Height);
            });

            Assert.True(status.Ok());
        }
        #endregion

        #region framebuffer
        [Test, GpuOnly]
        public void Framebuffer_ShouldReturnGLName()
        {
            using var glCalculatorHelper = new GlCalculatorHelper();
            glCalculatorHelper.InitializeForTest(GpuResources.Create().Value());

            // default frame buffer
            Assert.AreEqual(0, glCalculatorHelper.Framebuffer);
        }
        #endregion

        #region GetGlContext
        [Test, GpuOnly]
        public void GetGlContext_ShouldReturnCurrentContext()
        {
            using var glCalculatorHelper = new GlCalculatorHelper();
            glCalculatorHelper.InitializeForTest(GpuResources.Create().Value());

            using var glContext = glCalculatorHelper.GetGlContext();

            unsafe
            {
                if (OperatingSystem.IsLinux() || OperatingSystem.IsAndroid())
                    Assert.True(glContext.EglContext != null);
                else if (OperatingSystem.IsMacOS())
                    Assert.True(glContext.NsglContext != null);
                else if (OperatingSystem.IsIOS())
                    Assert.True(glContext.EaglContext != null);
            }
        }
        #endregion
    }
}
