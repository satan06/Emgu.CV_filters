using System;
using System.Collections.Generic;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace Introduction
{
    public class Detector
    {
        private Image<Gray, byte> _iterImage;
        private VectorOfVectorOfPoint _contours = new VectorOfVectorOfPoint();
        public VectorOfVectorOfPoint ApproxContours;
        public Image<Bgr, byte> ImageCopy { get; private set; }

        private Lazy<List<CircleF>> _circles = new Lazy<List<CircleF>>();
        public List<CircleF> Circles => _circles.Value;

        private Data _data;

        public Detector(Data data)
        {
            _data = data;
            ImageCopy = data.SourceImage.Copy();
        }

        public Detector GaussianBlur(int radius = 5)
        {
            Image<Gray, byte> grayImage = _data.SourceImage.Convert<Gray, byte>();
            Image<Gray, byte> bluredImage = grayImage.SmoothGaussian(radius);

            _iterImage = bluredImage;
            return this;
        }

        public Detector GetInterestStandart(double thresval = 80, double cval = 255)
        {
            var threshold = new Gray(thresval);
            var color = new Gray(cval);

            _iterImage = _iterImage.ThresholdBinary(threshold, color);
            return this;
        }

        public Detector GetInterestCanny(double thresh, double threshLinking)
        {
            _iterImage = _iterImage.Canny(thresh, threshLinking);            return this;
        }

        public Detector GetInterestByColor(byte color, byte rangeDelta = 10)
        {
            var hsvImage = _data.SourceImage.Convert<Hsv, byte>();  
            var hueChannel = hsvImage.Split()[0];
            var resultImage = hueChannel.InRange(new Gray(color - rangeDelta), 
                new Gray(color + rangeDelta));

            return this;
        }

        public Detector DetectContours()
        {
            CvInvoke.FindContours(
                _iterImage,
                _contours, 
                null, 
                RetrType.List, 
                ChainApproxMethod.ChainApproxSimple);

            return this;
        }

        public Detector DetectContours(double minDistance, double acTreshold, int minRadius, int maxRadius)
        {
            Circles.AddRange(
                new List<CircleF>(
                    CvInvoke.HoughCircles(_iterImage,
                        HoughType.Gradient,
                        1.0,
                        minDistance,
                        100,
                        acTreshold,
                        minRadius,
                        maxRadius)
                        )
                    );
            
            return this;
        }

        public Detector Approx(double eps = 0.05)
        {
            ApproxContours = new VectorOfVectorOfPoint(_contours.Size);

            for (int i = 0; i < _contours.Size; i++)
            { 
                CvInvoke.ApproxPolyDP(
                    _contours[i],
                    ApproxContours[i],
                    CvInvoke.ArcLength(_contours[i], true) * eps,
                    true);
            }

            return this;
        }
    }
}
