using System;
using Emgu.CV;

namespace Introduction
{
    class CaptureWebCam
    {
        private VideoCapture capture;
        public delegate void OnCaptureEvent();
        //public OnCaptureEvent OnCaptureEventCallback;

        public void GrabCamera()
        {
            capture = new VideoCapture();
            capture.ImageGrabbed += ProcessFrame;
            capture.Start(null);
        }

        public void GrabVideo(string fileName)
        {
             capture = new VideoCapture(fileName);
             capture.ImageGrabbed += ProcessFrame;
             capture.Start(null);
        }

        public void Stop()
        {
            capture.Stop();
        }

        private void ProcessFrame(object sender, EventArgs e)
        {
            var frame = new Mat();

            capture.Retrieve(frame);

           // if (OnCaptureEventCallback != null)
           //     OnCaptureEventCallback.Invoke();
        }
    }
}
