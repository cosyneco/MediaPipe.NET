// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using CommandLine;
using Mediapipe.Net.Calculators;
using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using SeeShark;
using SeeShark.Device;

namespace Mediapipe.Net.Examples.OsuFrameworkVisualTests
{
    public class OsuFrameworkVisualTestsGameBase : Game
    {
        private Camera? camera;
        private FrameConverter? converter;
        private FaceMeshCpuCalculator? calculator;

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

#pragma warning disable IDE0051
        [BackgroundDependencyLoader]
        private void load()
        {
            string[] args = Environment.GetCommandLineArgs();
            Options parsedArgs = Parser.Default.ParseArguments<Options>(args).Value;

            (int, int)? videoSize = null;
            if (parsedArgs.Width != null && parsedArgs.Height != null)
                videoSize = ((int)parsedArgs.Width, (int)parsedArgs.Height);
            else if (parsedArgs.Width != null && parsedArgs.Height == null)
                Console.Error.WriteLine("Specifying width requires to specify height");
            else if (parsedArgs.Width == null && parsedArgs.Height != null)
                Console.Error.WriteLine("Specifying height requires to specify width");

            var manager = new CameraManager();
            camera = manager.GetDevice(parsedArgs.CameraIndex,
                new VideoInputOptions
                {
                    InputFormat = parsedArgs.InputFormat,
                    VideoSize = videoSize,
                });
            dependencies?.Cache(camera);
            manager.Dispose();

            var dummyFrame = camera.GetFrame();
            converter = new FrameConverter(dummyFrame, PixelFormat.Rgba);
            dependencies?.Cache(converter);

            calculator = new FaceMeshCpuCalculator();
            calculator.Run();
            dependencies?.Cache(calculator);
        }
#pragma warning restore IDE0051

        protected override bool OnExiting()
        {
            calculator?.Dispose();
            converter?.Dispose();
            camera?.Dispose();
            return base.OnExiting();
        }
    }
}
