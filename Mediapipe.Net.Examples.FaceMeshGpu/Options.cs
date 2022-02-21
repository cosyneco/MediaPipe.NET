// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using CommandLine;

namespace Mediapipe.Net.Examples.FaceMeshGpu
{
    public class Options
    {
        [Option('c', "camera", Default = 0, HelpText = "The index of the camera to use")]
        public int CameraIndex { get; set; }

        [Option('f', "input-format", Default = null, HelpText = "The input format of the camera to use")]
        public string? InputFormat { get; set; }

        [Option('w', "width", Default = null, HelpText = "The width of the camera to use")]
        public int? Width { get; set; }

        [Option('h', "height", Default = null, HelpText = "The height of the camera to use")]
        public int? Height { get; set; }
    }
}
