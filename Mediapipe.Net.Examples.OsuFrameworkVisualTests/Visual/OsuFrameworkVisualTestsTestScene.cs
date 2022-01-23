// Copyright (c) homuler and Vignette
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using osu.Framework.Testing;

namespace Mediapipe.Net.Examples.OsuFrameworkVisualTests.Visual
{
    public class OsuFrameworkVisualTestsTestScene : TestScene
    {
        protected override ITestSceneTestRunner CreateRunner() => new OsuFrameworkVisualTestsTestSceneTestRunner();

        private class OsuFrameworkVisualTestsTestSceneTestRunner : OsuFrameworkVisualTestsGameBase, ITestSceneTestRunner
        {
            private TestSceneTestRunner.TestRunner? runner;

            protected override void LoadAsyncComplete()
            {
                base.LoadAsyncComplete();
                Add(runner = new TestSceneTestRunner.TestRunner());
            }

            public void RunTestBlocking(TestScene test) => runner?.RunTestBlocking(test);
        }
    }
}
