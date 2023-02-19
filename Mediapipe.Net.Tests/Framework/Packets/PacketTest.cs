// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Framework;
using Mediapipe.Net.Framework.Packets;
using Mediapipe.Net.Framework.Port;
using NUnit.Framework;

namespace Mediapipe.Net.Tests.Framework.NewPacket
{
    public class PacketTest
    {
        #region At
        [Test]
        public void At_ShouldReturnNewPacketWithTimestamp()
        {
            using var timestamp = new Timestamp(1);
            var packet = new BoolPacket(true).At(timestamp);

            Assert.True(packet?.Get());
            Assert.AreEqual(packet?.Timestamp(), timestamp);

            using (var newTimestamp = new Timestamp(2))
            {
                var newPacket = packet?.At(newTimestamp);
                Assert.NotNull(newPacket);
                if (newPacket == null)
                    return;

                Assert.True(newPacket.Get());
                Assert.AreEqual(newPacket.Timestamp(), newTimestamp);
            }

            Assert.True(packet?.Get());
            Assert.AreEqual(packet?.Timestamp(), timestamp);
        }
        #endregion

        #region ValidateAsProtoMessageLite
        [Test]
        public void ValidateAsProtoMessageLite_ShouldReturnInvalidArgument_When_ValueIsBool()
        {
            using var packet = new BoolPacket(true);
            Assert.AreEqual(packet?.ValidateAsProtoMessageLite().Code, Status.StatusCode.InvalidArgument);
        }
        #endregion
    }
}
