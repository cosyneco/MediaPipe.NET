// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Framework.Packet;
using NUnit.Framework;

namespace Mediapipe.Net.Tests.Framework.Packet
{
    public class SidePacketTest
    {
        #region Size
        [Test]
        public void Size_ShouldReturnZero_When_Initialized()
        {
            using var sidePacket = new SidePacket();
            Assert.AreEqual(sidePacket.Size, 0);
        }

        [Test]
        public void Size_ShouldReturnSize_When_AfterPacketsAreEmplaced()
        {
            using var sidePacket = new SidePacket();
            var flagPacket = new BoolPacket(true);
            var valuePacket = new FloatPacket(1.0f);
            sidePacket.Emplace("flag", flagPacket);
            sidePacket.Emplace("value", valuePacket);

            Assert.AreEqual(sidePacket.Size, 2);
            Assert.True(flagPacket.IsDisposed);
            Assert.True(valuePacket.IsDisposed);
        }
        #endregion

        #region Emplace
        [Test]
        public void Emplace_ShouldInsertAndDisposePacket()
        {
            using var sidePacket = new SidePacket();
            Assert.AreEqual(sidePacket.Size, 0);
            Assert.IsNull(sidePacket.At<FloatPacket>("value"));

            var flagPacket = new FloatPacket(1.0f);
            sidePacket.Emplace("value", flagPacket);

            Assert.AreEqual(sidePacket.Size, 1);
            Assert.AreEqual(sidePacket.At<FloatPacket>("value")?.Get(), 1.0f);
            Assert.True(flagPacket.IsDisposed);
        }

        [Test]
        public void Emplace_ShouldIgnoreValue_When_KeyExists()
        {
            using (var sidePacket = new SidePacket())
            {
                var oldValuePacket = new FloatPacket(1.0f);
                sidePacket.Emplace("value", oldValuePacket);
                Assert.AreEqual(sidePacket.At<FloatPacket>("value")?.Get(), 1.0f);

                var newValuePacket = new FloatPacket(2.0f);
                sidePacket.Emplace("value", newValuePacket);
                Assert.AreEqual(sidePacket.At<FloatPacket>("value")?.Get(), 1.0f);
            }
        }
        #endregion

        #region Erase
        [Test]
        public void Erase_ShouldDoNothing_When_KeyDoesNotExist()
        {
            using var sidePacket = new SidePacket();
            var count = sidePacket.Erase("value");

            Assert.AreEqual(sidePacket.Size, 0);
            Assert.AreEqual(count, 0);
        }

        [Test]
        public void Erase_ShouldEraseKey_When_KeyExists()
        {
            using var sidePacket = new SidePacket();
            sidePacket.Emplace("value", new BoolPacket(true));
            Assert.AreEqual(sidePacket.Size, 1);

            var count = sidePacket.Erase("value");
            Assert.AreEqual(sidePacket.Size, 0);
            Assert.AreEqual(count, 1);
        }
        #endregion

        #region Clear
        [Test]
        public void Clear_ShouldDoNothing_When_SizeIsZero()
        {
            using var sidePacket = new SidePacket();
            sidePacket.Clear();

            Assert.AreEqual(sidePacket.Size, 0);
        }

        [Test]
        public void Clear_ShouldClearAllKeys_When_SizeIsNotZero()
        {
            using var sidePacket = new SidePacket();
            sidePacket.Emplace("flag", new BoolPacket(true));
            sidePacket.Emplace("value", new FloatPacket(1.0f));
            Assert.AreEqual(sidePacket.Size, 2);

            sidePacket.Clear();
            Assert.AreEqual(sidePacket.Size, 0);
        }
        #endregion
    }
}
