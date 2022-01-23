// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Framework.Format;

namespace Mediapipe.Net.Calculators
{
    public interface ICalculator<T> : IDisposable
    {
        public void Run();

        public ImageFrame Send(ImageFrame frame);

        public event EventHandler<T> OnResult;
        
        public long CurrentFrame { get; }
    }
}
