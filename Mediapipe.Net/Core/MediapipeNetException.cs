// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;

namespace Mediapipe.Net.Core
{
    public class MediapipeNetException : Exception
    {
        public MediapipeNetException(string message) : base(message) { }
    }
}
