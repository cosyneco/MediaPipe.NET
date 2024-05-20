using Mediapipe;
using Mediapipe.Framework;
using Mediapipe.Framework.Formats;
using Mediapipe.Framework.Packet;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FaceMeshWebcamOpenCV
{
    public partial class Form1 : Form
    {
        private VideoCapture _capture = new VideoCapture(5);
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
        private int i =0;

        public void CaptureCameraCallback()
        {
            var graph = new CalculatorGraph(File.ReadAllText(@"face_mesh_desktop_live.pbtxt"));
            var poller = graph.AddOutputStreamPoller<ImageFrame>("output_video");
            graph.StartRun();
            while (true)
            {
                         Mat _image = new Mat();

        _capture.Read(_image);
                _image = _image.CvtColor(ColorConversionCodes.BGR2RGB);
                if (!_image.Empty())
                {
                    _image = _image.Flip(FlipMode.Y);
                }

                //mediapipe::ImageFormat::SRGB, camera_frame.cols, camera_frame.rows,
                //mediapipe::ImageFrame::kDefaultAlignmentBoundary
                //
                //cv::Mat input_frame_mat = mediapipe::formats::MatView(input_frame.get());

                // Wrap Mat into an ImageFrame.

                int w = _image.Width;
                int h = _image.Height;
                int l = w * h;
                byte[] output = new byte[l];
                Marshal.Copy(_image.Data, output, 0, l);
                ReadOnlySpan<byte> bytes = new ReadOnlySpan<byte>(output);


                ImageFrame imageFrame = new ImageFrame(ImageFormat.Types.Format.Srgb, _image.Cols, _image.Rows, (int)_image.Step(), bytes);
                long frame_timestamp_us = (long)(Cv2.GetTickCount() / Cv2.GetTickFrequency() * 1e6);
                // Send image packet into the graph.

                graph.AddPacketToInputStream("input_video", Packet.CreateImageFrameAt(imageFrame, frame_timestamp_us));

                Packet<ImageFrame> pOutputFrame = new Packet<ImageFrame>();
                if (!poller.Next(pOutputFrame))
                {
                    break;
                }
/*
                ImageFrame outputFrame = pOutputFrame.Get();
                List<int> sizes = new List<int>()
                {
                    outputFrame.Height(), outputFrame.Width()
                };
                List<long> steps = new List<long>() { outputFrame.WidthStep(), outputFrame.ByteDepth() };
                Mat outputMat = new Mat(sizes, MatType.CV_8U, outputFrame.MutablePixelData(), steps);

                Mat outputMat2 = outputMat.CvtColor(ColorConversionCodes.RGB2BGR);

                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(() => pictureBox1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(outputMat2)));
                }
                else
                {
                    pictureBox1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(outputMat2);
                }*/

            }
        


        }
    }
}
