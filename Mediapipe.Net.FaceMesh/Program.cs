using Mediapipe.Framework;
using Mediapipe.Framework.Packet;
using System;
using OpenCvSharp;
using System.IO;
using Mediapipe.Framework.Formats;
using Mediapipe;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Mediapipe.External;

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
/*        private readonly CalculatorGraph _graph;
        private readonly OutputStreamPoller<ImageFrame> _poller;
        private readonly OutputStreamPoller<List<NormalizedLandmarkList>> _landmarkPoller;
        private readonly Mat _inputMat;
        private readonly Mat _outputMat;*/

        public VideoProcessor()
        {
            _capture = new VideoCapture(2);
            _capture.FrameWidth = 1280;
            _capture.FrameHeight = 720;
            _window = new Window("Video Face Mesh");

            /*            _graph = new CalculatorGraph(File.ReadAllText(@"face_mesh_desktop_live.pbtxt"));
                        _poller = _graph.AddOutputStreamPoller<ImageFrame>("output_video");
                        _landmarkPoller = _graph.AddOutputStreamPoller<List<NormalizedLandmarkList>>("multi_face_landmarks");
                        _graph.StartRun();
                        _inputMat = _outputMat = new Mat();     */
        }

        public void ProcessVideo()
        {
            Glog.Initialize("VideoProcessor");
            Glog.Logtostderr = true; // when true, log will be output to `Editor.log` / `Player.log` 
            Glog.Minloglevel = 1; // output INFO logs
            Glog.V = 3; // output more verbose logs
            Glog.LogDir = "C:\\Users\\robin.bigeard\\source\\repos\\MediaPipe.NET\\Mediapipe.Net.FaceMesh";

            CalculatorGraph graph = new CalculatorGraph(File.ReadAllText(@"face_mesh_desktop_live.pbtxt"));
            //OutputStreamPoller<ImageFrame> poller = graph.AddOutputStreamPoller<ImageFrame>("output_video");
            OutputStream<List<NormalizedLandmarkList>> landmarkPoller = new OutputStream<List<NormalizedLandmarkList>>(graph, "multi_face_landmarks");
            landmarkPoller.StartPolling();
            graph.StartRun();
            Task t = Task.Run(() => {});

            while (true)
            {
                Mat _inputMat = new Mat();
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
                    graph.AddPacketToInputStream("input_video", Packet.CreateImageFrameAt(imageFrame, frameTimestampUs));

                    /*                    Packet<ImageFrame> pOutputFrame = new Packet<ImageFrame>();
                                        if (!poller.Next(pOutputFrame))
                                        {
                                            break;
                                        }*/

                    // landmarkPoller.Reset();
                    /*                    if (t.IsCompleted)
                                        {
                                            t = Task.Run(() =>
                                            {*/

                    Packet<List<NormalizedLandmarkList>> pOutputLandmark = new Packet<List<NormalizedLandmarkList>>();
                    var task = landmarkPoller.WaitNextAsync();
                    // Thread.Sleep(1000);

/*                    while (!task.IsCompleted)
                    {
                        Thread.Sleep(100);
                    }*/
                    //WaitUntil(() => task.IsCompleted);
                    pOutputLandmark = task.Result.packet;
                    if (pOutputLandmark != null)
                    {
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
                                Console.WriteLine($"- OPEN ({index} - {lipDistancePixels} | {threshold})");
                                _inputMat.PutText($"- OPEN ({index} - {lipDistancePixels} | {threshold})", new Point(upperLip.X * _inputMat.Width, upperLip.Y * _inputMat.Height - 60), HersheyFonts.HersheySimplex, 0.7, Scalar.Violet, 2);
                            }
                            else
                            {
                                Console.WriteLine($"- CLOSE ({index} - {lipDistancePixels} | {threshold})");
                                _inputMat.PutText($"- CLOSE ({index} - {lipDistancePixels} | {threshold})", new Point(upperLip.X * _inputMat.Width, upperLip.Y * _inputMat.Height - 60), HersheyFonts.HersheySimplex, 0.7, Scalar.Blue, 2);
                            }
                            _inputMat.Circle(new Point(upperLip.X * _inputMat.Width, upperLip.Y * _inputMat.Height), 1, Scalar.Red, 1);
                            _inputMat.Circle(new Point(lowerLip.X * _inputMat.Width, lowerLip.Y * _inputMat.Height), 1, Scalar.Green, 1);
                            index++;
                        });
                    }
                    else
                    {

                    }
/*                        });
                    }*/
                    // imageFrame.Dispose();
                }
                _window.ShowImage(_inputMat);
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

/*    public static async Task WaitUntil(Func<bool> condition, int checkInterval = 100)
    {
        while (!condition())
            await Task.Delay(checkInterval);
    }*/

    public void Dispose()
        {
/*            _inputMat.Dispose();
            _outputMat.Dispose();*/
            _capture.Release();
            _window.Dispose();
/*            _graph.CloseAllPacketSources();
            _graph.WaitUntilDone();
            _graph.Dispose();*/
        }
    }
}
