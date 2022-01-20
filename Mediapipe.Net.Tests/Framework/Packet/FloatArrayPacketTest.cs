// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Core;
using Mediapipe.Net.Framework;
using Mediapipe.Net.Framework.Packet;
using Mediapipe.Net.Framework.Port;
using NUnit.Framework;

namespace Mediapipe.Net.Tests.Framework.Packet
{
    public class FloatArrayPacketTest
    {
        #region Constructor
        [Test, SignalAbort]
        public void Ctor_ShouldInstantiatePacket_When_CalledWithNoArguments()
        {
            using var packet = new FloatArrayPacket();
#pragma warning disable IDE0058
            packet.Length = 0;
            Assert.AreEqual(packet.ValidateAsType().Code(), Status.StatusCode.Internal);
            Assert.Throws<MediapipeException>(() => { packet.Get(); });
            Assert.AreEqual(packet.Timestamp(), Timestamp.Unset());
#pragma warning restore IDE0058
        }

        [Test]
        public void Ctor_ShouldInstantiatePacket_When_CalledWithEmptyArray()
        {
            float[] array = Array.Empty<float>();
            using var packet = new FloatArrayPacket(array);
            Assert.True(packet.ValidateAsType().Ok());
            Assert.AreEqual(packet.Get(), array);
            Assert.AreEqual(packet.Timestamp(), Timestamp.Unset());
        }

        [Test]
        public void Ctor_ShouldInstantiatePacket_When_CalledWithArray()
        {
            float[] array = { 0.01f };
            using var packet = new FloatArrayPacket(array);
            Assert.True(packet.ValidateAsType().Ok());
            Assert.AreEqual(packet.Get(), array);
            Assert.AreEqual(packet.Timestamp(), Timestamp.Unset());
        }

        [Test]
        public void Ctor_ShouldInstantiatePacket_When_CalledWithValueAndTimestamp()
        {
            float[] array = { 0.01f, 0.02f };
            using var timestamp = new Timestamp(1);
            using var packet = new FloatArrayPacket(array, timestamp);
            Assert.True(packet.ValidateAsType().Ok());
            Assert.AreEqual(packet.Get(), array);
            Assert.AreEqual(packet.Timestamp(), timestamp);
        }
        #endregion

        #region IsDisposed
        [Test]
        public void IsDisposed_ShouldReturnFalse_When_NotDisposedYet()
        {
            using var packet = new FloatArrayPacket();
            Assert.False(packet.IsDisposed);
        }

        [Test]
        public void IsDisposed_ShouldReturnTrue_When_AlreadyDisposed()
        {
            var packet = new FloatArrayPacket();
            packet.Dispose();

            Assert.True(packet.IsDisposed);
        }
        #endregion

        #region Consume
        [Test]
        public void Consume_ShouldThrowNotSupportedException()
        {
            using var packet = new FloatArrayPacket();
#pragma warning disable IDE0058
            Assert.Throws<NotSupportedException>(() => { packet.Consume(); });
#pragma warning restore IDE0058
        }
        #endregion

        #region DebugTypeName
        [Test]
        public void DebugTypeName_ShouldReturnFloat_When_ValueIsSet()
        {
            float[] array = { 0.01f };
            using var packet = new FloatArrayPacket(array);
            Assert.AreEqual(packet.DebugTypeName(), "float []");
        }
        #endregion
    }
}
