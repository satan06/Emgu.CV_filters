using NUnit.Framework;
using Introduction;

namespace ImageFiltersTest
{
    [TestFixture]
    public class ImageColorCheckTest
    {
        [Test]
        public void ColorCheck_WithValidValue_Test()
        {
            // arrange
            ImageFilter f = new ImageFilter();
            const double min = 0;
            const double max = 255;
            const double color = 100;
            const byte expected = 100;

            // act
            byte result = f.ColorCheck(color, min, max);

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ColorCheck_WithLessValue_Test()
        {
            // arrange
            ImageFilter f = new ImageFilter();
            const double min = 0;
            const double max = 255;
            const double color = -156;
            const byte expected = 0;

            // act
            byte result = f.ColorCheck(color, min, max);

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ColorCheck_WithBiggerValue_Test()
        {
            // arrange
            ImageFilter f = new ImageFilter();
            const double min = 0;
            const double max = 255;
            const double color = 325;
            const byte expected = 255;

            // act
            byte result = f.ColorCheck(color, min, max);

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ColorCheck_WithMinEdgeValue_Test()
        {
            // arrange
            ImageFilter f = new ImageFilter();
            const double min = 0;
            const double max = 255;
            const double color = 0;
            const byte expected = 0;

            // act
            byte result = f.ColorCheck(color, min, max);

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ColorCheck_WithMaxEdgeValue_Test()
        {
            // arrange
            ImageFilter f = new ImageFilter();
            const double min = 0;
            const double max = 255;
            const double color = 255;
            const byte expected = 255;

            // act
            byte result = f.ColorCheck(color, min, max);

            // assert
            Assert.AreEqual(expected, result);
        }
    }

    [TestFixture]
    public class ImageSetOperationTest
    {
        [Test]
        public void SetOperation_Add_Test()
        {
            // arrange
            ImageFilter f = new ImageFilter();
            Data.Boolean b = Data.Boolean.Add;
            const double val = 125;
            const double subval = 100;
            const double expected = 225;

            // act
            byte result = f.SetOperaton(b, val, subval);

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SetOperation_Substract_Test()
        {
            // arrange
            ImageFilter f = new ImageFilter();
            Data.Boolean b = Data.Boolean.Substract;
            const double val = 125;
            const double subval = 100;
            const double expected = 25;

            // act
            byte result = f.SetOperaton(b, val, subval);

            // assert
            Assert.AreEqual(expected, result);
        }
    }

    [TestFixture]
    public class ImageCellShadingCheckTest
    {
        [Test]
        public void CellShad_Less50_Test()
        {
            // arrange
            ImageFilter f = new ImageFilter();
            const byte color = 12;
            const double expected = 0;

            // act
            byte result = f.CellShadingCheck(color);

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CellShad_Less100_Test()
        {
            // arrange
            ImageFilter f = new ImageFilter();
            const byte color = 73;
            const double expected = 25;

            // act
            byte result = f.CellShadingCheck(color);

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CellShad_Less150_Test()
        {
            // arrange
            ImageFilter f = new ImageFilter();
            const byte color = 127;
            const double expected = 180;

            // act
            byte result = f.CellShadingCheck(color);

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CellShad_Less200_Test()
        {
            // arrange
            ImageFilter f = new ImageFilter();
            const byte color = 199;
            const double expected = 210;

            // act
            byte result = f.CellShadingCheck(color);

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CellShad_Default_Test()
        {
            // arrange
            ImageFilter f = new ImageFilter();
            const byte color = 241;
            const double expected = 255;

            // act
            byte result = f.CellShadingCheck(color);

            // assert
            Assert.AreEqual(expected, result);
        }
    }

    [TestFixture]
    public class ImageThresholdNormalizeTest
    {
        [Test]
        public void Normalize_ValueTooSmall_Test()
        {
            // arrange
            ImageFilter f = new ImageFilter();
            const int value = 0;
            const int expected = 3;

            // act
            int result = f.ThresholdNormalize(value);

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Normalize_ValueIsEven_Test()
        {
            // arrange
            ImageFilter f = new ImageFilter();
            const int value = 4;
            const int expected = 5;

            // act
            int result = f.ThresholdNormalize(value);

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Normalize_ValueIsCorrect_0_Test()
        {
            // arrange
            ImageFilter f = new ImageFilter();
            const int value = 3;
            const int expected = 3;

            // act
            int result = f.ThresholdNormalize(value);

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Normalize_ValueIsCorrect_1_Test()
        {
            // arrange
            ImageFilter f = new ImageFilter();
            const int value = 101;
            const int expected = 101;

            // act
            int result = f.ThresholdNormalize(value);

            // assert
            Assert.AreEqual(expected, result);
        }
    }

    [TestFixture]
    public class ImagesimpNormalizeTest
    {
        [Test]
        public void Normalize_ValueSmall_0_Test()
        {
            // arrange
            ImageFilter f = new ImageFilter();
            const int value = -6;
            const float expected = 0.1f;

            // act
            float result = f.Normalize(value);

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Normalize_ValueSmall_1_Test()
        {
            // arrange
            ImageFilter f = new ImageFilter();
            const int value = 1;
            const float expected = 0.1f;

            // act
            float result = f.Normalize(value);

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Normalize_ValueLarge_0_Test()
        {
            // arrange
            ImageFilter f = new ImageFilter();
            const int value = 9;
            const float expected = 0.9f;

            // act
            float result = f.Normalize(value);

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Normalize_ValueLarge_1_Test()
        {
            // arrange
            ImageFilter f = new ImageFilter();
            const int value = 12;
            const float expected = 0.9f;

            // act
            float result = f.Normalize(value);

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Normalize_ValueInRange_Test()
        {
            // arrange
            ImageFilter f = new ImageFilter();
            const int value = 3;
            const float expected = 0.3f;

            // act
            float result = f.Normalize(value);

            // assert
            Assert.AreEqual(expected, result);
        }

    }
}
