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
    public class BoolPacketTest
    {
        #region Constructor
        [Test, SignalAbort]
        public void Ctor_ShouldInstantiatePacket_When_CalledWithNoArguments()
        {
            using var packet = new BoolPacket();
#pragma warning disable IDE0058
            Assert.AreEqual(packet.ValidateAsType().Code, Status.StatusCode.Internal);
            Assert.Throws<MediapipeException>(() => { packet.Get(); });
            Assert.AreEqual(packet.Timestamp(), Timestamp.Unset());
#pragma warning restore IDE0058
        }

        [Test]
        public void Ctor_ShouldInstantiatePacket_When_CalledWithTrue()
        {
            using var packet = new BoolPacket(true);
            Assert.True(packet.ValidateAsType().Ok());
            Assert.True(packet.Get());
            Assert.AreEqual(packet.Timestamp(), Timestamp.Unset());
        }

        [Test]
        public void Ctor_ShouldInstantiatePacket_When_CalledWithFalse()
        {
            using var packet = new BoolPacket(false);
            Assert.True(packet.ValidateAsType().Ok());
            Assert.False(packet.Get());
            Assert.AreEqual(packet.Timestamp(), Timestamp.Unset());
        }

        [Test]
        public void Ctor_ShouldInstantiatePacket_When_CalledWithValueAndTimestamp()
        {
            using var timestamp = new Timestamp(1);
            using var packet = new BoolPacket(true, timestamp);
            Assert.True(packet.ValidateAsType().Ok());
            Assert.True(packet.Get());
            Assert.AreEqual(packet.Timestamp(), timestamp);
        }
        #endregion

        #region IsDisposed
        [Test]
        public void IsDisposed_ShouldReturnFalse_When_NotDisposedYet()
        {
            using var packet = new BoolPacket();
            Assert.False(packet.IsDisposed);
        }

        [Test]
        public void IsDisposed_ShouldReturnTrue_When_AlreadyDisposed()
        {
            var packet = new BoolPacket();
            packet.Dispose();

            Assert.True(packet.IsDisposed);
        }
        #endregion

        #region Consume
        [Test]
        public void Consume_ShouldThrowNotSupportedException()
        {
            using var packet = new BoolPacket();
#pragma warning disable IDE0058
            Assert.Throws<NotSupportedException>(() => { packet.Consume(); });
#pragma warning restore IDE0058
        }
        #endregion

        #region DebugTypeName
        [Test]
        public void DebugTypeName_ShouldReturnBool_When_ValueIsSet()
        {
            using var packet = new BoolPacket(true);
            Assert.AreEqual(packet.DebugTypeName(), "bool");
        }
        #endregion
    }
}
