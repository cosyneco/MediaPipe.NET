// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.
using Mediapipe.Net.Framework;
using Mediapipe.Net.Framework.Packets;

namespace Mediapipe.Net.HelloWorld
{
    // This is the classic Hello world sample for MediaPipe, which is the same as the one in the MediaPipe repository.
    // and also the same as the one in MediaPipeUnity.
    internal class Program
    {
        private static readonly string configTxt = @"
input_stream: ""in""
output_stream: ""out""
node {
  calculator: ""PassThroughCalculator""
  input_stream: ""in""
  output_stream: ""out1""
}
node {
  calculator: ""PassThroughCalculator""
  input_stream: ""out1""
  output_stream: ""out""
}
";
        static void Main()
        {
            var graph = new CalculatorGraph(configTxt);
            var poller = graph.AddOutputStreamPoller<string>("out").Value();
            graph.StartRun().AssertOk();

            for (var i = 0; i < 10; i++)
            {
                graph.AddPacketToInputStream("in", new StringPacket("Hello World!"));
            }

            graph.CloseInputStream("in").AssertOk();
            var packet = new StringPacket();

            while (poller.Next(packet))
            {
                Console.WriteLine(packet.ToString());
            }

            graph.WaitUntilDone().AssertOk();
        }
    }
}
