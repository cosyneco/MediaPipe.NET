// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;
using System.Collections;
using System.IO;
using Mediapipe.Net.External;
using Mediapipe.Net.Util;

namespace Mediapipe.Net.Examples.FaceMesh
{
    public class DummyResourceManager : ResourceManager
    {
        public override PathResolver ResolvePath => (path) =>
        {
            Console.WriteLine($"PathResolver: (not) resolving path '{path}'");
            // return GetAssetNameFromPath(path);
            return path;
        };

        public unsafe override ResourceProvider ProvideResource => (path, output) =>
        {
            Console.WriteLine($"ResourceProvider: providing resource '{path}'");
            using StdString strOutput = new StdString(output, false);
            using StdString resource = new StdString(File.ReadAllBytes(path));
            resource.Swap(strOutput);
            return true;
        };

        public override bool IsPrepared(string name)
        {
            throw new NotImplementedException();
        }

        public override IEnumerator PrepareAssetAsync(string name, string uniqueKey, bool overwrite = true)
        {
            throw new NotImplementedException();
        }
    }
}
