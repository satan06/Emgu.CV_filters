using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Introduction
{
    class CaptureCamera
    {
        private VideoCapture capture;
        private Mat _frame = new Mat();
        public event EventHandler<CaptureFrameEv> FrameGrabbed;
        private BackgroundSubtractorMOG2 _substractor = new BackgroundSubtractorMOG2(1000, 32, true);
        private Image<Gray, byte> _background;
        private bool isBackgroundCaptured = false;

        public void GrabCamera()
        {
            capture = new VideoCapture();
            capture.ImageGrabbed += ProcessFrame;
            capture.Start(null);
        }

        public void GrabCamera(string fileName)
        {
            capture = new VideoCapture(fileName);
            capture.ImageGrabbed += ProcessFrameVid;
            capture.Start(null);
        }

        public void GrabCameraSimple(string fileName)
        {
            capture = new VideoCapture(fileName);
            capture.ImageGrabbed += ProcessFrameSimple;
            capture.Start(null);
        }

        private void ProcessFrameSimple(object sender, EventArgs e)
        {
            if (capture.Retrieve(_frame))
            {
                if(!isBackgroundCaptured)
                {
                    _background = new Image<Gray, byte>(_frame.Bitmap);
                    isBackgroundCaptured = true;
                }

                var diff = _background.AbsDiff(_frame.ToImage<Gray, byte>())
                    .Erode(3)
                    .Dilate(4)
                    .SmoothMedian(5);

                FrameGrabbed.Invoke(this, new CaptureFrameEv()
                {
                    CurFrame = new Image<Bgr, byte>(_frame.Bitmap),
                    CurFrameGray = FilterMask(diff, 60)
                });
            }
        }

        public void Stop()
        {
            capture.Stop();
        }

        private void ProcessFrame(object sender, EventArgs e)
        {
            if (capture.Retrieve(_frame))
            {
                FrameGrabbed.Invoke(this, new CaptureFrameEv()
                {
                    CurFrame = new Image<Bgr, byte>(_frame.Bitmap)
                });
            }
        }

        private void ProcessFrameVid(object sender, EventArgs e)
        {
            try
            {
                if (_frame != null && capture.Retrieve(_frame))
                {
                    var foregroundMask = _frame.ToImage<Gray, byte>().CopyBlank();
                    _substractor.Apply(_frame, foregroundMask);

                    FrameGrabbed.Invoke(this, new CaptureFrameEv()
                    {
                        CurFrame = _frame.ToImage<Bgr, byte>(),
                        CurFrameGray = FilterMask(foregroundMask)
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Argument {ex.Source} is cousing an error: {ex.Message}\n");
            }
        }

        public void UpdateBackground()
        {
            _background = _frame.ToImage<Gray, byte>();
        }

        private Image<Gray, byte> FilterMask(Image<Gray, byte> mask, int iter = 240)
        {
            var anchor = new Point(-1, -1); var borderValue = new MCvScalar(1);
            var kernel = CvInvoke.GetStructuringElement(ElementShape.Ellipse, new Size(3, 3), anchor); 
            var closing = mask.MorphologyEx(MorphOp.Close, kernel, anchor, 1, BorderType.Default, borderValue); 
            var opening = closing.MorphologyEx(MorphOp.Open, kernel, anchor, 1, BorderType.Default, borderValue); 
            var dilation = opening.Dilate(7); 
            var threshold = dilation.ThresholdBinary(new Gray(iter), new Gray(255)); 

            return threshold;
        }

        private void ReleaseData()
        {
            if (capture != null)
            {
                capture.Dispose();
            }
        }
    }
}
