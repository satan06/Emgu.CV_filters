using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Windows.Forms;

namespace Introduction
{
    class DetectionAssembler
    {
        public interface IPrimitive
        {
            void Create();
        }

        internal class Triangle : IPrimitive
        {
            public void Create()
            {
                Console.WriteLine("Creating tringle detections on image");
            }
        }

        public interface IPrimitiveFactory
        {
            IPrimitive Detect(VectorOfPoint vector, int minArea);
        }

        internal class TrianglesFactory : IPrimitiveFactory
        {
            public IPrimitive Detect(VectorOfPoint vector, int minArea)
            {
                Console.WriteLine($"Detecting triangles on minimum area {minArea}");
                return new Triangle();
            }
        }

        public class PrimitiveDetector
        {
            private List<Tuple<string, IPrimitiveFactory>> factories =
                new List<Tuple<string, IPrimitiveFactory>>();

            public PrimitiveDetector()
            {
                foreach(var t in typeof(PrimitiveDetector).Assembly.GetTypes())
                {
                    if(typeof(IPrimitiveFactory).IsAssignableFrom(t) &&
                        !t.IsInterface)
                    {
                        factories.Add(Tuple.Create(
                            t.Name.Replace("Factory", string.Empty),
                            (IPrimitiveFactory)Activator.CreateInstance(t)));
                    }
                }
            }

            public void FindPrimitive()
            {
                for (var i = 0; i < factories.Count; i++)
                {
                    // Run through array of factories and use each detector
                }
            }
        }
    }
}
