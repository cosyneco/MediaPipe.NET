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
        private readonly Mat _inputMat = new Mat();
        private readonly CalculatorGraph _graph;
        private readonly OutputStreamPoller<bool> _presencePoller;
        private readonly OutputStreamPoller<List<NormalizedLandmarkList>> _landmarkPoller;
        private Point _upperLipPoint = new Point();
        private Point _lowerLipPoint = new Point();

        public VideoProcessor()
        {
            _capture = new VideoCapture(2);
            _capture.FrameWidth = 1280;
            _capture.FrameHeight = 720;
            _capture.Fps = 10;
            _window = new Window("Video Face Mesh");
            _graph = new CalculatorGraph(File.ReadAllText(@"face_mesh_desktop_live.pbtxt"));
            _presencePoller = _graph.AddOutputStreamPoller<bool>("landmark_presence");
            _landmarkPoller = _graph.AddOutputStreamPoller<List<NormalizedLandmarkList>>("multi_face_landmarks");
            _graph.StartRun();
        }

        public void ProcessVideo()
        {
            while (true)
            {
                if (!_capture.Read(_inputMat))
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
                    _graph.AddPacketToInputStream("input_video", Packet.CreateImageFrameAt(imageFrame, frameTimestampUs));

                    using (Packet<bool> pOutputPresence = new Packet<bool>())
                    {
                        if (_presencePoller.QueueSize() <= 0 || !_presencePoller.Next(pOutputPresence) || !pOutputPresence.Get()) continue;
                    }

                    using (Packet<List<NormalizedLandmarkList>> pOutputLandmark = new Packet<List<NormalizedLandmarkList>>())
                    {
                        if (_landmarkPoller.QueueSize() <= 0 || !_landmarkPoller.Next(pOutputLandmark) || pOutputLandmark == null) continue;

                        List<NormalizedLandmarkList> _listLandmarks = pOutputLandmark.Get(NormalizedLandmarkList.Parser);
                        for (byte i = 0; i < _listLandmarks.Count; i++)
                        {
                            NormalizedLandmark upperLip = _listLandmarks[i].Landmark[13];
                            NormalizedLandmark lowerLip = _listLandmarks[i].Landmark[14];
                            double dxl = lowerLip.X - upperLip.X;
                            double dyl = lowerLip.Y - upperLip.Y;
                            double dzl = lowerLip.Z - upperLip.Z;
                            double lipDistancePixels = Math.Sqrt(dxl * dxl + dyl * dyl + dzl * dzl);

                            NormalizedLandmark leftEyeCorner = _listLandmarks[i].Landmark[159];
                            NormalizedLandmark rightEyeCorner = _listLandmarks[i].Landmark[386];
                            double dxe = leftEyeCorner.X - rightEyeCorner.X;
                            double dye = leftEyeCorner.Y - rightEyeCorner.Y;
                            double dze = leftEyeCorner.Z - rightEyeCorner.Z;
                            double distancePixels = Math.Sqrt(dxe * dxe + dye * dye + dze * dze);

                            double averageFaceSizeCm = 20.0;
                            double averageFaceSizePixels = 300.0;

                            double distanceCm = (averageFaceSizeCm * averageFaceSizePixels) / distancePixels;
                            double threshold = (130 - (distanceCm / 13000)) * (0.001f / (distanceCm / 13000));
                            int thresholdInt = (int)(threshold * 100000);
                            int lipDistanceInt = (int)(lipDistancePixels * 100000);

                            if (lipDistancePixels > threshold)
                            {
                                _inputMat.PutText($"- OPEN {lipDistanceInt}>{thresholdInt}", new Point(upperLip.X * _inputMat.Width, upperLip.Y * _inputMat.Height - 60), HersheyFonts.HersheySimplex, 1, Scalar.LightGreen, 3);
                            }
                            else
                            {
                                _inputMat.PutText($"- CLOSE {lipDistanceInt}>{thresholdInt}", new Point(upperLip.X * _inputMat.Width, upperLip.Y * _inputMat.Height - 60), HersheyFonts.HersheySimplex, 1, Scalar.LightGray, 3);
                            }
                            _upperLipPoint.X = (int)(upperLip.X * _inputMat.Width);
                            _upperLipPoint.Y = (int)(upperLip.Y * _inputMat.Height);
                            _lowerLipPoint.X = (int)(lowerLip.X * _inputMat.Width);
                            _lowerLipPoint.Y = (int)(lowerLip.Y * _inputMat.Height);
                            _inputMat.Rectangle(_upperLipPoint, new Point(_upperLipPoint.X + 2, _upperLipPoint.Y + 2), Scalar.Red);
                            _inputMat.Rectangle(_lowerLipPoint, new Point(_lowerLipPoint.X + 2, _lowerLipPoint.Y + 2), Scalar.Blue);
                        }
                    }
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
            _graph.Dispose();
        }
    }
}