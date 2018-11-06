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
            Detector Create(Data data);
        }

        internal class Triangle : IPrimitive
        {
            public Detector Create(Data data)
            {
                return new Detector(data)
                    .GaussianBlur()
                    .GetInterestArea()
                    .DetectContours();
            }
        }

        public interface IPrimitiveFactory
        {
            IPrimitive Detect(Detector detector);
        }

        internal class TrianglesFactory : IPrimitiveFactory
        {
            public IPrimitive Detect(Detector detector)
            {
                throw new NotImplementedException();
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

            public void FindAllPrimitives()
            {
                for (var i = 0; i < factories.Count; i++)
                {
                    // Run through array of factories and use each detector
                }
            }
        }
    }
}
