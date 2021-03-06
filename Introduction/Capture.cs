﻿using Emgu.CV;
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
        public Image<Bgr, byte> Image;
        public Image<Gray, byte> _iterImage;
        private VectorOfVectorOfPoint _contours = new VectorOfVectorOfPoint();
        public List<Rectangle> Rects = new List<Rectangle>();

        private Lazy<List<Image<Bgr, byte>>> _captions = new Lazy<List<Image<Bgr, byte>>>();
        public List<Image<Bgr, byte>> Captions => _captions.Value;

        public Capture(Data data) => Image = data.SourceImage;
        public Capture(Image<Bgr, byte> image) => Image = image;

        public Capture(Image<Bgr, byte> image, Image<Gray, byte> mask)
        {
            Image = image;
            _iterImage = mask;
        }

        public Capture Binary(double thresval = 80, double cval = 255)
        {
            Image<Gray, byte> grayImage = Image.Convert<Gray, byte>();
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

        public Capture FindContoursOnRetrivedFrame(int limit = 700)
        {
            CvInvoke.FindContours(
                _iterImage, 
                _contours, 
                null, 
                RetrType.External,
                ChainApproxMethod.ChainApproxTc89L1);

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
            var copy = Image.Copy();

            foreach(Rectangle rect in Rects)
            {
                copy.Draw(rect, new Bgr(Color.Yellow), thickness);
            }
            return copy;
        }

        public Capture GetCaptures()
        {
            for(int i = 0; i < Rects.Count; i++)
            {
                Image.ROI = Rects[i];
                Captions.Add(Image.Copy());
                Image.ROI = Rectangle.Empty;
            }

            return this;
        }
    }
}
