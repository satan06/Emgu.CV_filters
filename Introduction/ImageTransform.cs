using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using static Introduction.Data;

namespace Introduction
{
    /// <summary>
    /// Represents class with simple image transform operations
    /// </summary>
    public class ImageTransform
    {
        public delegate void Func<Targ0, Targ1, Targ2, Targ3>(Targ0 channel, Targ1 height, Targ2 width, Targ3 color);
        public delegate void FuncSimpl<Targ0, Targ1, Targ2>(Targ0 height, Targ1 width, Targ2 pixel);

        // Pixel image traversal
        private void EachPixel(FuncSimpl<int, int, Bgr> action)
        {
            for (int x = 0; x < sourceImage.Width; x++)
            {
                for (int y = 0; y < sourceImage.Height; y++)
                {
                    action(y, x, sourceImage[y, x]);
                }
            }
        }

        private void EachPixelChannel(Func<int, int, int, byte> action)
        {
            for (int channel = 0; channel < sourceImage.NumberOfChannels; channel++)
            {
                for (int x = 0; x < sourceImage.Width - 1; x++)
                {
                    for (int y = 0; y < sourceImage.Height - 1; y++)
                    {
                        action(channel, y, x, sourceImage.Data[y, x, channel]);
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
            Image<Bgr, byte> newImage = new Image<Bgr, byte>((int)(sourceImage.Width * scaleX),
                                                             (int)(sourceImage.Height * scaleY));

            EachPixel((height, width, pixel) =>
            {
                int newX = (int)(width * scaleX);
                int newY = (int)(height * scaleY);

                newImage[newY, newX] = pixel;
            });

            return newImage ?? throw new ArgumentNullException(paramName: nameof(newImage));
        }

        /// <summary>
        /// Reflect image
        /// </summary>
        /// <param name="rtype"> Reflection type</param>
        // Testing: OK
        public Image<Bgr, byte> Reflect(ReflType rtype)
        {
            Image<Bgr, byte> newImage = new Image<Bgr, byte>(sourceImage.Size);
            int[] param = ReflTypeToData(rtype);

            EachPixel((height, width, pixel) =>
            {
                int newX = width;
                int newY = height;

                if (param[0] == -1)
                {
                    newX = width * param[0] + sourceImage.Width - 1;
                }
                if (param[1] == -1)
                {
                    newY = height * param[1] + sourceImage.Height - 1;
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
            Image<Bgr, byte> newImage = new Image<Bgr, byte>(sourceImage.Width + FilterShiftOffset(type, value)[0],
                                                             sourceImage.Height + FilterShiftOffset(type, value)[1]);
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
        public Image<Bgr, byte> Rotate(Point p, double angle)
        {
            Image<Bgr, byte> result = new Image<Bgr, byte>(sourceImage.Size);
            double rad = ConvertToRad(angle);

            EachPixel((height, width, pixel) =>
            {
                int newX = (int)(Math.Cos(rad) * (width - p.Width) - 
                                 Math.Sin(rad) * (height - p.Height) + p.Width);

                int newY = (int)(Math.Sin(rad) * (width - p.Width) + 
                                 Math.Cos(rad) * (height - p.Height) + p.Height);

                if(newX < sourceImage.Width && newX >= 0 && newY < sourceImage.Height && newY >= 0)
                {
                    result[newY, newX] = pixel;
                }
            });

            return result ?? throw new ArgumentNullException(paramName: nameof(result));
        }

        /// <summary>
        /// Remove resulting artefacts from image after
        /// filtering operations
        /// </summary>
        /// <param name="img">Image to interpolate</param>
        /// <param name="par">Required width and height coeffs</param>
        // Testing: OK (tip: needs refactor ( too much variables )) 
        public Image<Bgr, byte> BilinearInterp(Image<Bgr, byte> img, params float [] par)
        {
            Image<Bgr, byte> result = new Image<Bgr, byte>(img.Size);
            var sn = new ScaleInterpBuilder();

            for (int channel = 0; channel < img.NumberOfChannels; channel++)
            {
                for (int x = 0; x < img.Width - 1; x++)
                {
                    for (int y = 0; y < img.Height - 1; y++)
                    {
                        ScaleInterp interp = sn
                            .Prep
                                .Floor(x, y, par[0], par[1])
                                .Ratio(x, y, par[0], par[1])
                                .InvRatio();
                        sn.Dat
                            .SetInvDataX(sourceImage.Data[interp.FloorY, interp.FloorX, channel])
                            .SetDataX(sourceImage.Data[interp.FloorY, interp.FloorX + 1, channel])
                            .SetInvDataY(sourceImage.Data[interp.FloorY + 1, interp.FloorX, channel])
                            .SetDataY(sourceImage.Data[interp.FloorY + 1, interp.FloorX + 1, channel]);

                        result.Data[y, x, channel] = IsPixelBlack(img.Data[y, x, channel],
                                                                 (byte)((interp.InvDataX + interp.DataX) * interp.InvRatioY +
                                                                        (interp.InvDataY + interp.DataY) * interp.RatioY));
                    }
                }
            }
            return result;
        }

        private byte IsPixelBlack(byte def, byte processed) => def == 0 ? processed : def;


        #region Additional methods

        private int[] ReflTypeToData(ReflType rtype)
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
                    (int)Math.Abs(sourceImage.Width * value), 0
                } :
                new int[] {
                    0, (int)Math.Abs(sourceImage.Height * value)
                };
        }

        private int[] FilterCoordinates(ShiftType type, float value, params int[] vs)
        {
            return new HorizontalSpecification(type, value).IsSatisfied(ShiftType.Horizontal) ?

                new int[] {
                    (int)Math.Abs(vs[0] + value * (sourceImage.Height - vs[1])),
                    vs[1]
                } :
                new int[] {
                    vs[0],
                    (int)Math.Abs(vs[1] + value * (sourceImage.Height - vs[0]))
                };
        }

        private double ConvertToRad(double angle) => Math.PI / 180 * angle;

        #endregion
    }
}
