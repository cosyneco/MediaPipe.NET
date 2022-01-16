// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

namespace Mediapipe.Net.External
{
    internal static class Protobuf
    {
        public delegate void ProtobufLogHandler(int level, string filename, int line, string message);
        // TODO: (from homuler) Overwrite protobuf logger to show logs in Console Window.
    }
}
