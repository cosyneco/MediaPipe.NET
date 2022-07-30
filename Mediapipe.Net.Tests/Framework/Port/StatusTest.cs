// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Core;
using Mediapipe.Net.Framework.Port;
using NUnit.Framework;

namespace Mediapipe.Net.Tests.Framework.Port
{
    public class StatusTest
    {
        #region Code
        [Test]
        public void Code_ShouldReturnStatusCode_When_StatusIsOk()
        {
            using var status = Status.Ok();
            Assert.AreEqual(Status.StatusCode.Ok, status.Code);
        }

        [Test]
        public void Code_ShouldReturnStatusCode_When_StatusIsFailedPrecondition()
        {
            using var status = Status.FailedPrecondition();
            Assert.AreEqual(Status.StatusCode.FailedPrecondition, status.Code);
        }
        #endregion

        #region IsDisposed
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

        #region RawCode
        [Test]
        public void RawCode_ShouldReturnRawCode_When_StatusIsOk()
        {
            using var status = Status.Ok();
            Assert.AreEqual(0, status.RawCode);
        }

        [Test]
        public void RawCode_ShouldReturnRawCode_When_StatusIsFailedPrecondition()
        {
            using var status = Status.FailedPrecondition();
            Assert.AreEqual(9, status.RawCode);
        }
        #endregion

        #region Ok
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

        #region AssertOk
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

        #region ToString
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
            Assert.AreEqual($"FAILED_PRECONDITION: {message}", status.ToString());
        }

        [Test]
        public void ToString_ShouldReturnMessage_When_StatusIsAborted()
        {
            var message = "Some error";
            using Status status = Status.Aborted(message);
            Assert.AreEqual($"ABORTED: {message}", status.ToString());
        }

        [Test]
        public void ToString_ShouldReturnMessage_When_StatusIsOutOfRange()
        {
            string message = "Some error";
            using Status status = Status.OutOfRange(message);
            Assert.AreEqual($"OUT_OF_RANGE: {message}", status.ToString());
        }

        [Test]
        public void ToString_ShouldReturnMessage_When_StatusIsUnimplemented()
        {
            var message = "Some error";
            using Status status = Status.Unimplemented(message);
            Assert.AreEqual($"UNIMPLEMENTED: {message}", status.ToString());
        }

        [Test]
        public void ToString_ShouldReturnMessage_When_StatusIsInternal()
        {
            var message = "Some error";
            using Status status = Status.Internal(message);
            Assert.AreEqual($"INTERNAL: {message}", status.ToString());
        }

        [Test]
        public void ToString_ShouldReturnMessage_When_StatusIsUnavailable()
        {
            var message = "Some error";
            using Status status = Status.Unavailable(message);
            Assert.AreEqual($"UNAVAILABLE: {message}", status.ToString());
        }

        [Test]
        public void ToString_ShouldReturnMessage_When_StatusIsDataLoss()
        {
            var message = "Some error";
            using Status status = Status.DataLoss(message);
            Assert.AreEqual($"DATA_LOSS: {message}", status.ToString());
        }

        [Test]
        public void ToString_ShouldReturnMessage_When_StatusIsUnauthenticated()
        {
            var message = "Some error";
            using Status status = Status.Unauthenticated(message);
            Assert.AreEqual($"UNAUTHENTICATED: {message}", status.ToString());
        }
    }
    #endregion
}
