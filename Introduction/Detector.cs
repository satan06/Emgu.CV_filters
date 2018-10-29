using System;
using Emgu.CV;
using Emgu.CV.Structure;
using static Introduction.Data;

namespace Introduction
{
    public class Detector
    {
        public Image<Gray, byte> GaussianBlur(Image<Bgr, byte> img, int radius)
        {
            Image<Gray, byte> grayImage = img.Convert<Gray, byte>();
            Image<Gray, byte> bluredImage = grayImage.SmoothGaussian(radius);            return bluredImage ?? throw new ArgumentNullException(paramName: nameof(bluredImage));        }


    }
}
