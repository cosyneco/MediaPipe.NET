// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Runtime.InteropServices;

using Google.Protobuf;
using Mediapipe.Net.Core;
using Mediapipe.Net.Framework.Packet;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Framework.Protobuf;
using Mediapipe.Net.Gpu;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework
{
    public unsafe class CalculatorGraph : MpResourceHandle
    {
        public delegate void* NativePacketCallback(void* graphPtr, void* packetPtr);
        public delegate Status PacketCallback<TPacket, TValue>(TPacket? packet) where TPacket : Packet<TValue>;

        public CalculatorGraph() : base()
        {
            UnsafeNativeMethods.mp_CalculatorGraph__(out var ptr).Assert();
            Ptr = ptr;
        }

        public CalculatorGraph(string textFormatConfig) : base()
        {
            UnsafeNativeMethods.mp_CalculatorGraph__PKc(textFormatConfig, out var ptr).Assert();
            Ptr = ptr;
        }

        public CalculatorGraph(byte[] serializedConfig) : base()
        {
            UnsafeNativeMethods.mp_CalculatorGraph__PKc_i(serializedConfig, serializedConfig.Length, out var ptr).Assert();
            Ptr = ptr;
        }

        public CalculatorGraph(CalculatorGraphConfig config) : this(config.ToByteArray()) { }

        protected override void DeleteMpPtr() => UnsafeNativeMethods.mp_CalculatorGraph__delete(Ptr);

        public Status Initialize(CalculatorGraphConfig config)
        {
            var bytes = config.ToByteArray();
            UnsafeNativeMethods.mp_CalculatorGraph__Initialize__PKc_i(MpPtr, bytes, bytes.Length, out var statusPtr).Assert();

            GC.KeepAlive(this);
            return new Status(statusPtr);
        }

        public Status Initialize(CalculatorGraphConfig config, SidePacket sidePacket)
        {
            var bytes = config.ToByteArray();
            UnsafeNativeMethods.mp_CalculatorGraph__Initialize__PKc_i_Rsp(MpPtr, bytes, bytes.Length, sidePacket.MpPtr, out var statusPtr).Assert();

            GC.KeepAlive(this);
            return new Status(statusPtr);
        }

        /// <remarks>Crashes if config is not set</remarks>
        public CalculatorGraphConfig Config()
        {
            UnsafeNativeMethods.mp_CalculatorGraph__Config(MpPtr, out var serializedProto).Assert();
            GC.KeepAlive(this);

            var config = serializedProto.Deserialize(CalculatorGraphConfig.Parser);
            serializedProto.Dispose();

            return config;
        }

        public Status ObserveOutputStream(string streamName, NativePacketCallback nativePacketCallback, bool observeTimestampBounds = false)
        {
            UnsafeNativeMethods.mp_CalculatorGraph__ObserveOutputStream__PKc_PF_b(MpPtr, streamName, nativePacketCallback, observeTimestampBounds, out var statusPtr).Assert();

            GC.KeepAlive(this);
            return new Status(statusPtr);
        }

        public Status ObserveOutputStream<TPacket, TValue>(string streamName, PacketCallback<TPacket, TValue> packetCallback, bool observeTimestampBounds, out GCHandle callbackHandle) where TPacket : Packet<TValue>
        {
            NativePacketCallback nativePacketCallback = (_, packetPtr) =>
            {
                Status status;
                try
                {
                    var packet = (TPacket?)Activator.CreateInstance(typeof(TPacket), (IntPtr)packetPtr, false);
                    status = packetCallback(packet);
                    packet?.Dispose();
                }
                catch (Exception e)
                {
                    status = Status.FailedPrecondition(e.ToString());
                }
                return status.MpPtr;
            };
            callbackHandle = GCHandle.Alloc(nativePacketCallback, GCHandleType.Normal);

            return ObserveOutputStream(streamName, nativePacketCallback, observeTimestampBounds);
        }

        public Status ObserveOutputStream<TPacket, TValue>(string streamName, PacketCallback<TPacket, TValue> packetCallback, out GCHandle callbackHandle) where TPacket : Packet<TValue>
            => ObserveOutputStream(streamName, packetCallback, false, out callbackHandle);

        public StatusOrPoller<T> AddOutputStreamPoller<T>(string streamName, bool observeTimestampBounds = false)
        {
            UnsafeNativeMethods.mp_CalculatorGraph__AddOutputStreamPoller__PKc_b(MpPtr, streamName, observeTimestampBounds, out var statusOrPollerPtr).Assert();

            GC.KeepAlive(this);
            return new StatusOrPoller<T>(statusOrPollerPtr);
        }

        public Status Run() => Run(new SidePacket());

        public Status Run(SidePacket sidePacket)
        {
            UnsafeNativeMethods.mp_CalculatorGraph__Run__Rsp(MpPtr, sidePacket.MpPtr, out var statusPtr).Assert();

            GC.KeepAlive(sidePacket);
            GC.KeepAlive(this);
            return new Status(statusPtr);
        }

        public Status StartRun() => StartRun(new SidePacket());

        public Status StartRun(SidePacket sidePacket)
        {
            UnsafeNativeMethods.mp_CalculatorGraph__StartRun__Rsp(MpPtr, sidePacket.MpPtr, out var statusPtr).Assert();

            GC.KeepAlive(sidePacket);
            GC.KeepAlive(this);
            return new Status(statusPtr);
        }

        public Status WaitUntilIdle()
        {
            UnsafeNativeMethods.mp_CalculatorGraph__WaitUntilIdle(MpPtr, out var statusPtr).Assert();

            GC.KeepAlive(this);
            return new Status(statusPtr);
        }

        public Status WaitUntilDone()
        {
            UnsafeNativeMethods.mp_CalculatorGraph__WaitUntilDone(MpPtr, out var statusPtr).Assert();

            GC.KeepAlive(this);
            return new Status(statusPtr);
        }

        public bool HasError() => SafeNativeMethods.mp_CalculatorGraph__HasError(MpPtr) > 0;

        public Status AddPacketToInputStream<T>(string streamName, Packet<T> packet)
        {
            UnsafeNativeMethods.mp_CalculatorGraph__AddPacketToInputStream__PKc_Ppacket(MpPtr, streamName, packet.MpPtr, out var statusPtr).Assert();
            packet.Dispose(); // respect move semantics

            GC.KeepAlive(this);
            return new Status(statusPtr);
        }

        public Status SetInputStreamMaxQueueSize(string streamName, int maxQueueSize)
        {
            UnsafeNativeMethods.mp_CalculatorGraph__SetInputStreamMaxQueueSize__PKc_i(MpPtr, streamName, maxQueueSize, out var statusPtr).Assert();

            GC.KeepAlive(this);
            return new Status(statusPtr);
        }

        public Status CloseInputStream(string streamName)
        {
            UnsafeNativeMethods.mp_CalculatorGraph__CloseInputStream__PKc(MpPtr, streamName, out var statusPtr).Assert();

            GC.KeepAlive(this);
            return new Status(statusPtr);
        }

        public Status CloseAllPacketSources()
        {
            UnsafeNativeMethods.mp_CalculatorGraph__CloseAllPacketSources(MpPtr, out var statusPtr).Assert();

            GC.KeepAlive(this);
            return new Status(statusPtr);
        }

        public void Cancel()
        {
            UnsafeNativeMethods.mp_CalculatorGraph__Cancel(MpPtr).Assert();
            GC.KeepAlive(this);
        }

        public bool GraphInputStreamsClosed() => SafeNativeMethods.mp_CalculatorGraph__GraphInputStreamsClosed(MpPtr) > 0;

        public bool IsNodeThrottled(int nodeId) => SafeNativeMethods.mp_CalculatorGraph__IsNodeThrottled__i(MpPtr, nodeId) > 0;

        public bool UnthrottleSources() => SafeNativeMethods.mp_CalculatorGraph__UnthrottleSources(MpPtr) > 0;

        public GpuResources GetGpuResources()
        {
            UnsafeNativeMethods.mp_CalculatorGraph__GetGpuResources(MpPtr, out var gpuResourcesPtr).Assert();

            GC.KeepAlive(this);
            return new GpuResources(gpuResourcesPtr);
        }

        public Status SetGpuResources(GpuResources gpuResources)
        {
            UnsafeNativeMethods.mp_CalculatorGraph__SetGpuResources__SPgpu(MpPtr, gpuResources.SharedPtr, out var statusPtr).Assert();

            GC.KeepAlive(gpuResources);
            GC.KeepAlive(this);
            return new Status(statusPtr);
        }
    }
}
