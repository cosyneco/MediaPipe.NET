// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.IO;
using Mediapipe.Net.Calculators;
using Mediapipe.Net.Framework.Format;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using SeeShark;
using SeeShark.FFmpeg;
using SixLabors.ImageSharp.PixelFormats;

namespace Mediapipe.Net.Examples.OsuFrameworkVisualTests;

public class MediapipeDrawable : CompositeDrawable
{
    private Camera camera;
    private FrameConverter converter;
    private FaceMeshCpuCalculator calculator;

    private Sprite sprite;
    private Texture texture;

    public MediapipeDrawable(int cameraIndex = 0)
    {
        var manager = new CameraManager();
        camera = manager.GetCamera(cameraIndex);

        manager.Dispose();

        camera.OnFrame += CameraOnOnFrame;
        calculator = new FaceMeshCpuCalculator();

        AddInternal(sprite = new Sprite
        {
            RelativeSizeAxes = Axes.Both,
            FillMode = FillMode.Fit,
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            FillAspectRatio = 1
        });
        Start();
    }

    public void Start()
    {
        calculator.Run();
        camera.StartCapture();
    }

    private unsafe void CameraOnOnFrame(object? sender, FrameEventArgs e)
    {
        if (calculator == null)
            return;

        var frame = e.Frame;
        if (converter == null)
            converter = new FrameConverter(frame, PixelFormat.Rgba);

        // Don't use a frame if it's not new
        if (e.Status != DecodeStatus.NewFrame)
            return;

        Frame cFrame = converter.Convert(frame);

        ImageFrame imgframe;
        fixed (byte* rawDataPtr = cFrame.RawData)
        {
            imgframe = new ImageFrame(ImageFormat.Srgba, cFrame.Width, cFrame.Height, cFrame.WidthStep,
                rawDataPtr);
        }

        ImageFrame img = calculator.Send(imgframe);
        byte[] data = img.CopyToByteBuffer(img.Height * img.WidthStep);
        img.Dispose();
        var pixelData = SixLabors.ImageSharp.Image.LoadPixelData<Rgba32>(data, cFrame.Width, cFrame.Height);

        texture ??= new Texture(cFrame.Width, cFrame.Height);
        texture.SetData(new TextureUpload(pixelData));
        sprite.FillAspectRatio = (float)cFrame.Width / cFrame.Height;
        sprite.Texture = texture;
    }

    public new void Dispose()
    {
        camera.Dispose();
        calculator.Dispose();
        sprite.Dispose();
        base.Dispose();
    }
}
