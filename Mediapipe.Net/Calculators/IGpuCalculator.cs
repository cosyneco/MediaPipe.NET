// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using Mediapipe.Net.Gpu;

namespace Mediapipe.Net.Calculators;

public interface IGpuCalculator<T> : ICalculator<GpuBuffer, T>
{

}
