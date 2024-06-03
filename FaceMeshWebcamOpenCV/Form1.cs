using Mediapipe;
using Mediapipe.Framework;
using Mediapipe.Framework.Formats;
using Mediapipe.Framework.Packet;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FaceMeshWebcamOpenCV
{
    public partial class Form1 : Form
    {
        private VideoCapture _capture = new VideoCapture(0);
        Thread thread;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            thread = new Thread(new ThreadStart(CaptureCameraCallback));
            thread.Start();
        }

        public void CaptureCameraCallback()
        {
            CalculatorGraph graph = new CalculatorGraph(File.ReadAllText(@"face_mesh_desktop_live.pbtxt"));
            OutputStreamPoller<ImageFrame> poller = graph.AddOutputStreamPoller<ImageFrame>("output_video");
            OutputStreamPoller<List<NormalizedLandmarkList>> landmarkPoller = graph.AddOutputStreamPoller<List<NormalizedLandmarkList>>("multi_face_landmarks");
            graph.StartRun();
            Task myTask;

            while (true)
            {
                Mat mat = new Mat();
                _capture.Read(mat);

                if (mat.Empty())
                {
                    Console.WriteLine("Failed to capture a frame.");
                    break;
                }

                ReadOnlySpan<byte> imageData = MatToReadOnlySpan(mat);
                ImageFrame imageFrame = new ImageFrame(
                    ImageFormat.Types.Format.Srgb,
                    mat.Width,
                    mat.Height,
                    mat.Width * mat.Channels(),
                    imageData
                );

                long frameTimestampUs = (long)(Cv2.GetTickCount() / Cv2.GetTickFrequency() * 1e6);
                graph.AddPacketToInputStream("input_video", Packet.CreateImageFrameAt(imageFrame, frameTimestampUs));

                Packet<ImageFrame> pOutputFrame = new Packet<ImageFrame>();
                if (!poller.Next(pOutputFrame))
                {
                    break;
                }
                myTask = Task.Run(() =>
                {
                    Packet<List<NormalizedLandmarkList>> pOutputLandmark = new Packet<List<NormalizedLandmarkList>>();
                    if (landmarkPoller.Next(pOutputLandmark))
                    {
                        Console.WriteLine("DDDD");
                    }
                });
                myTask.Dispose();


                /*                var task = poller.WaitNextAsync();
                                yield return new WaitUntil(() => task.IsCompleted);

                                if (!task.Result.ok)
                                {
                                    throw new Exception("Something went wrong");
                                }*/

                ImageFrame outputFrame = pOutputFrame.Get();
                Mat outputMat = new Mat(
                    new int[] { outputFrame.Height(), outputFrame.Width() },
                    MatType.CV_8UC3,
                    outputFrame.MutablePixelData(),
                    new long[] { outputFrame.WidthStep(), outputFrame.ByteDepth() }
                );
                System.Drawing.Bitmap outputBitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(outputMat.Flip(FlipMode.Y));

                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(() => pictureBox1.Image = outputBitmap));
                }
                else
                {
                    pictureBox1.Image = outputBitmap;
                }

            }
        }

        private unsafe static ReadOnlySpan<byte> MatToReadOnlySpan(Mat mat)
        {
            byte* ptr = (byte*)mat.Data.ToPointer();
            return new ReadOnlySpan<byte>(ptr, mat.Width * mat.Height * mat.Channels());
        }
    }
}
