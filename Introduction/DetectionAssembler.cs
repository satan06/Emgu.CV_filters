using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Introduction
{
    class DetectionAssembler
    {
        public static int MinArea { get; set; } = 256;

        public interface IPrimitive
        {
            Image<Bgr, byte> Detect(Data data);
        }

        public abstract class BaseCreator
        {
            public virtual Detector Create(Data data)
            {
                return new Detector(data)
                    .GaussianBlur()
                    .GetInterestArea()
                    .DetectContours()
                    .Approx();
            }
        }

        internal class Triangle : BaseCreator, IPrimitive
        {
            public Image<Bgr, byte> Detect(Data data)
            {
                Detector detector = new Triangle().Create(data);

                for (int i = 0; i < detector.ApproxContours.Size; i++)
                {
                    if (detector.ApproxContours[i].Size == 3 &&
                        CvInvoke.ContourArea(detector.ApproxContours[i], false) > MinArea)
                    {
                        var points = detector.ApproxContours[i].ToArray();

                        detector.ImageCopy.Draw(new Triangle2DF(points[0], points[1], points[2]),
                        new Bgr(Color.GreenYellow), 2);
                    }
                }

                return detector.ImageCopy;
            }
        }

        internal class Circle : BaseCreator, IPrimitive
        {
            public Image<Bgr, byte> Detect(Data data)
            {
                Detector detector = new Circle().Create(data);

                foreach (CircleF circle in detector.Circles)
                {
                    detector.ImageCopy.Draw(circle, new Bgr(Color.Blue), 2);                }

                return detector.ImageCopy;
            }

            public override Detector Create(Data data)
            {
                return new Detector(data)
                    .GaussianBlur()
                    .DetectContours(250, 36, 50, 150);
            }
        }
    }
}
