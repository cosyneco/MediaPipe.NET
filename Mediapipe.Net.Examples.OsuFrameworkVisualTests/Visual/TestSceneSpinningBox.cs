// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace Mediapipe.Net.Examples.OsuFrameworkVisualTests.Visual
{
    public class TestSceneSpinningBox : OsuFrameworkVisualTestsTestScene
    {
        public TestSceneSpinningBox()
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
                    new SpinningBox
                    {
                        Anchor = Anchor.Centre,
                        Size = new Vector2(200, 200),
                    },
                }
            });
        }
    }
}
