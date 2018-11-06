using System;
using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace Introduction
{
    public class Detector
    {
        private Image<Gray, byte> _iterImage;
        private Image<Bgr, byte> _contImage;
        private VectorOfVectorOfPoint _contours = new VectorOfVectorOfPoint();

        private Lazy<List<VectorOfPoint>> _approxContours = new Lazy<List<VectorOfPoint>>();

        public List<VectorOfPoint> ApproxContours => _approxContours.Value;

        public Image<Bgr, byte> GetContImage => _contImage;

        private void SetContImage(Image<Bgr, byte> value)
        {
            _contImage = value;
        }

        public Data Data;

        public Detector(Data data)
        {
            Data = data;
        }

        public Detector GaussianBlur(int radius = 5)
        {
            Image<Gray, byte> grayImage = Data.SourceImage.Convert<Gray, byte>();
            Image<Gray, byte> bluredImage = grayImage.SmoothGaussian(radius);

            _iterImage = bluredImage;
            return this;
        }

        public Detector GetInterestArea(double thresval = 80, double cval = 255)
        {
            var threshold = new Gray(thresval);
            var color = new Gray(cval);

            _iterImage = _iterImage.ThresholdBinary(threshold, color);
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

        public Detector Approx(int minArea = 256, double eps = 0.05)
        {
            var contoursImage = Data.SourceImage.Copy(); 

            for (int i = 0; i < _contours.Size; i++)
            { 
                CvInvoke.ApproxPolyDP(
                    _contours[i],
                    _contours[i],
                    CvInvoke.ArcLength(_contours[i], true) * eps,
                    true);


                if (_contours[i].Size == 3 && 
                    CvInvoke.ContourArea(_contours[i], false) > minArea)
                {
                    var points = _contours[i].ToArray();

                    contoursImage.Draw(new Triangle2DF(points[0], points[1], points[2]),
                    new Bgr(Color.GreenYellow), 2);
                }
            }

            SetContImage(contoursImage);

            return this;
        }
    }
}
