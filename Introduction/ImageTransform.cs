﻿using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using static Introduction.Data;

namespace Introduction
{
    /// <summary>
    /// Represents class with simple image transform operations
    /// </summary>
    public class ImageTransform
    {
        public delegate void FuncSimpl<Targ0, Targ1, Targ2>(Targ0 height, Targ1 width, Targ2 pixel);
        public delegate void Func<Targ0, Targ1, Targ2, Targ3>(Targ0 channel, Targ1 height, Targ2 width, Targ3 color);
        public Data Data;

        public ImageTransform(Data data)
        {
            Data = data;
        }

        public ImageTransform() { }

        // Pixel image traversal
        private void EachPixel(FuncSimpl<int, int, Bgr> action)
        {
            for (int x = 0; x < Data.SourceImage.Width; x++)
            {
                for (int y = 0; y < Data.SourceImage.Height; y++)
                {
                    action(y, x, Data.SourceImage[y, x]);
                }
            }
        }
        private void EachPixelChannel(Func<int, int, int, byte> action, Image<Bgr, byte> image)
        {
            for (int channel = 0; channel < image.NumberOfChannels; channel++)
            {
                for (int x = 0; x < image.Width - 1; x++)
                {
                    for (int y = 0; y < image.Height - 1; y++)
                    {
                        action(channel, y, x, image.Data[y, x, channel]);
                    }
                }
            }
        }

        /// <summary>
        /// Scales image
        /// </summary>
        /// <param name="scaleX">Width scale factor</param>
        /// <param name="scaleY">Height scale factor</param>
        // Testing: OK
        public Image<Bgr, byte> Scale(float scaleX, float scaleY)
        {
            Image<Bgr, byte> newImage = new Image<Bgr, byte>((int)(Data.SourceImage.Width * scaleX),
                                                             (int)(Data.SourceImage.Height * scaleY));

            EachPixel((height, width, pixel) =>
            {
                newImage[(int)(height * scaleY), (int)(width * scaleX)] = pixel;
            });

            return BilinearInterp(newImage, scaleX, scaleY);
        }

        /// <summary>
        /// Reflect image
        /// </summary>
        /// <param name="rtype"> Reflection type</param>
        // Testing: OK
        public Image<Bgr, byte> Reflect(ReflType rtype)
        {
            Image<Bgr, byte> newImage = new Image<Bgr, byte>(Data.SourceImage.Size);
            int[] param = ReflTypeToData(rtype);

            EachPixel((height, width, pixel) =>
            {
                int newX = width;
                int newY = height;

                if (param[0] == -1)
                {
                    newX = width * param[0] + Data.SourceImage.Width - 1;
                }
                if (param[1] == -1)
                {
                    newY = height * param[1] + Data.SourceImage.Height - 1;
                }
                newImage[newY, newX] = pixel;
            });

            return newImage ?? throw new ArgumentNullException(paramName: nameof(newImage));
        }

        /// <summary>
        /// Shearing image relative to 
        /// <see cref="ShiftType"></see>
        /// </summary>
        /// <param name="type">Shift type</param>
        /// <param name="value">Shifting intensity</param>
        // Testing: OK
        public Image<Bgr, byte> Shear(ShiftType type, float value)
        {
            Image<Bgr, byte> newImage = new Image<Bgr, byte>(Data.SourceImage.Width + FilterShiftOffset(type, value)[0],
                                                             Data.SourceImage.Height + FilterShiftOffset(type, value)[1]);
            EachPixel((height, width, pixel) =>
            {
                int newX = FilterCoordinates(type, value, width, height)[0];
                int newY = FilterCoordinates(type, value, width, height)[1];

                newImage[newY, newX] = pixel;
            });

            return newImage ?? throw new ArgumentNullException(paramName: nameof(newImage));
        }

        /// <summary>
        /// Rotates image
        /// </summary>
        /// <param name="p">Anchor of the rotation</param>
        /// <param name="angle">Rotation angle</param>
        // Testing: OK
        public Image<Bgr, byte> Rotate(CstPoint p, double angle)
        {
            Image<Bgr, byte> result = new Image<Bgr, byte>(Data.SourceImage.Size);

            angle = ConvertToRad(angle);

            EachPixel((height, width, pixel) =>
            {
                int newX = (int)(Math.Cos(angle) * (width - p.X) -
                                 Math.Sin(angle) * (height - p.Y) + p.X);

                int newY = (int)(Math.Sin(angle) * (width - p.X) +
                                 Math.Cos(angle) * (height - p.Y) + p.Y);

                if (newX < Data.SourceImage.Width && newX >= 0 && 
                    newY < Data.SourceImage.Height && newY >= 0)
                {
                    result[newY, newX] = pixel;
                }
            });

            return BilinearInterp(result, p, angle) ?? throw new ArgumentNullException(paramName: nameof(result));
        }

        /// <summary>
        /// Remove resulting artefacts from image after
        /// filtering operations
        /// </summary>
        /// <param name="img">Image to interpolate</param>
        /// <param name="par">Required width and height coeffs</param>
        // Testing: OK
        public Image<Bgr, byte> BilinearInterp(Image<Bgr, byte> img, params float[] par)
        {
            Image<Bgr, byte> result = new Image<Bgr, byte>(img.Size);
            var sn = new ScaleInterpBuilder();

            EachPixelChannel((channel, height, width, color) =>
            {
                ScaleInterp interp = sn
                    .Prep
                    .Floor(width, height, par[0], par[1])
                    .Ratio(width, height, par[0], par[1])
                    .InvRatio();

                if (interp.FloorX < Data.SourceImage.Width - 1 && interp.FloorX >= 0 &&
                    interp.FloorY < Data.SourceImage.Height - 1 && interp.FloorY >= 0)
                {
                    sn.Dat
                        .SetInvDataX(Data.SourceImage.Data[interp.FloorY, interp.FloorX, channel])
                        .SetDataX(Data.SourceImage.Data[interp.FloorY, interp.FloorX + 1, channel])
                        .SetInvDataY(Data.SourceImage.Data[interp.FloorY + 1, interp.FloorX, channel])
                        .SetDataY(Data.SourceImage.Data[interp.FloorY + 1, interp.FloorX + 1, channel]);

                    result.Data[height, width, channel] = (byte)((interp.InvDataX + interp.DataX) * interp.InvRatioY +
                                                                 (interp.InvDataY + interp.DataY) * interp.RatioY);
                }

            }, img);

            return result ?? throw new ArgumentNullException(paramName: nameof(result));
        }

        // Rotation overload => Testing: OK
        public Image<Bgr, byte> BilinearInterp(Image<Bgr, byte> img, CstPoint p, double angle)
        {
            Image<Bgr, byte> result = new Image<Bgr, byte>(Data.SourceImage.Size);
            var rn = new RotateInterpBuilder();

            EachPixelChannel((channel, height, width, color) =>
            {
                RotateInterp interp = rn
                    .Prep
                        .Dimens(width, height)
                        .Floor(p, angle)
                        .Ratio(p, angle)
                        .InvRatio();

                if (interp.FloorX < Data.SourceImage.Width - 1 && interp.FloorX >= 0 &&
                    interp.FloorY < Data.SourceImage.Height - 1 && interp.FloorY >= 0)
                {
                    rn.Dat
                        .SetInvDataX(Data.SourceImage.Data[interp.FloorY, interp.FloorX, channel])
                        .SetDataX(Data.SourceImage.Data[interp.FloorY, interp.FloorX + 1, channel])
                        .SetInvDataY(Data.SourceImage.Data[interp.FloorY + 1, interp.FloorX, channel])
                        .SetDataY(Data.SourceImage.Data[interp.FloorY + 1, interp.FloorX + 1, channel]);

                    result.Data[height, width, channel] = (byte)((interp.InvDataX + interp.DataX) * interp.InvRatioY +
                                                                 (interp.InvDataY + interp.DataY) * interp.RatioY);
                }

            }, img);

            return result ?? throw new ArgumentNullException(paramName: nameof(result));
        }

        public Image<Bgr, byte> Homograph(PointF[] src)
        {
            var destPoints = new PointF[]
            {
                 new PointF(0, 0),
                 new PointF(0, Data.SourceImage.Height - 1),
                 new PointF(Data.SourceImage.Width - 1, Data.SourceImage.Height - 1),
                 new PointF(Data.SourceImage.Width - 1, 0)
            };
            var homographyMatrix = CvInvoke.GetPerspectiveTransform(src, destPoints);
            var destImage = new Image<Bgr, byte>(Data.SourceImage.Size);

            CvInvoke.WarpPerspective(Data.SourceImage, destImage, homographyMatrix, destImage.Size);

            return destImage;
        }

        public void DrawPoint(Point center, int radius, int thickness)
        {
            var color = new Bgr(Color.Blue).MCvScalar;

            CvInvoke.Circle(Data.SourceImage, center, radius, color, thickness);
        }

        #region Additional methods

        public int[] ReflTypeToData(ReflType rtype)
        {
            bool isHoriz = rtype == ReflType.Horizontal;
            bool isVert = rtype == ReflType.Vertical;
            bool isDiag = rtype == ReflType.Diagonal;

            return isHoriz ? new int[] { -1, 1 } : isVert ?
                             new int[] { 1, -1 } : new int[] { -1, -1 };
        }

        private int[] FilterShiftOffset(ShiftType type, float value)
        {
            return new HorizontalSpecification(type, value).IsSatisfied(ShiftType.Horizontal) ?

                new int[] {
                    (int)Math.Abs(Data.SourceImage.Width * value), 0
                } :
                new int[] {
                    0, (int)Math.Abs(Data.SourceImage.Height * value)
                };
        }

        private int[] FilterCoordinates(ShiftType type, float value, params int[] vs)
        {
            return new HorizontalSpecification(type, value).IsSatisfied(ShiftType.Horizontal) ?

                new int[] {
                    (int)Math.Abs(vs[0] + value * (Data.SourceImage.Height - vs[1])),
                    vs[1]
                } :
                new int[] {
                    vs[0],
                    (int)Math.Abs(vs[1] + value * (Data.SourceImage.Height - vs[0]))
                };
        }

        public double ConvertToRad(double angle) => angle * Math.PI / 180 ;
        public byte IsPixelBlack(byte def, byte processed) => def == 0 ? processed : def;

        public void Swap(ref PointF a, ref PointF b)
        {
            PointF temp;

            temp = a;
            a = b;
            b = temp;
        }

        public PointF[] InsertionSort(PointF[] input)
        { 
            for (int i = 1; i < input.Length; i++)
            {
                for (int j = i; j > 0; j--)
                {
                    if (input[j].X < input[j - 1].X)
                    {
                        Swap(ref input[j], ref input[j - 1]);
                    }
                }
            }
            return input;
        }

        public PointF [] CoordSort (PointF [] points)
        {
            points = InsertionSort(points);

            if(points[0].Y > points[1].Y)
            {
                Swap(ref points[1], ref points[0]);
            }

            if (points[2].Y < points[3].Y)
            {
                Swap(ref points[3], ref points[2]);
            }

            return points;
        }

        #endregion
    }
}
