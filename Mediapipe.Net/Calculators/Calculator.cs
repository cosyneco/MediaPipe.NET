// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.IO;
using System.Runtime.InteropServices;
using Mediapipe.Net.Core;
using Mediapipe.Net.Framework;
using Mediapipe.Net.Framework.Format;
using Mediapipe.Net.Framework.Packets;
using Mediapipe.Net.Framework.Port;

namespace Mediapipe.Net.Calculators
{
    /// <summary>
    /// The base for any calculator.
    /// </summary>
    public abstract class Calculator : Disposable
    {
        protected const string INPUT_VIDEO_STREAM = "input_video";
        protected const string OUTPUT_VIDEO_STREAM = "output_video";

        protected readonly string GraphPath;
        protected readonly string? SecondaryOutputStream;

        protected readonly CalculatorGraph Graph;
        protected SidePackets? SidePackets { get; set; }
        private GCHandle? observeStreamHandle;

        /// <summary>
        /// Triggered every time the calculator returns a secondary output.
        /// </summary>
        public event EventHandler<Packet>? OnResult;

        protected Calculator(string graphPath, string? secondaryOutputStream = null)
        {
            GraphPath = graphPath;
            SecondaryOutputStream = secondaryOutputStream;

            Graph = new CalculatorGraph(File.ReadAllText(GraphPath));

            if (SecondaryOutputStream != null)
            {
                Graph.ObserveOutputStream(SecondaryOutputStream, (packet) =>
                {
                    if (packet == null)
                        return Status.Ok();

                    OnResult?.Invoke(this, packet);
                    return Status.Ok();
                }, out GCHandle handle).AssertOk();
                observeStreamHandle = handle;
            }
        }

        /// <summary>
        /// Starts the calculator.
        /// </summary>
        /// <remarks>You need to call this method before sending frames to it.</remarks>
        public void Run() => Graph.StartRun(SidePackets).AssertOk();

        protected abstract void SendFrame(ImageFrame frame);

        /// <summary>
        /// Sends an <see cref="ImageFrame"/> for the calculator to process.
        /// </summary>
        /// <remarks>If the input <see cref="ImageFrame"/> doesn't get disposed after being sent, MediaPipe will crash.</remarks>
        /// <param name="frame">The frame that MediaPipe should process.</param>
        /// <param name="disposeSourceFrame">Whether or not to dispose the source frame.</param>
        public void Send(ImageFrame frame, bool disposeSourceFrame = true)
        {
            lock (frame)
            {
                SendFrame(frame);
                CurrentFrame++;
                if (disposeSourceFrame)
                    frame.Dispose();
            }
        }

        /// <summary>
        /// The number of the current processed frame.
        /// </summary>
        public long CurrentFrame { get; private set; } = 0;

        protected override void DisposeManaged()
        {
            Graph.CloseInputStream(INPUT_VIDEO_STREAM);
            Graph.WaitUntilDone();
            Graph.Dispose();

            observeStreamHandle?.Free();
        }
    }
}
