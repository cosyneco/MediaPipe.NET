// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;

namespace Mediapipe.Net.Examples.OsuFrameworkVisualTests.Visual
{
    public class TestSceneFaceMeshCpu : OsuFrameworkVisualTestsTestScene
    {
        [BackgroundDependencyLoader]
        private void load(MediapipeDrawable mediapipeDrawable)
        {
            Add(new Container
            {
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = Color4Extensions.FromHex(@"272727"),
                    },
                    mediapipeDrawable,
                },
            });
            mediapipeDrawable.Start();
        }
    }
}
