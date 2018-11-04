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

        public Detector Approx(double eps = 0.05, int minArea = 256)
        {
            var contoursImage = Data.SourceImage.Copy(); 

            for (int i = 0; i < Contours.Size; i++)
            {
                var approxContour = new VectorOfPoint(); 

                CvInvoke.ApproxPolyDP(
                    Contours[i],                         
                    approxContour,
                    CvInvoke.ArcLength(Contours[i], true) * eps,
                    true);

                // Difference in factories => specific primitive detection 
                // and its drawing method

                if (approxContour.Size == 3 && 
                    CvInvoke.ContourArea(approxContour, false) > minArea)
                {
                    var points = approxContour.ToArray();

                    contoursImage.Draw(new Triangle2DF(points[0], points[1], points[2]),
                    new Bgr(Color.GreenYellow), 2);
                }
            }

            SetContImage(contoursImage);

            return this;
        }
    }
}
