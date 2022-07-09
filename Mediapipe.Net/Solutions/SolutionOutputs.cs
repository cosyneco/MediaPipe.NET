// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System.Collections.Generic;
using System.Linq;

namespace Mediapipe.Net.Solutions
{
    /// <summary>
    /// Solution outputs queued by timestamp.
    /// </summary>
    public class SolutionOutputs
    {
        private readonly IDictionary<long, IDictionary<string, object?>> tempOutputs;
        private readonly IDictionary<long, IDictionary<string, object?>> finishedOutputs;

        public SolutionOutputs()
        {
            tempOutputs = new Dictionary<long, IDictionary<string, object?>>();
            finishedOutputs = new Dictionary<long, IDictionary<string, object?>>();
        }

        public void Put(long timestamp, string output, object? packetOutput)
        {
            if (!tempOutputs.ContainsKey(timestamp))
                tempOutputs[timestamp] = new Dictionary<string, object?>();
            tempOutputs[timestamp][output] = packetOutput;
        }

        public void Finish(long timestamp)
        {
            if (tempOutputs.ContainsKey(timestamp))
            {
                finishedOutputs[timestamp] = tempOutputs[timestamp];
                tempOutputs.Remove(timestamp);
            }
            else
            {
                finishedOutputs[timestamp] = new Dictionary<string, object?>();
            }
        }

        public IDictionary<string, object?> GetSolutionOutput(long timestamp)
        {
            IDictionary<string, object?> solutionOutput = finishedOutputs[timestamp];
            finishedOutputs.Remove(timestamp);
            return solutionOutput;
        }

        public IDictionary<string, object?> GetEarliestSolutionOutput()
        {
            long earliestTimestamp = finishedOutputs.Keys.Min();
            return GetSolutionOutput(earliestTimestamp);
        }
    }
}
