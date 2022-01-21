// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Framework;
using Mediapipe.Net.Framework.Packet;
using Mediapipe.Net.Framework.Port;
using NUnit.Framework;

namespace Mediapipe.Net.Tests.Framework.Packet
{
    public class PacketTest
    {
        #region At
        [Test]
        public void At_ShouldReturnNewPacketWithTimestamp()
        {
            using var timestamp = new Timestamp(1);
            var packet = new BoolPacket(true).At(timestamp);

            // NOTE: This assert is here because `At()` returns a `Packet<T>?`.
            // TODO: Investigate if `At()` can return a non-nullable `Packet<T>`.
            Assert.NotNull(packet);
            if (packet == null)
                return;

            Assert.True(packet.Get());
            Assert.AreEqual(packet.Timestamp(), timestamp);

            using (var newTimestamp = new Timestamp(2))
            {
                var newPacket = packet.At(newTimestamp);
                Assert.NotNull(newPacket);
                if (newPacket == null)
                    return;

                Assert.IsInstanceOf<BoolPacket>(newPacket);
                Assert.True(newPacket.Get());
                Assert.AreEqual(newPacket.Timestamp(), newTimestamp);
            }

            Assert.True(packet.Get());
            Assert.AreEqual(packet.Timestamp(), timestamp);
        }
        #endregion

        #region DebugString
        [Test]
        public void DebugString_ShouldReturnDebugString_When_InstantiatedWithDefaultConstructor()
        {
            using var packet = new BoolPacket();
            Assert.AreEqual(packet.DebugString(), "mediapipe::Packet with timestamp: Timestamp::Unset() and no data");
        }
        #endregion

        #region DebugTypeName
        [Test]
        public void DebugTypeName_ShouldReturnTypeName_When_ValueIsNotSet()
        {
            using var packet = new BoolPacket();
            Assert.AreEqual(packet.DebugTypeName(), "{empty}");
        }
        #endregion

        #region RegisteredTypeName
        [Test]
        public void RegisteredTypeName_ShouldReturnEmptyString()
        {
            using var packet = new BoolPacket();
            Assert.AreEqual(packet.RegisteredTypeName(), "");
        }
        #endregion

        #region ValidateAsProtoMessageLite
        [Test]
        public void ValidateAsProtoMessageLite_ShouldReturnInvalidArgument_When_ValueIsBool()
        {
            using var packet = new BoolPacket(true);
            Assert.AreEqual(packet.ValidateAsProtoMessageLite().Code(), Status.StatusCode.InvalidArgument);
        }
        #endregion
    }
}
