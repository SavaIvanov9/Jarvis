using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Vision.Motion;
using AForge.Video.DirectShow;

namespace AForgeTestGround
{
    class Program
    {
        static FilterInfoCollection videoDevices;
        //private static System.Windows.Forms.ComboBox devicesCombo;
        //private static AForge.Controls.VideoSourcePlayer videoSourcePlayer;
        private static MotionDetector detector = new MotionDetector(
                new SimpleBackgroundModelingDetector(),
                new MotionAreaHighlighting());

        private static volatile bool _isActivated = false;
        private static volatile bool _isAlive = true;
        private static int a = 0;

        private static SpeechSynthesizer _speaker;
        private static readonly PromptBuilder _promptBuilder = new PromptBuilder();
        private static VideoCaptureDevice videoSource;

        static void Main(string[] args)
        {
            #region testcode

            //// create motion detector
            ////MotionDetector detector = new MotionDetector(
            ////    new SimpleBackgroundModelingDetector(),
            ////    new MotionAreaHighlighting());

            ////// continuously feed video frames to motion detector
            ////while (true)
            ////{
            ////    // process new video frame and check motion level
            ////    if (detector.ProcessFrame(videoFrame) > 0.02)
            ////    {
            ////        // ring alarm or do somethng else
            ////    }
            ////}


            //try
            //{
            //    // enumerate video devices
            //    videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            //    if (videoDevices.Count == 0)
            //        throw new ApplicationException();

            //    Console.WriteLine(videoDevices[0].MonikerString.ToString());


            //    //// add all devices to combo
            //    //foreach (FilterInfo device in videoDevices)
            //    //{
            //    //    devicesCombo.Items.Add(device.Name);
            //    //}
            //}
            //catch (ApplicationException)
            //{
            //    devicesCombo.Items.Add("No local capture devices");
            //    devicesCombo.Enabled = false;
            //    //takePictureBtn.Enabled = false;
            //}

            ////devicesCombo.SelectedIndex = 0;

            //VideoCaptureDevice videoCaptureSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            //videoCaptureSource.Start();

            #endregion

            Console.WriteLine("Enter Password:");
            var password = Console.ReadLine();
            Console.WriteLine("Enter seconds to delay start:");
            int delayTime = int.Parse(Console.ReadLine());
            Console.Clear();
            Thread.Sleep(delayTime * 1000);
            //Console.Beep(1000, 1000);
            Speak("Movement detection started.");

            // enumerate video devices
            videoDevices = new FilterInfoCollection(
                FilterCategory.VideoInputDevice);
            // create video source
            videoSource = new VideoCaptureDevice(
                videoDevices[0].MonikerString);
            // set NewFrame event handler
            videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
            // start the video source
            var thread = new Thread(videoSource.Start);
            thread.IsBackground = true;
            thread.Start();
            //videoSource.Start();
            // ...
            // signal to stop
            //videoSource.SignalToStop();
            // ...

            var thread2 = new Thread(StartVoiceAlarm);
            thread2.IsBackground = true;
            thread2.Start();

            var input = Console.ReadLine();

            while (_isAlive)
            {
                if (input == password)
                {
                    videoSource.SignalToStop();
                    _isAlive = false;
                    break;
                    //Environment.Exit(0);
                }
                input = Console.ReadLine();
            }

            Speak("Movement detection stopped");
            //new Thread(ReadInput).Start();

            //while (_isAlive)
            //{
            //    if (_isActivated)
            //    {
            //        VoiceAlarm();
            //    }
            //}
        }

        private static void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = eventArgs.Frame;

            // process new video frame and check motion level
            if (detector.ProcessFrame(bitmap) >= 0.1)
            {
                //Console.WriteLine(a);
                _isActivated = true;
            }
            else
            {
                _isActivated = false;
            }
        }

        private static void StartVoiceAlarm()
        {
            while (_isAlive)
            {
                if (_isActivated)
                {
                    Speak("Warrning!Intruder detected!");
                }
            }
        }

        private static void Speak(string message)
        {
            using (_speaker = new SpeechSynthesizer())
            {
                _speaker.SelectVoice("Microsoft David Desktop");
                _promptBuilder.ClearContent();
                _promptBuilder.AppendText(message);
                _speaker.Speak(_promptBuilder);
            }
        }

        //private void takePictureBtn_Click(object sender, EventArgs e)
        //{
        //    DateTime time = DateTime.Now;              // Use current time
        //    string format = "MMM ddd d HH mm yyyy";    // Use this format
        //    String strFilename = "Capture-" + time.ToString(format) + ".jpg";
        //    if (videoSourcePlayer.IsRunning)
        //    {
        //        Bitmap picture = videoSourcePlayer.GetCurrentVideoFrame();
        //        picture.Save(strFilename, ImageFormat.Jpeg);
        //        labelSaved.Text = "Capture Saved : " + strFilename;
        //    }
        //}

        //private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    videoSourcePlayer.SignalToStop();
        //    videoSourcePlayer.WaitForStop();
        //    videoDevices = null;
        //    videoDevices = null;
        //}

        //private void devicesCombo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    videoSourcePlayer.SignalToStop();
        //    videoSourcePlayer.WaitForStop();
        //    VideoCaptureDevice videoCaptureSource = new VideoCaptureDevice(videoDevices[devicesCombo.SelectedIndex].MonikerString);
        //    videoSourcePlayer.VideoSource = videoCaptureSource;
        //    videoSourcePlayer.Start();
        //}
    }
}
