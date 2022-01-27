// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

namespace Mediapipe.Net.Util
{
    internal unsafe static class UnsafeUtil
    {
        public static T[] SafeArrayCopy<T>(T* ptr, int length)
            where T : unmanaged
        {
            T[] array = new T[length];
            for (int i = 0; i < array.Length; i++)
                array[i] = ptr[i];
            return array;
        }
    }
}
