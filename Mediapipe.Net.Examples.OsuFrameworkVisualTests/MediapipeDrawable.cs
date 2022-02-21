// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Calculators;
using Mediapipe.Net.Framework.Format;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using SeeShark;
using SeeShark.Decode;
using SeeShark.Device;
using SixLabors.ImageSharp.PixelFormats;
using Image = SixLabors.ImageSharp.Image;

namespace Mediapipe.Net.Examples.OsuFrameworkVisualTests
{
    public class MediapipeDrawable : CompositeDrawable
    {
        private Camera? camera;
        private FrameConverter? converter;
        private FaceMeshCpuCalculator? calculator;

        private readonly Sprite sprite;
        private Texture? texture;

        public MediapipeDrawable()
        {
            Masking = true;
            CornerRadius = 10;
            AddInternal(sprite = new Sprite
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                FillMode = FillMode.Fit,
                FillAspectRatio = 1,
            });
        }

#pragma warning disable IDE0051
        [BackgroundDependencyLoader]
        private void load(Camera camera, FrameConverter converter, FaceMeshCpuCalculator calculator)
        {
            this.camera = camera;
            this.converter = converter;
            this.calculator = calculator;
        }
#pragma warning restore IDE0051

        protected override unsafe void Update()
        {
            base.Update();
            if (camera == null || converter == null || calculator == null)
                return;

            if (camera.TryGetFrame(out Frame frame) != DecodeStatus.NewFrame)
                return;

            Frame cFrame = converter.Convert(frame);
            ImageFrame imgFrame = new ImageFrame(ImageFormat.Srgba,
                cFrame.Width, cFrame.Height, cFrame.WidthStep, cFrame.RawData);
            using ImageFrame outImgFrame = calculator.Send(imgFrame);
            imgFrame.Dispose();

            var span = new ReadOnlySpan<byte>(outImgFrame.MutablePixelData, outImgFrame.Height * outImgFrame.WidthStep);
            var pixelData = Image.LoadPixelData<Rgba32>(span, cFrame.Width, cFrame.Height);

            texture ??= new Texture(cFrame.Width, cFrame.Height);
            texture.SetData(new TextureUpload(pixelData));
            sprite.FillAspectRatio = (float)cFrame.Width / cFrame.Height;
            sprite.Texture = texture;
        }
    }
}
