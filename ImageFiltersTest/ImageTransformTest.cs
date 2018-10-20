using NUnit.Framework;
using Introduction;
using System;

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
    public class ImageFilterShiftOffsetTest
    {
        [Test]
        public void FilterShiftOffset_IsHorizontalSpec_Test()
        {
            // arrange
            ImageTransform t = new ImageTransform();
            Data.ShiftType type = Data.ShiftType.Horizontal;
            float value = 0.25f;
            const int width = 640;
            int[] expected = { (int)Math.Abs(width * value), 0 }; 

            // act
            int [] result = t.FilterShiftOffset(type, value);

            // assert
            Assert.AreEqual(expected, result);
        }
    }
}
