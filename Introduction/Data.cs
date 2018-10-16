using Emgu.CV;
using Emgu.CV.Structure;

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

        // Implementing multiple classes for diff points (SOLID's Open/Close)
        #region Point Templates

        public abstract class Point
        {
            public virtual int Width { get; set; } = 0;
            public virtual int Height { get; set; } = 0;
        }

        public class TopLeft : Point
        {
            public override int Width { get => base.Width; set => base.Width = value; }
            public override int Height { get => base.Height; set => base.Height = value; }
        }

        public class TopRight : Point
        {
            public TopRight(int width)
            {
                Width = width;
            }

            public override int Width { get => base.Width; set => base.Width = value; }
        }

        public class BottomLeft : Point
        {
            public BottomLeft(int height)
            {
                Height = height;
            }

            public override int Height { get => base.Height; set => base.Height = value; }
        }
    
        public class Center : Point
        {
            public Center(int width, int height)
            {
                Width = width / 2;
                Height = height / 2;
            }

            public override int Width { get => base.Width; set => base.Width = value; }
            public override int Height { get => base.Height; set => base.Height = value; }
        }

        public class CustomPoint : Point
        {
            public CustomPoint(int width, int height)
            {
                Width = width;
                Height = height;
            }

            public override int Width { get => base.Width; set => base.Width = value; }
            public override int Height { get => base.Height; set => base.Height = value; }
        }

        #endregion

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
