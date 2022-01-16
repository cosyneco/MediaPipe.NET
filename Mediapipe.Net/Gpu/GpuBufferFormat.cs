// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Framework.Format;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Gpu
{
    public enum GpuBufferFormat : uint
    {
        KUnknown = 0,
        KBgra32 = ('B' << 24) + ('G' << 16) + ('R' << 8) + 'A',
        kGrayFloat32 = ('L' << 24) + ('0' << 16) + ('0' << 8) + 'f',
        kGrayHalf16 = ('L' << 24) + ('0' << 16) + ('0' << 8) + 'h',
        kOneComponent8 = ('L' << 24) + ('0' << 16) + ('0' << 8) + '8',
        kTwoComponentHalf16 = ('2' << 24) + ('C' << 16) + ('0' << 8) + 'h',
        kTwoComponentFloat32 = ('2' << 24) + ('C' << 16) + ('0' << 8) + 'f',
        kBiPlanar420YpCbCr8VideoRange = ('4' << 24) + ('2' << 16) + ('0' << 8) + 'v',
        kBiPlanar420YpCbCr8FullRange = ('4' << 24) + ('2' << 16) + ('0' << 8) + 'f',
        kRgb24 = 0x00000018,  // Note: prefer Bgra32 whenever possible.
        kRgbaHalf64 = ('R' << 24) + ('G' << 16) + ('h' << 8) + 'A',
        kRgbaFloat128 = ('R' << 24) + ('G' << 16) + ('f' << 8) + 'A',
    }

    public static class GpuBufferFormatExtension
    {
        public static ImageFormat ImageFormatFor(this GpuBufferFormat gpuBufferFormat)
        {
            return SafeNativeMethods.mp__ImageFormatForGpuBufferFormat__ui(gpuBufferFormat);
        }

        public static GlTextureInfo GlTextureInfoFor(this GpuBufferFormat gpuBufferFormat, int plane, GlVersion glVersion = GlVersion.KGles3)
        {
            UnsafeNativeMethods.mp__GlTextureInfoForGpuBufferFormat__ui_i_ui(gpuBufferFormat, plane, glVersion, out var glTextureInfo).Assert();
            return glTextureInfo;
        }
    }
}