using System.Drawing;
using AForge.Video;
using AForge.Vision.Motion;
using AForge.Video.DirectShow;

namespace Jarvis.MovementDetection
{
    public class MovementDetector
    {
        private FilterInfoCollection _videoDevices;
        private readonly MotionDetector _detector = new MotionDetector(
                new SimpleBackgroundModelingDetector(),
                new MotionAreaHighlighting());

        private VideoCaptureDevice _videoSource;

        public MovementDetector()
        {
            Initialize();
        }

        public VideoCaptureDevice VideoSource
        {
            get { return this._videoSource; }
        }

        private void Initialize()
        {
            // enumerate video devices
            _videoDevices = new FilterInfoCollection(
                FilterCategory.VideoInputDevice);
            // create video source
            _videoSource = new VideoCaptureDevice(
                _videoDevices[0].MonikerString);
            // set NewFrame event handler
            _videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = eventArgs.Frame;

            // process new video frame and check motion level
            if (_detector.ProcessFrame(bitmap) >= 0.1)
            {
                //Console.WriteLine(a);
                Config.IsActivatedAlarm = true;
            }
            else
            {
                Config.IsActivatedAlarm = false;
            }
        }
    }
}
