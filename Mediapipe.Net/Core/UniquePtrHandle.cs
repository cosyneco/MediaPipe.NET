// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;

namespace Mediapipe.Net.Core;

public abstract class UniquePtrHandle : MpResourceHandle
{
    protected UniquePtrHandle(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

    /// <returns>The owning pointer</returns>
    public abstract IntPtr Get();

    /// <summary>Release the owning pointer</summary>
    public abstract IntPtr Release();
}
