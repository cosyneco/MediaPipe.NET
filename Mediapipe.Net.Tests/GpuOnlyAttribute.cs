// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using NUnit.Framework;

namespace Mediapipe.Net.Tests
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class GpuOnlyAttribute : CategoryAttribute { }
}
