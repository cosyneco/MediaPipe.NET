// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Framework.OldPacket;
using NUnit.Framework;

namespace Mediapipe.Net.Tests.Framework.Packet
{
    public class SidePacketsTest
    {
        #region Size
        [Test]
        public void Size_ShouldReturnZero_When_Initialized()
        {
            using var sidePackets = new SidePackets();
            Assert.AreEqual(sidePackets.Size, 0);
        }

        [Test]
        public void Size_ShouldReturnSize_When_AfterPacketsAreEmplaced()
        {
            using var sidePackets = new SidePackets();
            var flagPacket = new BoolPacket(true);
            var valuePacket = new FloatPacket(1.0f);
            sidePackets.Emplace("flag", flagPacket);
            sidePackets.Emplace("value", valuePacket);

            Assert.AreEqual(sidePackets.Size, 2);
            Assert.True(flagPacket.IsDisposed);
            Assert.True(valuePacket.IsDisposed);
        }
        #endregion

        #region Emplace
        [Test]
        public void Emplace_ShouldInsertAndDisposePacket()
        {
            using var sidePackets = new SidePackets();
            Assert.AreEqual(sidePackets.Size, 0);
            Assert.IsNull(sidePackets.At<FloatPacket>("value"));

            var flagPacket = new FloatPacket(1.0f);
            sidePackets.Emplace("value", flagPacket);

            Assert.AreEqual(sidePackets.Size, 1);
            Assert.AreEqual(sidePackets.At<FloatPacket>("value")?.Get(), 1.0f);
            Assert.True(flagPacket.IsDisposed);
        }

        [Test]
        public void Emplace_ShouldIgnoreValue_When_KeyExists()
        {
            using var sidePackets = new SidePackets();
            var oldValuePacket = new FloatPacket(1.0f);
            sidePackets.Emplace("value", oldValuePacket);
            Assert.AreEqual(sidePackets.At<FloatPacket>("value")?.Get(), 1.0f);

            var newValuePacket = new FloatPacket(2.0f);
            sidePackets.Emplace("value", newValuePacket);
            Assert.AreEqual(sidePackets.At<FloatPacket>("value")?.Get(), 1.0f);
        }
        #endregion

        #region Erase
        [Test]
        public void Erase_ShouldDoNothing_When_KeyDoesNotExist()
        {
            using var sidePackets = new SidePackets();
            var count = sidePackets.Erase("value");

            Assert.AreEqual(sidePackets.Size, 0);
            Assert.AreEqual(count, 0);
        }

        [Test]
        public void Erase_ShouldEraseKey_When_KeyExists()
        {
            using var sidePackets = new SidePackets();
            sidePackets.Emplace("value", new BoolPacket(true));
            Assert.AreEqual(sidePackets.Size, 1);

            var count = sidePackets.Erase("value");
            Assert.AreEqual(sidePackets.Size, 0);
            Assert.AreEqual(count, 1);
        }
        #endregion

        #region Clear
        [Test]
        public void Clear_ShouldDoNothing_When_SizeIsZero()
        {
            using var sidePackets = new SidePackets();
            sidePackets.Clear();

            Assert.AreEqual(sidePackets.Size, 0);
        }

        [Test]
        public void Clear_ShouldClearAllKeys_When_SizeIsNotZero()
        {
            using var sidePackets = new SidePackets();
            sidePackets.Emplace("flag", new BoolPacket(true));
            sidePackets.Emplace("value", new FloatPacket(1.0f));
            Assert.AreEqual(sidePackets.Size, 2);

            sidePackets.Clear();
            Assert.AreEqual(sidePackets.Size, 0);
        }
        #endregion
    }
}
