using Mediapipe.Framework;
using Mediapipe.Framework.Packet;
using System;

namespace MediapipeNet2
{
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
            var poller = graph.AddOutputStreamPoller<string>("out");
            graph.StartRun();

            for (var i = 0; i < 10; i++)
            {
                graph.AddPacketToInputStream("in", Packet.CreateStringAt("Hello World!", i));
            }

            graph.CloseInputStream("in");
            var packet = new Packet<string>();

            while (poller.Next(packet))
            {
                Console.WriteLine(packet.ToString());
            }

            graph.WaitUntilDone();
            Console.ReadLine();
        }
    }
}
