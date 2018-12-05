using System;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;

namespace Introduction
{
    class CaptureWebCam
    {
        private VideoCapture capture;
        private Mat _frame;
        public event EventHandler<CaptureFrameEv> ImageGrabbed;

        public Mat Frame => _frame;

        public void GrabCamera()
        {
            capture = new VideoCapture();
            capture.ImageGrabbed += ProcessFrame;
            capture.Start(null);
        }

        public void Stop()
        {
            capture.Stop();
        }

        private void ProcessFrame(object sender, EventArgs e)
        {
            _frame = new Mat();
            capture.Retrieve(_frame);
            ImageGrabbed.Invoke(this, new CaptureFrameEv() { CurFrame = new Image<Bgr, byte>(_frame.Bitmap) });
        }
    }
}
