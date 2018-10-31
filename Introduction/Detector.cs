using System;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Introduction
{
    public class Detector
    {
        private Image<Gray, byte> _iterImage; 
        public Image<Gray, byte> IterImage { get => _iterImage; private set => _iterImage = value; }

        public Data Data;

        public Detector(Data data)
        {
            Data = data;
        }

        public Detector GaussianBlur(int radius)
        {
            Image<Gray, byte> grayImage = Data.SourceImage.Convert<Gray, byte>();
            Image<Gray, byte> bluredImage = grayImage.SmoothGaussian(radius);

            IterImage = bluredImage;
            return this;
        }

        public Detector GetInterestArea(double thresval = 80, double cval = 255)
        {
            var threshold = new Gray(thresval);
            var color = new Gray(cval);

            IterImage = IterImage.ThresholdBinary(threshold, color);
            return this;
        }
    }
}
