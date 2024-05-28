using Mediapipe.Framework;
using Mediapipe.Framework.Packet;
using System;
using OpenCvSharp;
using System.IO;
using Mediapipe.Framework.Formats;
using Mediapipe;

namespace MediapipeNet2
{
    class Program
    {
        static void Main()
        {
            using (var videoProcessor = new VideoProcessor())
            {
                videoProcessor.ProcessVideo();
            }
        }
    }

    class VideoProcessor : IDisposable
    {
        private readonly VideoCapture capture;
        private readonly Window window;
        private readonly CalculatorGraph graph;
        private readonly OutputStreamPoller<ImageFrame> poller;
        private readonly Mat captureMat;
        private readonly Mat outputMat;

        public VideoProcessor()
        {
            capture = new VideoCapture(2);
            window = new Window("Video Face Mesh");
            capture.FrameWidth = 1280;
            capture.FrameHeight = 720;

            graph = new CalculatorGraph(File.ReadAllText(@"face_mesh_desktop_live.pbtxt"));
            poller = graph.AddOutputStreamPoller<ImageFrame>("output_video");
            graph.StartRun();

            captureMat = new Mat();
            outputMat = new Mat();
        }

        public void ProcessVideo()
        {
            while (true)
            {
                capture.Read(captureMat);

                if (captureMat.Empty())
                {
                    Console.WriteLine("Failed to capture a frame.");
                    break;
                }

                var imageData = MatToReadOnlySpan(captureMat);
                using (var imageFrame = new ImageFrame(
                    ImageFormat.Types.Format.Srgb,
                    captureMat.Width,
                    captureMat.Height,
                    captureMat.Width * captureMat.Channels(),
                    imageData
                ))
                {
                    long frameTimestampUs = (long)(Cv2.GetTickCount() / Cv2.GetTickFrequency() * 1e6);
                    graph.AddPacketToInputStream("input_video", Packet.CreateImageFrameAt(imageFrame, frameTimestampUs));

                    using (var pOutputFrame = new Packet<ImageFrame>())
                    {
                        if (!poller.Next(pOutputFrame))
                        {
                            break;
                        }

                        var outputFrame = pOutputFrame.Get();
                        using (var outputMat = new Mat(
                            new int[] { outputFrame.Height(), outputFrame.Width() },
                            MatType.CV_8UC3,
                            outputFrame.MutablePixelData(),
                            new long[] { outputFrame.WidthStep(), outputFrame.ByteDepth() }
                        ))
                        {
                            window.ShowImage(outputMat);
                        }
                    }
                }

                int key = Cv2.WaitKey(1);
                if (key == 27)
                    break;
            }
        }

        private unsafe ReadOnlySpan<byte> MatToReadOnlySpan(Mat mat)
        {
            byte* ptr = (byte*)mat.Data.ToPointer();
            return new ReadOnlySpan<byte>(ptr, mat.Width * mat.Height * mat.Channels());
        }

        public void Dispose()
        {
            captureMat.Dispose();
            outputMat.Dispose();
            capture.Release();
            window.Dispose();
            graph.CloseAllPacketSources();
            graph.WaitUntilDone();
            graph.Dispose();
        }
    }
}
