// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Framework;
using Mediapipe.Net.Framework.Format;
using Mediapipe.Net.Framework.Packets;
using Mediapipe.Net.Framework.Protobuf;
using NUnit.Framework;

namespace Mediapipe.Net.Tests.Framework.NewPacket
{
    public class ImageFramePacketTest
    {
        #region Constructor
        [Test]
        public void Ctor_ShouldInstantiatePacket_When_CalledWithValue()
        {
            var srcImageFrame = new ImageFrame();

            using var packet = new ImageFramePacket(srcImageFrame);
            Assert.True(srcImageFrame.IsDisposed);
            Assert.True(packet.ValidateAsType().Ok());
            Assert.AreEqual(packet.Timestamp(), Timestamp.Unset());

            using var statusOrImageFrame = packet.Consume();
            Assert.True(statusOrImageFrame.Ok());

            using ImageFrame? imageFrame = statusOrImageFrame.Value();
            Assert.AreEqual(imageFrame?.Format(), ImageFormat.Types.Format.Unknown);
        }

        [Test]
        public void Ctor_ShouldInstantiatePacket_When_CalledWithValueAndTimestamp()
        {
            var srcImageFrame = new ImageFrame();

            using var timestamp = new Timestamp(1);

            // Using PacketFactory.ImageFramePacket(srcImageFrame).At(timestamp) fails the test...
            // I have no clue why. We'll have to inspect the native bindings to find out.
            using var packet = new ImageFramePacket(srcImageFrame, timestamp);
            Assert.True(srcImageFrame.IsDisposed);
            Assert.True(packet.ValidateAsType().Ok());

            using var statusOrImageFrame = packet.Consume();
            Assert.True(statusOrImageFrame.Ok());

            using var imageFrame = statusOrImageFrame.Value();
            Assert.AreEqual(imageFrame?.Format(), ImageFormat.Types.Format.Unknown);
            Assert.AreEqual(packet.Timestamp(), timestamp);
        }
        #endregion

        #region IsDisposed
        [Test]
        public void IsDisposed_ShouldReturnFalse_When_NotDisposedYet()
        {
            using var packet = new ImageFramePacket(new ImageFrame());
            Assert.False(packet.IsDisposed);
        }

        [Test]
        public void IsDisposed_ShouldReturnTrue_When_AlreadyDisposed()
        {
            var packet = new ImageFramePacket(new ImageFrame());
            packet.Dispose();

            Assert.True(packet.IsDisposed);
        }
        #endregion

        #region Get
        [Test]
        public void Get_ShouldReturnImageFrame_When_DataIsNotEmpty()
        {
            using var packet = new ImageFramePacket(new ImageFrame(ImageFormat.Types.Format.Sbgra, 10, 10));
            using var imageFrame = packet.Get();
            Assert.AreEqual(imageFrame.Format(), ImageFormat.Types.Format.Sbgra);
            Assert.AreEqual(imageFrame.Width(), 10);
            Assert.AreEqual(imageFrame.Height(), 10);
        }
        #endregion

        #region Consume
        [Test]
        public void Consume_ShouldReturnImageFrame()
        {
            using var packet = new ImageFramePacket(new ImageFrame(ImageFormat.Types.Format.Sbgra, 10, 10));
            using var statusOrImageFrame = packet.Consume();
            Assert.True(statusOrImageFrame.Ok());

            using ImageFrame? imageFrame = statusOrImageFrame.Value();
            Assert.AreEqual(imageFrame?.Format(), ImageFormat.Types.Format.Sbgra);
            Assert.AreEqual(imageFrame?.Width(), 10);
            Assert.AreEqual(imageFrame?.Height(), 10);
        }
        #endregion

        #region DebugTypeName
        [Test]
        public void DebugTypeName_ShouldReturnFloat_When_ValueIsSet()
        {
            using var packet = new ImageFramePacket(new ImageFrame());

            Assert.AreEqual(packet.DebugTypeName(),
                OperatingSystem.IsWindows()
                    ? "class mediapipe::ImageFrame"
                    : "mediapipe::ImageFrame");
        }
        #endregion
    }
}
