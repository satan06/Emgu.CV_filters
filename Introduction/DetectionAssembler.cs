using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Introduction
{
    class DetectionAssembler
    {
        public interface IPrimitive
        {
            Image<Bgr, byte> Detect(Data data);
        }

        public abstract class Constructive
        {
            public Detector Detector { get; }
            public int PrimitiveCount { get; set; }
            public Constructive(Detector detector) => Detector = detector;
        }

        public class Unfiltered : Constructive, IPrimitive
        {
            public Unfiltered(Detector detector) : base(detector)
            {
            }

            public Image<Bgr, byte> Detect(Data data)
            {
                for (int i = 0; i < Detector.ApproxContours.Size; i++)
                {
                    var points = Detector.ApproxContours[i].ToArray();

                    Detector.ImageCopy.Draw(points, new Bgr(Color.Blue), 2);
                    PrimitiveCount++;
                }
                return Detector.ImageCopy;
            }
        }

        public class Triangle : Constructive, IPrimitive
        {
            public Triangle(Detector detector) : base(detector)
            {
            }

            public int MinArea { get; set; } = 256;

            public Image<Bgr, byte> Detect(Data data)
            {
                for (int i = 0; i < Detector.ApproxContours.Size; i++)
                {
                    if (Detector.ApproxContours[i].Size == 3 &&
                        CvInvoke.ContourArea(Detector.ApproxContours[i], false) > MinArea)
                    {
                        var points = Detector.ApproxContours[i].ToArray();

                        Detector.ImageCopy.Draw(new Triangle2DF(points[0], points[1], points[2]),
                        new Bgr(Color.GreenYellow), 2);

                        PrimitiveCount++;
                    }
                }

                return Detector.ImageCopy;
            }
        }

        internal class Circle : Constructive ,IPrimitive
        {
            public Circle(Detector detector) : base(detector)
            {
            }

            public Image<Bgr, byte> Detect(Data data)
            {
                foreach (CircleF circle in Detector.Circles)
                {
                    Detector.ImageCopy.Draw(circle, new Bgr(Color.Blue), 2);
                    PrimitiveCount++;
                }

                return Detector.ImageCopy;
            }
        }

        internal class Rectangle : Constructive, IPrimitive
        {
            public Rectangle(Detector detector) : base(detector)
            {
            }

            public Image<Bgr, byte> Detect(Data data)
            {
                for (int i = 0; i < Detector.ApproxContours.Size; i++)
                {
                    if (IsRectangle(Detector.ApproxContours[i].ToArray()))
                    {
                        Detector.ImageCopy.Draw(CvInvoke.MinAreaRect(
                            Detector.ApproxContours[i]), new Bgr(Color.GreenYellow), 2);
                        PrimitiveCount++;
                    }
                }

                return Detector.ImageCopy;
            }

            private bool IsRectangle(Point[] points)
            {
                int delta = 10;
                LineSegment2D[] edges = PointCollection.PolyLine(points, true);

                for (int i = 0; i < edges.Length; i++)
                {
                    double angle = Math.Abs(edges[(i + 1) %
                    edges.Length].GetExteriorAngleDegree(edges[i]));

                    if (angle < 90 - delta || angle > 90 + delta)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
