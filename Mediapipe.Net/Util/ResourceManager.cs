// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Collections;
using System.IO;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Util
{
    /// <summary>
    /// Class to manage assets that MediaPipe accesses.
    /// </summary>
    /// <remarks>
    /// There must not be more than one instance at the same time.
    /// </remarks>
    public unsafe abstract class ResourceManager
    {
        public delegate string PathResolver(string path);
        public abstract PathResolver ResolvePath { get; }
        public delegate bool ResourceProvider(string path, IntPtr output);
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
                SafeNativeMethods.mp__SetCustomGlobalResourceProvider__P(ProvideResource);
                isInitialized = true;
            }
        }

        /// <param name="name">Asset name</param>
        /// <returns>
        ///   Returns true if <paramref name="name" /> is already prepared (saved locally on the device).
        /// </returns>
        public abstract bool IsPrepared(string name);

        /// <summary>
        ///   Saves <paramref name="name" /> as <paramref name="uniqueKey" /> asynchronously.
        /// </summary>
        /// <param name="overwrite">
        ///   Specifies whether <paramref name="uniqueKey" /> will be overwritten if it already exists.
        /// </param>
        public abstract IEnumerator PrepareAssetAsync(string name, string uniqueKey, bool overwrite = true);

        public IEnumerator PrepareAssetAsync(string name, bool overwrite = true)
            => PrepareAssetAsync(name, name, overwrite);

        protected static string GetAssetNameFromPath(string assetPath)
        {
            var assetName = Path.GetFileNameWithoutExtension(assetPath);
            var extension = Path.GetExtension(assetPath);

            switch (extension)
            {
                case ".binarypb":
                case ".tflite":
                    return $"{assetName}.bytes";
                case ".pbtxt":
                    return $"{assetName}.txt";
                default:
                    return $"{assetName}{extension}";
            }
        }
    }
}
