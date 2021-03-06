﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.OCR;
using System.Drawing;
using Emgu.CV.Structure;

namespace Introduction
{
    public class FaceGrabber
    {
        public Rectangle[] DetectedFaces;
        public string PackDirectory { get; set; } = "C:/githb/haarcascades/";
        public Image<Bgr, byte> Image;

        public FaceGrabber(Data data) => Image = data.SourceImage;
        public FaceGrabber(Image<Bgr, byte> image) => Image = image;

        public FaceGrabber GetFrontal(double scaleFactor = 1.1, int neighbours = 10)
        {
            string name = "haarcascade_frontalface_default.xml";

            using (CascadeClassifier cascadeClassifier = 
                new CascadeClassifier(PackDirectory + name))
            {
                var grayImage = Image.Convert<Gray, byte>();
                DetectedFaces = cascadeClassifier.DetectMultiScale(grayImage, 
                    scaleFactor, neighbours, new Size(20, 20));
            }

            return this;
        }

        public Image<Bgr, byte> DrawFaces(int thickness = 2)
        {
            var copy = Image.Copy();

            foreach (Rectangle face in DetectedFaces)
            {
                copy.Draw(face, new Bgr(Color.Yellow), thickness);
            }

            return copy;
        }

    }
}
