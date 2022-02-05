// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using Mediapipe.Net.Core;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.Format
{
    public unsafe class ImageFrame : MpResourceHandle
    {
        public static readonly uint DefaultAlignmentBoundary = 16;
        public static readonly uint GlDefaultAlignmentBoundary = 4;

        public delegate void Deleter(IntPtr ptr);

        public ImageFrame() : base()
        {
            UnsafeNativeMethods.mp_ImageFrame__(out var ptr).Assert();
            Ptr = ptr;
        }

        public ImageFrame(IntPtr imageFramePtr, bool isOwner = true) : base(imageFramePtr, isOwner) { }

        public ImageFrame(ImageFormat format, int width, int height) : this(format, width, height, DefaultAlignmentBoundary) { }

        public ImageFrame(ImageFormat format, int width, int height, uint alignmentBoundary) : base()
        {
            UnsafeNativeMethods.mp_ImageFrame__ui_i_i_ui(format, width, height, alignmentBoundary, out var ptr).Assert();
            Ptr = ptr;
        }

        // NOTE: This byte* was a NativeArray<byte> from Unity.
        // It will naturally translate to C++'s uint8*.
        // The signature on the native side is:
        //
        // MP_CAPI(MpReturnCode) mp_ImageFrame__ui_i_i_i_Pui8_PF(
        //     mediapipe::ImageFormat::Format format,
        //     int width, int height, int width_step, uint8* pixel_data,
        //     Deleter* deleter, mediapipe::ImageFrame** image_frame_out);
        unsafe public ImageFrame(ImageFormat format, int width, int height, int widthStep, byte* pixelData) : base()
        {
            UnsafeNativeMethods.mp_ImageFrame__ui_i_i_i_Pui8_PF(
                format, width, height, widthStep,
                pixelData,
                releasePixelData,
                out var ptr).Assert();
            Ptr = ptr;
        }

        public ImageFrame(ImageFormat format, int width, int height, int widthStep, ReadOnlySpan<byte> pixelData)
            : this(format, width, height, widthStep, spanToBytePtr(pixelData)) { }

        private static byte* spanToBytePtr(ReadOnlySpan<byte> span)
        {
            fixed (byte* ptr = span)
            {
                return ptr;
            }
        }

        protected override void DeleteMpPtr() => UnsafeNativeMethods.mp_ImageFrame__delete(Ptr);

        // [AOT.MonoPInvokeCallback(typeof(Deleter))] (?)
        private static void releasePixelData(IntPtr ptr)
        {
            // Do nothing (pixelData is moved)
        }

        public bool IsEmpty => SafeNativeMethods.mp_ImageFrame__IsEmpty(MpPtr) > 0;

        public bool IsContiguous => SafeNativeMethods.mp_ImageFrame__IsContiguous(MpPtr) > 0;

        public bool IsAligned(uint alignmentBoundary)
        {
            SafeNativeMethods.mp_ImageFrame__IsAligned__ui(MpPtr, alignmentBoundary, out var value).Assert();

            GC.KeepAlive(this);
            return value;
        }

        public ImageFormat Format => SafeNativeMethods.mp_ImageFrame__Format(MpPtr);

        public int Width => SafeNativeMethods.mp_ImageFrame__Width(MpPtr);

        public int Height => SafeNativeMethods.mp_ImageFrame__Height(MpPtr);

        public int ChannelSize
        {
            get
            {
                var code = SafeNativeMethods.mp_ImageFrame__ChannelSize(MpPtr, out var value);

                GC.KeepAlive(this);
                return valueOrFormatException(code, value);
            }
        }

        public int NumberOfChannels
        {
            get
            {
                var code = SafeNativeMethods.mp_ImageFrame__NumberOfChannels(MpPtr, out var value);

                GC.KeepAlive(this);
                return valueOrFormatException(code, value);
            }
        }

        public int ByteDepth
        {
            get
            {
                var code = SafeNativeMethods.mp_ImageFrame__ByteDepth(MpPtr, out var value);

                GC.KeepAlive(this);
                return valueOrFormatException(code, value);
            }
        }

        public int WidthStep => SafeNativeMethods.mp_ImageFrame__WidthStep(MpPtr);

        public IntPtr MutablePixelData => SafeNativeMethods.mp_ImageFrame__MutablePixelData(MpPtr);

        public int PixelDataSize => SafeNativeMethods.mp_ImageFrame__PixelDataSize(MpPtr);

        public int PixelDataSizeStoredContiguously
        {
            get
            {
                var code = SafeNativeMethods.mp_ImageFrame__PixelDataSizeStoredContiguously(MpPtr, out var value);

                GC.KeepAlive(this);
                return valueOrFormatException(code, value);
            }
        }

        public void SetToZero()
        {
            UnsafeNativeMethods.mp_ImageFrame__SetToZero(MpPtr).Assert();
            GC.KeepAlive(this);
        }

        public void SetAlignmentPaddingAreas()
        {
            UnsafeNativeMethods.mp_ImageFrame__SetAlignmentPaddingAreas(MpPtr).Assert();
            GC.KeepAlive(this);
        }

        public byte[] CopyToByteBuffer(int bufferSize)
            => copyToBuffer<byte>(UnsafeNativeMethods.mp_ImageFrame__CopyToBuffer__Pui8_i, bufferSize);

        public ushort[] CopyToUshortBuffer(int bufferSize)
            => copyToBuffer<ushort>(UnsafeNativeMethods.mp_ImageFrame__CopyToBuffer__Pui16_i, bufferSize);

        public float[] CopyToFloatBuffer(int bufferSize)
            => copyToBuffer<float>(UnsafeNativeMethods.mp_ImageFrame__CopyToBuffer__Pf_i, bufferSize);


        /// <summary>
        ///   Get the value of a specific channel only.
        ///   It's useful when only one channel is used (e.g. Hair Segmentation mask).
        /// </summary>
        /// <param name="channelNumber">
        ///   Specify from which channel the data will be retrieved.
        ///   For example, if the format is RGB, 0 means R channel, 1 means G channel, and 2 means B channel.
        /// </param>
        /// <param name="colors" >
        ///   The array to which the output data will be written.
        /// </param>
        public byte[] GetChannel(int channelNumber, bool flipVertically, byte[] colors)
        {
            var format = Format;

            switch (format)
            {
                case ImageFormat.Srgb:
                    if (channelNumber < 0 || channelNumber > 3)
                        throw new ArgumentException($"There are only 3 channels, but No. {channelNumber} is specified");
                    readChannel(MutablePixelData, channelNumber, 3, Width, Height, WidthStep, flipVertically, colors);
                    return colors;
                case ImageFormat.Srgba:
                    if (channelNumber < 0 || channelNumber > 4)
                        throw new ArgumentException($"There are only 4 channels, but No. {channelNumber} is specified");
                    readChannel(MutablePixelData, channelNumber, 4, Width, Height, WidthStep, flipVertically, colors);
                    return colors;
                default:
                    throw new NotImplementedException($"Currently only SRGB and SRGBA format are supported: {format}");
            }
        }

        /// <summary>
        ///   Get the value of a specific channel only.
        ///   It's useful when only one channel is used (e.g. Hair Segmentation mask).
        /// </summary>
        /// <param name="channelNumber">
        ///   Specify from which channel the data will be retrieved.
        ///   For example, if the format is RGB, 0 means R channel, 1 means G channel, and 2 means B channel.
        /// </param>
        public byte[] GetChannel(int channelNumber, bool flipVertically)
            => GetChannel(channelNumber, flipVertically, new byte[Width * Height]);

        private delegate MpReturnCode CopyToBufferHandler<T>(IntPtr ptr, T* buffer, int bufferSize)
            where T : unmanaged;

        private T[] copyToBuffer<T>(CopyToBufferHandler<T> handler, int bufferSize)
            where T : unmanaged
        {
            var buffer = new T[bufferSize];

            unsafe
            {
                fixed (T* bufferPtr = buffer)
                {
                    handler(MpPtr, bufferPtr, bufferSize).Assert();
                }
            }

            GC.KeepAlive(this);
            return buffer;
        }

        private T valueOrFormatException<T>(MpReturnCode code, T value)
        {
            try
            {
                code.Assert();
                return value;
            }
            catch (MediapipeException)
            {
                throw new FormatException($"Invalid image format: {Format}");
            }
        }

        /// <remarks>
        /// In the source array, pixels are laid out left to right, top to bottom,
        /// but in the returned array, left to right, top to bottom.
        /// </remarks>
        private static void readChannel(IntPtr ptr, int channelNumber, int channelCount, int width, int height, int widthStep, bool flipVertically, byte[] colors)
        {
            if (colors.Length != width * height)
                throw new ArgumentException("colors length is invalid");
            var padding = widthStep - channelCount * width;

            unsafe
            {
                fixed (byte* dest = colors)
                {
                    byte* pSrc = (byte*)ptr;
                    pSrc += channelNumber;

                    if (flipVertically)
                    {
                        var pDest = dest + colors.Length - 1;

                        for (int i = 0; i < height; i++)
                        {
                            for (int j = 0; j < width; j++)
                            {
                                *pDest-- = *pSrc;
                                pSrc += channelCount;
                            }
                            pSrc += padding;
                        }
                    }
                    else
                    {
                        var pDest = dest + width * (height - 1);

                        for (int i = 0; i < height; i++)
                        {
                            for (int j = 0; j < width; j++)
                            {
                                *pDest++ = *pSrc;
                                pSrc += channelCount;
                            }
                            pSrc += padding;
                            pDest -= 2 * width;
                        }
                    }
                }
            }
        }
    }
}
