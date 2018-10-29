using System;
using Emgu.CV;
using Emgu.CV.Structure;
using static Introduction.Data;

namespace Introduction
{
    public class Detector
    {
        private Image<Gray, byte> iter;

        public Image<Gray, byte> IterImage { get => iter; set => iter = value; }

        public Detector GaussianBlur(Image<Bgr, byte> img, int radius)
        {
            Image<Gray, byte> grayImage = img.Convert<Gray, byte>();
            Image<Gray, byte> bluredImage = grayImage.SmoothGaussian(radius);            IterImage = bluredImage;            return this;            }


    }
}
