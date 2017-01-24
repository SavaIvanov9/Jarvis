using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

using Emgu.CV;
using Emgu.CV.Structure;
using Jarvis.FaceDetection.Recognition;

namespace Jarvis.FaceRecognition.WinForms
{
    public partial class RecognizerForm : Form
    {
        private Capture capture = null;
        private CascadeClassifier haar = null;

        private Image<Bgr, Byte> frame = null;
        private Image<Gray, Byte> grayFrame = null;
        private Image<Bgr, Byte> currentFace = null;
        private Image<Gray, Byte> currentFaceGray = null;

        private Recognizer recognizer;

        public static RecognizerForm Instance
        {
            get;
            private set;
        }

        public RecognizerForm()
        {
            InitializeComponent();

            Instance = this;

            haar = new CascadeClassifier(@"haarcascade_frontalface_default.xml");

            InitializeCapture();
        }

        public void InitializeCapture()
        {
            capture = new Capture();
            recognizer = new Recognizer();

            Application.Idle += ProcessFrame;
        }

        public void StopCapture()
        {
            Application.Idle -= ProcessFrame;
            capture.Dispose();
        }

        private void ProcessFrame(object sender, EventArgs arg)
        {
            frame = capture.QueryFrame().ToImage<Bgr, Byte>();
            grayFrame = frame.Convert<Gray, Byte>();

            Rectangle[] facesDetected = haar.DetectMultiScale(grayFrame, 1.2, 10, new Size(50, 50), Size.Empty);

            for (int i = 0; i < facesDetected.Length; i++)
            {
                facesDetected[i].X += (int)(facesDetected[i].Height * 0.15);
                facesDetected[i].Y += (int)(facesDetected[i].Width * 0.22);
                facesDetected[i].Height -= (int)(facesDetected[i].Height * 0.3);
                facesDetected[i].Width -= (int)(facesDetected[i].Width * 0.35);

                currentFace = frame.Copy(facesDetected[i]);
                currentFaceGray = grayFrame.Copy(facesDetected[i]).Resize(100, 100, Emgu.CV.CvEnum.Inter.Cubic);
                currentFaceGray._EqualizeHist();

                frame.Draw(facesDetected[i], new Bgr(Color.Red), 2);

                if (recognizer.IsTrained)
                {
                    var result = recognizer.Recognize(currentFaceGray);
                    frame.Draw(result.Name + ":" + (int)result.Distance,
                        new Point(facesDetected[i].X - 2, facesDetected[i].Y - 2),
                        Emgu.CV.CvEnum.FontFace.HersheyComplex,
                        0.5d,
                        new Bgr(255, 0, 0)
                        );
                }
            }

            pictureBox1.Image = frame.ToBitmap();
        }

        private void ReleaseData()
        {
            if (Instance != null)
            {
                Instance = null;
            }
            if (capture != null)
            {
                StopCapture();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StopCapture();
            TrainingForm training = new TrainingForm(haar);

            training.ShowDialog();
        }
    }
}
