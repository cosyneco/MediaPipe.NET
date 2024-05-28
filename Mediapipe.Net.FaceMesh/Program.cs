using Mediapipe.Framework;
using Mediapipe.Framework.Packet;
using System;
using OpenCvSharp;
using System.IO;
using Mediapipe.Framework.Formats;
using Mediapipe;
using System.Collections.Generic;

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
        private readonly OutputStreamPoller<List<NormalizedLandmarkList>> landmarkPoller;
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
            landmarkPoller = graph.AddOutputStreamPoller<List<NormalizedLandmarkList>>("multi_face_landmarks");
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
                            window.ShowImage(MouthMovement(captureMat));
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
        
        private Mat MouthMovement(Mat outputMat)
        {
            var pOutputLandmark = new Packet<List<NormalizedLandmarkList>>();
            var pOutputRectLandmark = new Packet<List<NormalizedRect>>();

            if (!landmarkPoller.Next(pOutputLandmark))
            {
                return outputMat;
            }

            var outputLandmark = pOutputLandmark.Get(NormalizedLandmarkList.Parser);

            var i = 1;
            outputLandmark.ForEach((l) =>
            {
                var upperLip = l.Landmark[13];
                var lowerLip = l.Landmark[14];
                var lipDistancePixels = Math.Abs(upperLip.Y - lowerLip.Y);

                var leftEyeCorner = l.Landmark[159];
                var rightEyeCorner = l.Landmark[386];
                var distancePixels = Math.Sqrt(Math.Pow(leftEyeCorner.X - rightEyeCorner.X, 2) + Math.Pow(leftEyeCorner.Y - rightEyeCorner.Y, 2));

                var initialThreshold = 0.01;
                var averageFaceSizeCm = 20.0;
                var averageFaceSizePixels = 300.0;

                var distanceCm = (averageFaceSizeCm * averageFaceSizePixels) / distancePixels;
                var threshold = (130 - (distanceCm / 5000)) * (0.001 / (distanceCm / 5000));

                if (lipDistancePixels > threshold)
                {
                    outputMat.PutText($"- OPEN ({i})", new Point(upperLip.X * outputMat.Width, upperLip.Y * outputMat.Height - 60), HersheyFonts.HersheySimplex, 0.7, Scalar.Violet, 2);
                }
                else
                {
                    outputMat.PutText($"- CLOSE ({i})", new Point(upperLip.X * outputMat.Width, upperLip.Y * outputMat.Height - 60), HersheyFonts.HersheySimplex, 0.7, Scalar.Blue, 2);
                }
                i++;
                captureMat.Circle(new Point(upperLip.X * outputMat.Width, upperLip.Y * outputMat.Height), 1, Scalar.Red, 1);
                captureMat.Circle(new Point(lowerLip.X * outputMat.Width, lowerLip.Y * outputMat.Height), 1, Scalar.Green, 1);
            });
            return outputMat;
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
