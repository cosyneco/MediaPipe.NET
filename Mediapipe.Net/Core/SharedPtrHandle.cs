// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

namespace Mediapipe.Net.Core
{
    public unsafe abstract class SharedPtrHandle : MpResourceHandle
    {
        protected SharedPtrHandle(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        /// <returns>The owning pointer</returns>
        public abstract IntPtr Get();

        /// <summary>Release the owning pointer</summary>
        public abstract void Reset();
    }
}
