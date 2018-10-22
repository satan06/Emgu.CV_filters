using NUnit.Framework;
using Introduction;
using System;
using System.Drawing;

namespace ImageFiltersTest
{
    [TestFixture]
    public class ImageTransformTest
    {
        [Test]
        public void ReflType_Horiz_Test()
        {
            // arrange
            ImageTransform t = new ImageTransform();
            Data.ReflType rt = Data.ReflType.Horizontal;
            int[] expected = { -1, 1 };

            // act
            int[] result = t.ReflTypeToData(rt);

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ReflType_Vert_Test()
        {
            // arrange
            ImageTransform t = new ImageTransform();
            Data.ReflType rt = Data.ReflType.Vertical;
            int[] expected = { 1, -1 };

            // act
            int[] result = t.ReflTypeToData(rt);

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ReflType_Diag_Test()
        {
            // arrange
            ImageTransform t = new ImageTransform();
            Data.ReflType rt = Data.ReflType.Diagonal;
            int[] expected = { -1, -1 };

            // act
            int[] result = t.ReflTypeToData(rt);

            // assert
            Assert.AreEqual(expected, result);
        }
    }

    [TestFixture]
    public class ImageConvertToRadTest
    {
        [Test]
        public void ConvertToRad_45Deg_Test()
        {
            // arrange
            ImageTransform t = new ImageTransform();
            double value = 45.0;
            double expected = Math.PI / 4;

            // act
            double result = t.ConvertToRad(value);

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ConvertToRad_90Deg_Test()
        {
            // arrange
            ImageTransform t = new ImageTransform();
            double value = 90.0;
            double expected = Math.PI / 2;

            // act
            double result = t.ConvertToRad(value);

            // assert
            Assert.AreEqual(expected, result);
        }
    }

    [TestFixture]
    public class ImageIsPixelBlackTest
    {
        [Test]
        public void IsPixelBlack_PxBlack_Test()
        {
            // arrange
            ImageTransform t = new ImageTransform();
            byte def = 0, process = 85;
            double expected = process;

            // act
            byte result = t.IsPixelBlack(def, process);

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void IsPixelBlack_PxNotBlack_Test()
        {
            // arrange
            ImageTransform t = new ImageTransform();
            byte def = 45, process = 85;
            double expected = def;

            // act
            byte result = t.IsPixelBlack(def, process);

            // assert
            Assert.AreEqual(expected, result);
        }
    }

    [TestFixture]
    public class SortingTest
    {
        [Test]
        public void SwapTest()
        {
            // arrange
            ImageTransform t = new ImageTransform();
            PointF a = new PointF(10, 45);
            PointF b = new PointF(11, 21);
            string expected = $"{a.ToString()}, {b.ToString()}";

            // act
            t.Swap(ref a, ref b);
            string result = $"{a.ToString()}, {b.ToString()}"; ;

            // assert
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void InsertionSortTest()
        {
            // arrange
            ImageTransform t = new ImageTransform();
            var points = new PointF[]
            {
                 new PointF(140, 195),
                 new PointF(213, 183),
                 new PointF(141, 246),
                 new PointF(211, 230),
            };
            var expected = new PointF[]
            {
                 new PointF(140, 195),
                 new PointF(141, 246),
                 new PointF(211, 230),
                 new PointF(213, 183),
            };

            // act
            var result = t.InsertionSort(points);

            // assert
            Assert.AreEqual(expected, result);
        }
    }
}
