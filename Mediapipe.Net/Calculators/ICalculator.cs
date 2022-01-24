// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Framework.Format;

namespace Mediapipe.Net.Calculators
{
    /// <summary>
    /// This interface allows for a simple use of any MediaPipe calculator.
    /// </summary>
    /// <typeparam name="T">The landmarks return type of the calculator graph.</typeparam>
    public interface ICalculator<T> : IDisposable
    {
        /// <summary>
        /// Starts the calculator.
        /// </summary>
        public void Run();

        /// <summary>
        /// Sends an <see cref="ImageFrame"/> to MediaPipe to process.
        /// </summary>
        /// <remarks>If the input <see cref="ImageFrame"/> doesn't get disposed, MediaPipe will crash.</remarks>
        /// <param name="frame">The frame that MediaPipe should process.</param>
        /// <returns>An <see cref="ImageFrame"/> with the contents of the source <see cref="ImageFrame"/> and the landmarks drawn.</returns>
        public ImageFrame Send(ImageFrame frame);

        /// <summary>
        /// This event triggers every time MediaPipe is able to detect landmarks on a given <see cref="ImageFrame"/>.
        /// </summary>
        public event EventHandler<T> OnResult;

        /// <summary>
        /// The current processed frame number.
        /// </summary>
        public long CurrentFrame { get; }
    }
}
