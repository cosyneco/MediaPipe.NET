// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Runtime.InteropServices;
using Mediapipe.Net.Native;
using static Mediapipe.Net.Native.MpReturnCodeExtension;

namespace Mediapipe.Net.Core
{
    public abstract class MpResourceHandle : Disposable, IMpResourceHandle
    {
        protected IntPtr Ptr;

        protected MpResourceHandle(bool isOwner = true) : this(IntPtr.Zero, isOwner) { }

        protected MpResourceHandle(IntPtr ptr, bool isOwner = true) : base(isOwner)
        {
            Ptr = ptr;
        }

        #region IMpResourceHandle
        public IntPtr MpPtr
        {
            get
            {
                ThrowIfDisposed();
                return Ptr;
            }
        }

        public void ReleaseMpResource()
        {
            if (OwnsResource)
                DeleteMpPtr();

            TransferOwnership();
        }

        public bool OwnsResource => IsOwner && Ptr != IntPtr.Zero;
        #endregion

        protected override void DisposeUnmanaged()
        {
            if (OwnsResource)
                DeleteMpPtr();

            ReleaseMpPtr();
            base.DisposeUnmanaged();
        }

        /// <summary>
        /// Forgets the pointer address.
        /// After calling this method, <see ref="OwnsResource" /> will return false.
        /// </summary>
        protected void ReleaseMpPtr() => Ptr = IntPtr.Zero;

        /// <summary>
        /// Release the memory (call `delete` or `delete[]`) whether or not it owns it.
        /// </summary>
        /// <remarks>In most cases, this method should not be called directly.</remarks>
        protected abstract void DeleteMpPtr();

        protected delegate MpReturnCode StringOutFunc(IntPtr ptr, out IntPtr strPtr);
        protected string? MarshalStringFromNative(StringOutFunc func)
        {
            func(MpPtr, out IntPtr strPtr).Assert();
            GC.KeepAlive(this);

            string? str = Marshal.PtrToStringAnsi(strPtr);
            UnsafeNativeMethods.delete_array__PKc(strPtr);

            return str;
        }
    }
}
