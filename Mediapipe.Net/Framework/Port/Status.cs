// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Core;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.Port
{
    public unsafe class Status : MpResourceHandle
    {
        public enum StatusCode : int
        {
            Ok = 0,
            Cancelled = 1,
            Unknown = 2,
            InvalidArgument = 3,
            DeadlineExceeded = 4,
            NotFound = 5,
            AlreadyExists = 6,
            PermissionDenied = 7,
            ResourceExhausted = 8,
            FailedPrecondition = 9,
            Aborted = 10,
            OutOfRange = 11,
            Unimplemented = 12,
            Internal = 13,
            Unavailable = 14,
            DataLoss = 15,
            Unauthenticated = 16,
        }

        public Status(void* ptr, bool isOwner = true) : base(ptr, isOwner) { }

        protected override void DeleteMpPtr() => UnsafeNativeMethods.absl_Status__delete(Ptr);

        private bool? ok;
        private int? rawCode;

        public void AssertOk()
        {
            if (!Ok())
                throw new MediapipeException(ToString() ?? "");
        }

        public bool Ok()
        {
            if (ok is bool valueOfOk)
                return valueOfOk;
            ok = SafeNativeMethods.absl_Status__ok(MpPtr);
            return (bool)ok;
        }

        public StatusCode Code => (StatusCode)RawCode;

        public int RawCode
        {
            get
            {
                if (rawCode is int valueOfRawCode)
                    return valueOfRawCode;

                rawCode = SafeNativeMethods.absl_Status__raw_code(MpPtr);
                return (int)rawCode;
            }
        }

        public override string? ToString() => MarshalStringFromNative(UnsafeNativeMethods.absl_Status__ToString);

        public static Status Build(StatusCode code, string message, bool isOwner = true)
        {
            UnsafeNativeMethods.absl_Status__i_PKc((int)code, message, out var ptr).Assert();

            return new Status(ptr, isOwner);
        }

        public static Status Ok(bool isOwner = true) => Build(StatusCode.Ok, "", isOwner);

        public static Status FailedPrecondition(string message = "", bool isOwner = true)
            => Build(StatusCode.FailedPrecondition, message, isOwner);
    }
}
