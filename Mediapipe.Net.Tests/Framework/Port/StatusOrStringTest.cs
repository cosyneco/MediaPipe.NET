// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Framework.Port;
using NUnit.Framework;

namespace Mediapipe.Net.Tests
{
    public class StatusOrStringTest
    {
        #region #status
        [Test]
        public void Status_ShouldReturnOk_When_StatusIsOk()
        {
            using var statusOrString = initializeSubject("");
            Assert.True(statusOrString.Ok());
            Assert.AreEqual(Status.StatusCode.Ok, statusOrString.Status.Code);
        }
        #endregion

        #region #isDisposed
        [Test]
        public void IsDisposed_ShouldReturnFalse_When_NotDisposedYet()
        {
            using var statusOrString = initializeSubject("");
            Assert.False(statusOrString.IsDisposed);
        }

        [Test]
        public void IsDisposed_ShouldReturnTrue_When_AlreadyDisposed()
        {
            var statusOrString = initializeSubject("");
            statusOrString.Dispose();

            Assert.True(statusOrString.IsDisposed);
        }
        #endregion

        #region #Value
        [Test]
        public void Value_ShouldReturnString_When_StatusIsOk()
        {
            var bytes = new byte[] { (byte)'a', (byte)'b', 0, (byte)'c' };
            using var statusOrString = initializeSubject(bytes);
            Assert.True(statusOrString.Ok());
            Assert.AreEqual("ab", statusOrString.Value());
        }
        #endregion

        #region #ValueAsByteArray
        [Test]
        public void ValueAsByteArray_ShouldReturnByteArray_When_StatusIsOk()
        {
            var bytes = new byte[] { (byte)'a', (byte)'b', 0, (byte)'c' };
            using var statusOrString = initializeSubject(bytes);
            Assert.True(statusOrString.Ok());
            Assert.AreEqual(bytes, statusOrString.ValueAsByteArray());
        }
        #endregion

        private StatusOrString initializeSubject(string str)
        {
            using Packet packet = PacketFactory.StringPacket(str);
            return (StatusOrString)packet.ConsumeString();
        }

        private StatusOrString initializeSubject(byte[] bytes)
        {
            using Packet packet = PacketFactory.StringPacket(bytes);
            return (StatusOrString)packet.ConsumeString();
        }
    }
}
