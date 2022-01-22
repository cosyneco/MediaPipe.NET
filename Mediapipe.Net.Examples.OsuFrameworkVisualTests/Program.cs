// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using osu.Framework;
using osu.Framework.Platform;

namespace Mediapipe.Net.Examples.OsuFrameworkVisualTests
{
    public static class Program
    {
        public static void Main()
        {
            using GameHost host = Host.GetSuitableHost("visual-tests");
            using var game = new OsuFrameworkVisualTestsTestBrowser();
            host.Run(game);
        }
    }
}
