// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Core;
using Mediapipe.Net.Framework.Port;
using NUnit.Framework;

namespace Mediapipe.Net.Tests.Framework.Port
{
    public class StatusTest
    {
        #region #Code
        [Test]
        public void Code_ShouldReturnStatusCode_When_StatusIsOk()
        {
            using var status = Status.Ok();
            Assert.AreEqual(status.Code(), Status.StatusCode.Ok);
        }

        [Test]
        public void Code_ShouldReturnStatusCode_When_StatusIsFailedPrecondition()
        {
            using var status = Status.FailedPrecondition();
            Assert.AreEqual(status.Code(), Status.StatusCode.FailedPrecondition);
        }
        #endregion

        #region #IsDisposed
        [Test]
        public void IsDisposed_ShouldReturnFalse_When_NotDisposedYet()
        {
            using var status = Status.Ok();
            Assert.False(status.IsDisposed);
        }

        [Test]
        public void IsDisposed_ShouldReturnTrue_When_AlreadyDisposed()
        {
            var status = Status.Ok();
            status.Dispose();

            Assert.True(status.IsDisposed);
        }
        #endregion

        #region #RawCode
        [Test]
        public void RawCode_ShouldReturnRawCode_When_StatusIsOk()
        {
            using var status = Status.Ok();
            Assert.AreEqual(status.RawCode(), 0);
        }

        [Test]
        public void RawCode_ShouldReturnRawCode_When_StatusIsFailedPrecondition()
        {
            using var status = Status.FailedPrecondition();
            Assert.AreEqual(status.RawCode(), 9);
        }
        #endregion

        #region #Ok
        [Test]
        public void Ok_ShouldReturnTrue_When_StatusIsOk()
        {
            using var status = Status.Ok();
            Assert.True(status.Ok());
        }

        [Test]
        public void Ok_ShouldReturnFalse_When_StatusIsFailedPrecondition()
        {
            using var status = Status.FailedPrecondition();
            Assert.False(status.Ok());
        }
        #endregion

        #region #AssertOk
        [Test]
        public void AssertOk_ShouldNotThrow_When_StatusIsOk()
        {
            using var status = Status.Ok();
            Assert.DoesNotThrow(() => { status.AssertOk(); });
        }

        [Test]
        public void AssertOk_ShouldThrow_When_StatusIsNotOk()
        {
            using var status = Status.FailedPrecondition();
#pragma warning disable IDE0058
            Assert.Throws<MediapipeException>(() => { status.AssertOk(); });

#pragma warning restore IDE0058
        }
        #endregion

        #region #ToString
        [Test]
        public void ToString_ShouldReturnMessage_When_StatusIsOk()
        {
            using var status = Status.Ok();
            Assert.AreEqual(status.ToString(), "OK");
        }

        [Test]
        public void ToString_ShouldReturnMessage_When_StatusIsFailedPrecondition()
        {
            var message = "Some error";
            using var status = Status.FailedPrecondition(message);
            Assert.AreEqual(status.ToString(), $"FAILED_PRECONDITION: {message}");
        }
        #endregion
    }
}
