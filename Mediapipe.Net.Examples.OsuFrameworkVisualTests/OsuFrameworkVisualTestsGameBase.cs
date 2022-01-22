// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace Mediapipe.Net.Examples.OsuFrameworkVisualTests
{
    public class OsuFrameworkVisualTestsGameBase : osu.Framework.Game
    {
        protected override Container<Drawable> Content { get; }

        protected OsuFrameworkVisualTestsGameBase()
        {
            base.Content.Add(Content = new DrawSizePreservingFillContainer
            {
                TargetDrawSize = new Vector2(1366, 768),
            });
        }
    }
}
