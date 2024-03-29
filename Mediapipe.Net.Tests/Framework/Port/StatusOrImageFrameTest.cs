// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Framework;
using Mediapipe.Net.Framework.Format;
using Mediapipe.Net.Framework.Packets;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Framework.Protobuf;
using NUnit.Framework;

namespace Mediapipe.Net.Tests.Framework.Port
{
    public class StatusOrImageFrameTest
    {
        #region Status
        [Test]
        public void Status_ShouldReturnOk_When_StatusIsOk()
        {
            using var statusOrImageFrame = initializeSubject();
            Assert.True(statusOrImageFrame.Ok());
            Assert.AreEqual(Status.StatusCode.Ok, statusOrImageFrame.Status.Code);
        }
        #endregion

        #region IsDisposed
        [Test]
        public void IsDisposed_ShouldReturnFalse_When_NotDisposedYet()
        {
            using var statusOrImageFrame = initializeSubject();
            Assert.False(statusOrImageFrame.IsDisposed);
        }

        [Test]
        public void IsDisposed_ShouldReturnTrue_When_AlreadyDisposed()
        {
            var statusOrImageFrame = initializeSubject();
            statusOrImageFrame.Dispose();

            Assert.True(statusOrImageFrame.IsDisposed);
        }
        #endregion

        #region Value
        [Test]
        public void Value_ShouldReturnImageFrame_When_StatusIsOk()
        {
            using var statusOrImageFrame = initializeSubject();
            Assert.True(statusOrImageFrame.Ok());

            using var imageFrame = statusOrImageFrame.Value();
            Assert.AreEqual(10, imageFrame.Width());
            Assert.AreEqual(10, imageFrame.Height());
            Assert.True(statusOrImageFrame.IsDisposed);
        }
        #endregion

        private static StatusOrImageFrame initializeSubject()
        {
            var imageFrame = new ImageFrame(ImageFormat.Types.Format.Sbgra, 10, 10);
            var packet = new ImageFramePacket(imageFrame, new Timestamp(1));

            return (StatusOrImageFrame)packet.Consume();
        }
    }
}
