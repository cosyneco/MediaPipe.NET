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
                videoProcessor.ProcessVideo();
        }
    }

    class VideoProcessor : IDisposable
    {
        private readonly VideoCapture _capture;
        private readonly Window _window;
        private readonly CalculatorGraph _graph;
        private readonly OutputStreamPoller<List<NormalizedLandmarkList>> _landmarkPoller;
        private readonly Mat _inputMat;
        private readonly Mat _outputMat;

        public VideoProcessor()
        {
            _capture = new VideoCapture(2);
            _capture.FrameWidth = 1280;
            _capture.FrameHeight = 720;
            _window = new Window("Video Face Mesh");

            _graph = new CalculatorGraph(File.ReadAllText(@"face_mesh_desktop_live.pbtxt"));
            _landmarkPoller = _graph.AddOutputStreamPoller<List<NormalizedLandmarkList>>("multi_face_landmarks");
            _graph.StartRun();
            _inputMat = _outputMat = new Mat();
        }

        public void ProcessVideo()
        {
            while (true)
            {
                _capture.Read(_inputMat);

                if (_inputMat.Empty())
                {
                    Console.WriteLine("Failed to capture a frame.");
                    break;
                }

                ReadOnlySpan<byte> imageData = MatToReadOnlySpan(_inputMat);
                using (var imageFrame = new ImageFrame(
                    ImageFormat.Types.Format.Srgb,
                    _inputMat.Width,
                    _inputMat.Height,
                    _inputMat.Width * _inputMat.Channels(),
                    imageData
                ))
                {
                    long frameTimestampUs = (long)(Cv2.GetTickCount() / Cv2.GetTickFrequency() * 1e6);
                    _graph.AddPacketToInputStream("input_video", Packet.CreateImageFrameAt(imageFrame, frameTimestampUs));
                    Packet<List<NormalizedLandmarkList>> pOutputLandmark = new Packet<List<NormalizedLandmarkList>>();
                    if (!_landmarkPoller.Next(pOutputLandmark))
                    {
                        break;
                    }
                    var outputLandmark = pOutputLandmark.Get(NormalizedLandmarkList.Parser);

                    int index = 1;
                    outputLandmark.ForEach((l) =>
                    {
                        NormalizedLandmark upperLip = l.Landmark[13];
                        NormalizedLandmark lowerLip = l.Landmark[14];
                        double lipDistancePixels = Math.Abs(upperLip.Y - lowerLip.Y);

                        NormalizedLandmark leftEyeCorner = l.Landmark[159];
                        NormalizedLandmark rightEyeCorner = l.Landmark[386];
                        double distancePixels = Math.Sqrt(Math.Pow(leftEyeCorner.X - rightEyeCorner.X, 2) + Math.Pow(leftEyeCorner.Y - rightEyeCorner.Y, 2));

                        // double initialThreshold = 0.01;
                        double averageFaceSizeCm = 20.0;
                        double averageFaceSizePixels = 300.0;

                        double distanceCm = (averageFaceSizeCm * averageFaceSizePixels) / distancePixels;
                        double threshold = (130 - (distanceCm / 5000)) * (0.001f / (distanceCm / 5000));

                        if (lipDistancePixels > threshold)
                        {
                            _outputMat.PutText($"- OPEN ({index} - {lipDistancePixels} | {threshold})", new Point(upperLip.X * _outputMat.Width, upperLip.Y * _outputMat.Height - 60), HersheyFonts.HersheySimplex, 0.7, Scalar.Violet, 2);
                        }
                        else
                        {
                            _outputMat.PutText($"- CLOSE ({index} - {lipDistancePixels} | {threshold})", new Point(upperLip.X * _outputMat.Width, upperLip.Y * _outputMat.Height - 60), HersheyFonts.HersheySimplex, 0.7, Scalar.Blue, 2);
                        }
                        _outputMat.Circle(new Point(upperLip.X * _outputMat.Width, upperLip.Y * _outputMat.Height), 1, Scalar.Red, 1);
                        _outputMat.Circle(new Point(lowerLip.X * _outputMat.Width, lowerLip.Y * _outputMat.Height), 1, Scalar.Green, 1);
                        index++;
                    });
                    _window.ShowImage(_outputMat);
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
            _inputMat.Dispose();
            _outputMat.Dispose();
            _capture.Release();
            _window.Dispose();
            _graph.CloseAllPacketSources();
            _graph.WaitUntilDone();
            _graph.Dispose();
        }
    }
}
