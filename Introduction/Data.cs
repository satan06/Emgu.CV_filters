using Emgu.CV;
using Emgu.CV.Structure;
using System;

namespace Introduction
{
    public static class Data
    { 
        public static Image<Bgr, byte> sourceImage;
        public static Image<Bgr, byte> tempImage;

        #region Enums

        /// <summary>
        /// HSV channels 
        /// </summary>
        public enum HSV : byte
        {
            Hue,
            Saturation,
            Value
        }

        /// <summary>
        /// Two general boolean operations struct
        /// </summary>
        public enum Boolean
        {
            Add,
            Substract
        }

        /// <summary>
        /// BGR channels 
        /// </summary>
        public enum BGR : byte
        {
            Blue,
            Green,
            Red
        }

        /// <summary>
        /// Image reflection types
        /// </summary>
        public enum ReflType
        {
            Horizontal,
            Vertical,
            Diagonal
        }

        /// <summary>
        /// Image shearing types
        /// </summary>
        public enum ShiftType
        {
            Horizontal,
            Vertical
        }

        #endregion
        #region Window filter matrixes

        /// <summary>
        /// Sharpen effect
        /// </summary>
        static public int[,] Sharp;

        /// <summary>
        /// Embos effect
        /// </summary>
        static public int[,] Embos;

        /// <summary>
        /// Edges detecting effect
        /// </summary>
        static public int[,] Edges;

        /// <summary>
        /// Custom matrix. Empty by default
        /// </summary>
        static public int[,] Custom;

        #endregion

        public interface ISpecification<T>
        {
            bool IsSatisfied(T t);
        }

        public class HorizontalSpecification : ISpecification<ShiftType>
        {
            public HorizontalSpecification(ShiftType shift, float value)
            {
                Shift = shift;
                Value = value;
            }

            public ShiftType Shift { get; set; }
            public float Value { get; set; }

            public bool IsSatisfied(ShiftType t) => t == Shift;
        }

        static Data()
        {
            int[,] sTemp = { { -1, -1, -1 },
                             { -1, 9, -1 },
                             { -1, -1, -1 } };

            int[,] emTemp = { { -4, -2, 0 },
                              { -2, 1, 2 },
                              { 0, 2, 4 } };

            int[,] edTemp = { { 0, 0, 0 },
                              { -4, 4, 0 },
                              { 0, 0, 0 } };

            Sharp = sTemp;
            Embos = emTemp;
            Edges = edTemp;
            Custom = new int[3, 3];
        }
    }
}
