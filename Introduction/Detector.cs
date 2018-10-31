using System;
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

        public VectorOfVectorOfPoint Contours { get => _contours; private set => _contours = value; }

        public Image<Bgr, byte> GetContImage => _contImage;

        private void SetContImage(Image<Bgr, byte> value)
        {
            _contImage = value;
        }

        public Image<Gray, byte> IterImage { get => _iterImage; private set => _iterImage = value; }

        public Data Data;

        public Detector(Data data)
        {
            Data = data;
        }

        public Detector GaussianBlur(int radius = 5)
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

        public Detector DetectContours()
        {
            CvInvoke.FindContours(
                IterImage, 
                Contours, 
                null, 
                RetrType.List, 
                ChainApproxMethod.ChainApproxSimple);

            return this;
        }

        public Detector GetContours(int thickness = 2)
        {
            var contoursImage = Data.SourceImage.CopyBlank();

            for (int i = 0; i < Contours.Size; i++)            {
                var points = Contours[i].ToArray();
                contoursImage.Draw(points, new Bgr(Color.GreenYellow), thickness); 
            }            SetContImage(contoursImage);            return this;
        }

    }
}
