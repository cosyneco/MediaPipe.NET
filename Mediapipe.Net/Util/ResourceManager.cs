// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.External;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Util
{
    /// <summary>
    /// Class to manage MediaPipe resources, such as `.tflite` and `.pbtxt` files that it requests.
    /// </summary>
    /// <remarks>
    /// There must not be more than one instance at the same time.
    /// </remarks>
    public unsafe abstract class ResourceManager
    {
        public delegate string PathResolver(string path);
        public abstract PathResolver ResolvePath { get; }

        public delegate Span<byte> ResourceProvider(string path);
        public abstract ResourceProvider ProvideResource { get; }

        private static readonly object initLock = new object();
        private static bool isInitialized = false;

        public ResourceManager()
        {
            lock (initLock)
            {
                if (isInitialized)
                    throw new InvalidOperationException("ResourceManager can be initialized only once");

                SafeNativeMethods.mp__SetCustomGlobalPathResolver__P(ResolvePath);
                SafeNativeMethods.mp__SetCustomGlobalResourceProvider__P(provideResource);
                isInitialized = true;
            }
        }

        private bool provideResource(string path, void* output)
        {
            Span<byte> span = ProvideResource(path);
            if (span.IsEmpty)
                return false;

            fixed (void* spanPtr = span)
            {
                using StdString strOutput = new StdString(output, isOwner: false);
                using StdString strSpan = new StdString(spanPtr, isOwner: true);
                strOutput.Swap(strSpan);
            }

            return true;
        }
    }
}
