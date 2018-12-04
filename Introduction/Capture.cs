using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using Emgu.CV.OCR;

namespace Introduction
{
    public class Capture
    {
        private readonly Data _data;
        public Image<Gray, byte> _iterImage;
        private VectorOfVectorOfPoint _contours = new VectorOfVectorOfPoint();
        public Image<Bgr, byte> ImageCopy { get; private set; }
        public List<Rectangle> Rects = new List<Rectangle>();

        private Lazy<List<Image<Bgr, byte>>> _captions = new Lazy<List<Image<Bgr, byte>>>();
        public List<Image<Bgr, byte>> Captions => _captions.Value;

        public Capture(Data data)
        {
            _data = data;
            ImageCopy = data.SourceImage.Copy();
        }

        public Capture Binary(double thresval = 80, double cval = 255)
        {
            Image<Gray, byte> grayImage = _data.SourceImage.Convert<Gray, byte>();
            _iterImage = grayImage.ThresholdBinaryInv(new Gray(thresval), new Gray(cval));
            return this;
        }

        public Capture Impact(int iterations)
        {
            _iterImage = _iterImage.Dilate(iterations);
            return this;
        }

        public Capture GetContours(int limit = 50)
        {
            CvInvoke.FindContours(_iterImage, _contours, null, RetrType.List,
                ChainApproxMethod.ChainApproxSimple);

            for (int i = 0; i < _contours.Size; i++)
            {
                if (CvInvoke.ContourArea(_contours[i], false) > limit)
                {
                    Rectangle rect = CvInvoke.BoundingRectangle(_contours[i]);
                    Rects.Add(rect);
                }
            }

            return this;
        }

        public Image<Bgr, byte> DrawContours(int thickness = 1)
        {
            foreach(Rectangle rect in Rects)
            {
                ImageCopy.Draw(rect, new Bgr(Color.Blue), thickness);
            }
            return ImageCopy;
        }

        public Capture GetCaptures()
        {
            for(int i = 0; i < Rects.Count; i++)
            {
                _data.SourceImage.ROI = Rects[i];
                Captions.Add(_data.SourceImage.Copy());
                _data.SourceImage.ROI = Rectangle.Empty;
            }

            return this;
        }
    }
}
