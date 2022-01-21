// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;

namespace Mediapipe.Net.Calculators
{
    public interface ICalculator<U, T> : IDisposable
    {
        public void Run();
        public U Send(U frame);

        public event EventHandler<T> OnResult;

        public long CurrentFrame { get; }
    }
}
