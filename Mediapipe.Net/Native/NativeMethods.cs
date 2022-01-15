// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

namespace Mediapipe.Net.Native
{
    // TODO: (message to future self) Beware of CharSet.Unicode!
    // If tests fail because of odd string issues, try to marshal strings according to
    // https://docs.microsoft.com/en-us/dotnet/standard/native-interop/type-marshaling.

    /// <summary>
    /// Contains all the directly bound native methods from Mediapipe.
    /// </summary>
    public class NativeMethods
    {
        /// <summary>
        /// Name of the mediapipe shared library.
        /// </summary>
        internal const string MEDIAPIPE_LIBRARY = "mediapipe_c";
    }
}
