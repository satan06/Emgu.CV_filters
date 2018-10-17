using Emgu.CV;
using Emgu.CV.Structure;
using static System.Math;

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

        #region BilinearInterpolation (Scale)

        public class ScaleInterp
        {
            // Interp preporation
            public int FloorX, FloorY;
            public double RatioX, RatioY, InvRatioX, InvRatioY;

            // Building data
            public double DataX, DataY, InvDataX, InvDataY;
        }

        public class ScaleInterpBuilder
        {
            protected ScaleInterp interp = new ScaleInterp();

            public ScaleInterpPrepBuilder Prep => new ScaleInterpPrepBuilder(interp);
            public ScaleInterpDataBuilder Dat => new ScaleInterpDataBuilder(interp);

            public static implicit operator ScaleInterp(ScaleInterpBuilder sb)
            {
                return sb.interp;
            }
        }

        public class ScaleInterpPrepBuilder : ScaleInterpBuilder
        {
            public ScaleInterpPrepBuilder(ScaleInterp interp)
            {
                this.interp = interp;
            }

            public ScaleInterpPrepBuilder Floor(int x, int y, float coefX, double coefY)
            {
                interp.FloorX = (int)(x / coefX);
                interp.FloorY = (int)(y / coefY);
                return this;
            }

            public ScaleInterpPrepBuilder Ratio(int x, int y, float coefX, double coefY)
            {
                interp.RatioX = x / coefX - interp.FloorX;
                interp.RatioY = y / coefY - interp.FloorY;
                return this;
            }

            public ScaleInterpPrepBuilder InvRatio()
            {
                interp.InvRatioX = 1 - interp.RatioX;
                interp.InvRatioY = 1 - interp.RatioY;
                return this;
            }
        }

        public class ScaleInterpDataBuilder : ScaleInterpBuilder
        {
            public ScaleInterpDataBuilder(ScaleInterp interp)
            {
                this.interp = interp;
            }

            public ScaleInterpDataBuilder SetDataX(byte src)
            {
                interp.DataX = (byte)(src * interp.RatioX);
                return this;
            }

            public ScaleInterpDataBuilder SetDataY(byte src)
            {
                interp.DataY = (byte)(src * interp.RatioX);
                return this;
            }

            public ScaleInterpDataBuilder SetInvDataX(byte src)
            {
                interp.InvDataX = (byte)(src * interp.InvRatioX);
                return this;
            }

            public ScaleInterpDataBuilder SetInvDataY(byte src)
            {
                interp.InvDataY = (byte)(src * interp.InvRatioX);
                return this;
            }
        }

        #endregion
        #region BilinearInterpolation (Rotate)

        public class RotateInterp
        {
            // Interp preporation
            public int FloorX, FloorY, InWidth, InHeight;
            public double RatioX, RatioY, InvRatioX, InvRatioY;

            // Building data
            public double DataX, DataY, InvDataX, InvDataY;
        }

        public class RotateInterpBuilder
        {
            protected RotateInterp interp = new RotateInterp();

            public RotateInterpPrepBuilder Prep => new RotateInterpPrepBuilder(interp);
            public RotateInterpDataBuilder Dat => new RotateInterpDataBuilder(interp);

            public static implicit operator RotateInterp(RotateInterpBuilder rb)
            {
                return rb.interp;
            }
        }

        public class RotateInterpPrepBuilder : RotateInterpBuilder
        {
            public RotateInterpPrepBuilder(RotateInterp interp)
            {
                this.interp = interp;
            }

            public RotateInterpPrepBuilder Dimens(int width, int height)
            {
                interp.InWidth = width;
                interp.InHeight = height;
                return this;
            }

            public RotateInterpPrepBuilder Floor(Point p, double angle)
            {
                interp.FloorX = (int)(Cos(-angle) * (interp.InWidth - p.Width) -
                                      Sin(-angle) * (interp.InHeight - p.Height) + p.Width);
                interp.FloorY = (int)(Sin(-angle) * (interp.InWidth - p.Width) +
                                      Cos(-angle) * (interp.InHeight - p.Height) + p.Height);
                return this;
            }

            public RotateInterpPrepBuilder Ratio(Point p, double angle)
            {
                interp.RatioX = Cos(-angle) * (interp.InWidth - p.Width) -
                                (Sin(-angle) * (interp.InHeight - p.Height)) + p.Width - interp.FloorX;
                interp.RatioY = Sin(-angle) * (interp.InWidth - p.Width) +
                                Cos(-angle) * (interp.InHeight - p.Height) + p.Height - interp.FloorY;
                return this;
            }

            public RotateInterpPrepBuilder InvRatio()
            {
                interp.InvRatioX = 1 - interp.RatioX;
                interp.InvRatioY = 1 - interp.RatioY;
                return this;
            }
        }

        public class RotateInterpDataBuilder : RotateInterpBuilder
        {
            public RotateInterpDataBuilder(RotateInterp interp)
            {
                this.interp = interp;
            }

            public RotateInterpDataBuilder SetDataX(byte src)
            {
                interp.DataX = (byte)(src * interp.RatioX);
                return this;
            }

            public RotateInterpDataBuilder SetDataY(byte src)
            {
                interp.DataY = (byte)(src * interp.RatioX);
                return this;
            }

            public RotateInterpDataBuilder SetInvDataX(byte src)
            {
                interp.InvDataX = (byte)(src * interp.InvRatioX);
                return this;
            }

            public RotateInterpDataBuilder SetInvDataY(byte src)
            {
                interp.InvDataY = (byte)(src * interp.InvRatioX);
                return this;
            }
        }

        #endregion

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
