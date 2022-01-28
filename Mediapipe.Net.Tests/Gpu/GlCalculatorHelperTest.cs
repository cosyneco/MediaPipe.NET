// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Framework.Format;
using Mediapipe.Net.Framework.Port;
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

            var status = glCalculatorHelper.RunInGlContext(() => { return Status.Ok(); });
            Assert.True(status.Ok());
        }

        [Test, GpuOnly]
        public void RunInGlContext_ShouldReturnInternal_When_FunctionReturnsInternal()
        {
            using var glCalculatorHelper = new GlCalculatorHelper();
            glCalculatorHelper.InitializeForTest(GpuResources.Create().Value());

            var status = glCalculatorHelper.RunInGlContext(() => { return Status.Build(Status.StatusCode.Internal, "error"); });
            Assert.AreEqual(status.Code, Status.StatusCode.Internal);
        }

        [Test, GpuOnly]
        public void RunInGlContext_ShouldReturnFailedPreCondition_When_FunctionThrows()
        {
            using var glCalculatorHelper = new GlCalculatorHelper();
            glCalculatorHelper.InitializeForTest(GpuResources.Create().Value());

#pragma warning disable IDE0039
            GlCalculatorHelper.GlStatusFunction glStatusFunction = () => { throw new InvalidProgramException(); };
#pragma warning restore IDE0039
            var status = glCalculatorHelper.RunInGlContext(glStatusFunction);
            Assert.AreEqual(status.Code, Status.StatusCode.FailedPrecondition);
        }
        #endregion

        #region CreateSourceTexture
        [Test, GpuOnly]
        public void CreateSourceTexture_ShouldReturnGlTexture_When_CalledWithImageFrame()
        {
            using var glCalculatorHelper = new GlCalculatorHelper();
            glCalculatorHelper.InitializeForTest(GpuResources.Create().Value());

            using var imageFrame = new ImageFrame(ImageFormat.Srgba, 32, 24);
            var status = glCalculatorHelper.RunInGlContext(() =>
            {
                var texture = glCalculatorHelper.CreateSourceTexture(imageFrame);

                Assert.AreEqual(texture.Width, 32);
                Assert.AreEqual(texture.Height, 24);

                texture.Dispose();
                return Status.Ok();
            });
            Assert.True(status.Ok());

            status.Dispose();
        }

        [Test, GpuOnly]
        public void CreateSourceTexture_ShouldFail_When_ImageFrameFormatIsInvalid()
        {
            using var glCalculatorHelper = new GlCalculatorHelper();
            glCalculatorHelper.InitializeForTest(GpuResources.Create().Value());

            using var imageFrame = new ImageFrame(ImageFormat.Sbgra, 32, 24);
            var status = glCalculatorHelper.RunInGlContext(() =>
            {
                using (var texture = glCalculatorHelper.CreateSourceTexture(imageFrame))
                {
                    texture.Release();
                }
                return Status.Ok();
            });
            Assert.AreEqual(status.Code, Status.StatusCode.FailedPrecondition);

            status.Dispose();
        }
        #endregion

        #region CreateDestinationTexture
        [Test, GpuOnly]
        public void CreateDestinationTexture_ShouldReturnGlTexture_When_GpuBufferFormatIsValid()
        {
            using var glCalculatorHelper = new GlCalculatorHelper();
            glCalculatorHelper.InitializeForTest(GpuResources.Create().Value());

            var status = glCalculatorHelper.RunInGlContext(() =>
            {
                var glTexture = glCalculatorHelper.CreateDestinationTexture(32, 24, GpuBufferFormat.KBgra32);

                Assert.AreEqual(glTexture.Width, 32);
                Assert.AreEqual(glTexture.Height, 24);
                return Status.Ok();
            });

            Assert.True(status.Ok());
        }
        #endregion

        #region Framebuffer
        [Test, GpuOnly]
        public void Framebuffer_ShouldReturnGLName()
        {
            using var glCalculatorHelper = new GlCalculatorHelper();
            glCalculatorHelper.InitializeForTest(GpuResources.Create().Value());

            // default frame buffer
            Assert.AreEqual(glCalculatorHelper.Framebuffer, 0);
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
