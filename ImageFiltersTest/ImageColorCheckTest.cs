using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Introduction;

namespace ImageFiltersTest
{
    [TestClass]
    public class ImageColorCheckTest
    {
        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

    [TestClass]
    public class ImageSetOperationTest
    {
        [TestMethod]
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

        [TestMethod]
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

    [TestClass]
    public class ImageCellShadingCheckTest
    {
        [TestMethod]
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
    }
}
