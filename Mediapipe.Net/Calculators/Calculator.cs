// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.IO;
using System.Runtime.InteropServices;
using Mediapipe.Net.Core;
using Mediapipe.Net.Framework;
using Mediapipe.Net.Framework.Format;
using Mediapipe.Net.Framework.Packet;
using Mediapipe.Net.Framework.Port;

namespace Mediapipe.Net.Calculators
{
    /// <summary>
    /// The base for any calculator.
    /// </summary>
    /// <typeparam name="TI">The type of
    /// <typeparam name="TPacket">The type of packet the calculator returns the secondary output in.</typeparam>
    /// <typeparam name="T">The type of secondary output.</typeparam>
    public abstract class Calculator<TPacket, T> : Disposable, ICalculator<T>
        where TPacket : Packet<T>
    {
        protected const string INPUT_VIDEO_STREAM = "input_video";
        protected const string OUTPUT_VIDEO_STREAM = "output_video";

        protected readonly string GraphPath;
        protected readonly string? SecondaryOutputStream;

        protected readonly CalculatorGraph Graph;
        private GCHandle observeStreamHandle;

        public event EventHandler<T>? OnResult;

        protected Calculator(string graphPath, string? secondaryOutputStream = null)
        {
            GraphPath = graphPath;
            SecondaryOutputStream = secondaryOutputStream;

            Graph = new CalculatorGraph(File.ReadAllText(GraphPath));

            if (SecondaryOutputStream != null)
            {
                Graph.ObserveOutputStream<TPacket, T>(SecondaryOutputStream, (packet) =>
                {
                    if (packet == null)
                        return Status.Ok();

                    T secondaryOutput = packet.Get();
                    OnResult?.Invoke(this, secondaryOutput);
                    return Status.Ok();
                }, out observeStreamHandle).AssertOk();
            }
        }

        public void Run() => Graph.StartRun().AssertOk();

        protected abstract ImageFrame SendFrame(ImageFrame frame);

        public ImageFrame Send(ImageFrame frame)
        {
            ImageFrame outFrame = SendFrame(frame);
            CurrentFrame++;
            return outFrame;
        }

        public long CurrentFrame { get; private set; } = 0;

        protected override void DisposeManaged()
        {
            Graph.CloseInputStream(INPUT_VIDEO_STREAM);
            Graph.WaitUntilDone();
            Graph.Dispose();

            observeStreamHandle.Free();
        }
    }
}
