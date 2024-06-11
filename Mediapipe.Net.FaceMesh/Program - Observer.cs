/*using Mediapipe.Framework;
using Mediapipe.Framework.Packet;
using System;
using OpenCvSharp;
using System.IO;
using Mediapipe.Framework.Formats;
using Mediapipe;
using System.Collections.Generic;
using System.Runtime.InteropServices;

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
        private List<NormalizedLandmarkList> _listLandmarks = new List<NormalizedLandmarkList>();
        private Mat _inputMat = new Mat();

        public VideoProcessor()
        {
            _capture = new VideoCapture(2);
            _capture.FrameWidth = 1280;
            _capture.FrameHeight = 720;
            _capture.Fps = 10;
            _window = new Window("Video Face Mesh");
        }

        public void ProcessVideo()
        {
            CalculatorGraph graph = new CalculatorGraph(File.ReadAllText(@"face_mesh_desktop_live.pbtxt"));
            graph.ObserveOutputStream("multi_face_landmarks", (Packet<List<NormalizedLandmarkList>> packet) => _listLandmarks = packet.Get(), out GCHandle cbHandle);
            graph.StartRun();

            while (true)
            {
                _capture.Read(_inputMat);
                if (_inputMat.Empty())
                {
                    Console.WriteLine("Failed to capture a frame.");
                    break;
                }

                using (ImageFrame imageFrame = new ImageFrame(
                    ImageFormat.Types.Format.Srgb,
                    _inputMat.Width,
                    _inputMat.Height,
                    _inputMat.Width * _inputMat.Channels(),
                    MatToReadOnlySpan(_inputMat)))
                {
                    long frameTimestampUs = (long)(Cv2.GetTickCount() / Cv2.GetTickFrequency() * 1e6);
                    graph.AddPacketToInputStream("input_video", Packet.CreateImageFrameAt(imageFrame, frameTimestampUs));

                    for(int i = 0;  i < _listLandmarks.Count; i++)
                    {
                        // Distance Lips
                        NormalizedLandmark upperLip = _listLandmarks[i].Landmark[13];
                        NormalizedLandmark lowerLip = _listLandmarks[i].Landmark[14];
                        double dxl = lowerLip.X - upperLip.X;
                        double dyl = lowerLip.Y - upperLip.Y;
                        double dzl = lowerLip.Z - upperLip.Z;
                        double lipDistancePixels = Math.Sqrt(dxl * dxl + dyl * dyl + dzl * dzl);

                        // Distance Pixels
                        NormalizedLandmark leftEyeCorner = _listLandmarks[i].Landmark[159];
                        NormalizedLandmark rightEyeCorner = _listLandmarks[i].Landmark[386];
                        double dxe = leftEyeCorner.X - rightEyeCorner.X;
                        double dye = leftEyeCorner.Y - rightEyeCorner.Y;
                        double dze = leftEyeCorner.Z - rightEyeCorner.Z;
                        double distancePixels = Math.Sqrt(dxe * dxe + dye * dye + dze * dze);

                        // double initialThreshold = 0.01;
                        double averageFaceSizeCm = 20.0;
                        double averageFaceSizePixels = 300.0;

                        double distanceCm = (averageFaceSizeCm * averageFaceSizePixels) / distancePixels;
                        double threshold = (130 - (distanceCm / 13000)) * (0.001f / (distanceCm / 13000));

                        if (lipDistancePixels > threshold)
                        {
                            _inputMat.PutText($"- OPEN {(int)(lipDistancePixels * 100000)}>{(int)(threshold * 100000)}", new Point(upperLip.X * _inputMat.Width, upperLip.Y * _inputMat.Height - 60), HersheyFonts.HersheySimplex, 1, Scalar.LightGreen, 3);
                        }
                        else
                        {
                            _inputMat.PutText($"- CLOSE {(int)(lipDistancePixels * 100000)}>{(int)(threshold * 100000)}", new Point(upperLip.X * _inputMat.Width, upperLip.Y * _inputMat.Height - 60), HersheyFonts.HersheySimplex, 1, Scalar.LightGray, 3);
                        }
                        _inputMat.Rectangle(new Point(upperLip.X * _inputMat.Width, upperLip.Y * _inputMat.Height), new Point(upperLip.X * _inputMat.Width + 2, upperLip.Y * _inputMat.Height + 2), Scalar.Red);
                        _inputMat.Rectangle(new Point(lowerLip.X * _inputMat.Width, lowerLip.Y * _inputMat.Height), new Point(lowerLip.X * _inputMat.Width + 2, lowerLip.Y * _inputMat.Height + 2), Scalar.Blue);
                    }
                    _listLandmarks = new List<NormalizedLandmarkList>();
                }
                _window.ShowImage(_inputMat);
                int key = Cv2.WaitKey(1);
                if (key == 27)
                    break;
            }
        }

        static private unsafe ReadOnlySpan<byte> MatToReadOnlySpan(Mat mat)
        {
            byte* ptr = (byte*)mat.Data.ToPointer();
            return new ReadOnlySpan<byte>(ptr, mat.Width * mat.Height * mat.Channels());
        }

        public void Dispose()
        {
            _capture.Release();
            _window.Dispose();
        }
    }
}*/