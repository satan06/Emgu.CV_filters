using System;
using Emgu.CV;
using Emgu.CV.UI;

namespace Introduction
{
    class CaptureWebCam
    {
        private VideoCapture capture;
        private Mat _frame;
        private ImageBox container;

        public CaptureWebCam(ImageBox container) => this.container = container;

        public Mat Frame => _frame;

        public void GrabCameraFace()
        {
            capture = new VideoCapture();
            capture.ImageGrabbed += ProcessFrameFace;
            capture.Start(null);
        }

        public void GrabCameraWords()
        {
            capture = new VideoCapture();
            capture.ImageGrabbed += ProcessFrameText;
            capture.Start(null);
        }

        public void Stop()
        {
            capture.Stop();
        }

        private void ProcessFrameText(object sender, EventArgs e)
        {
            _frame = new Mat();
            capture.Retrieve(_frame);
            GetText();
        }

        private void ProcessFrameFace(object sender, EventArgs e)
        {
            _frame = new Mat();
            capture.Retrieve(_frame);
            GetFaces();
        }

        private void GetText()
        {
            container.Image = new Capture(_frame)
                .Binary()
                .Impact(iterations: 5)
                .GetContours()
                .DrawContours();
        }

        private void GetFaces()
        {
            container.Image = new FaceGrabber(_frame).GetFrontal().DrawFaces();
        }
    }
}
