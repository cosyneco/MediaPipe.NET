// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace Mediapipe.Net.Examples.OsuFrameworkVisualTests
{
    public class OsuFrameworkVisualTestsGameBase : Game
    {
        private MediapipeDrawable? mediapipeDrawable;
        protected override Container<Drawable> Content { get; }

        private DependencyContainer? dependencies;
        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        protected OsuFrameworkVisualTestsGameBase()
        {
            base.Content.Add(Content = new DrawSizePreservingFillContainer
            {
                TargetDrawSize = new Vector2(1366, 768),
            });
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            mediapipeDrawable = new MediapipeDrawable
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Size = new Vector2(1280, 720),
                FillMode = FillMode.Fit
            };
            dependencies?.Cache(mediapipeDrawable);
        }

        protected override bool OnExiting()
        {
            mediapipeDrawable?.Dispose();
            return base.OnExiting();
        }
    }
}
