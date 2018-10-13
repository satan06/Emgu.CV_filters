using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;

namespace Introduction
{
    /// <summary>
    /// Represents class with simple image transform operations
    /// </summary>
    public class ImageTransform
    {
        public delegate void Func<Targ0, Targ1, Targ2, Targ3>(Targ0 channel, Targ1 width, Targ2 height, Targ3 color);
        public delegate void FuncSimpl<Targ0, Targ1, Targ2>(Targ0 height, Targ1 width, Targ2 pixel);

        // Pixel image traversal
        private void EachPixel(FuncSimpl<int, int, Bgr> action)
        {
            for (int x = 0; x < Data.sourceImage.Width; x++)
            {
                for (int y = 0; y < Data.sourceImage.Height; y++)
                {
                    action(y, x, Data.sourceImage[y, x]);
                }
            }
        }

        private void EachPixelChannel(Func<int, int, int, byte> action)
        {
            for (int channel = 0; channel < Data.sourceImage.NumberOfChannels; channel++)
            {
                for (int x = 0; x < Data.sourceImage.Width; x++)
                {
                    for (int y = 0; y < Data.sourceImage.Height; y++)
                    {
                        action(channel, y, x, Data.sourceImage.Data[y, x, channel]);
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
            Image<Bgr, byte> newImage = new Image<Bgr, byte>((int)(Data.sourceImage.Width * scaleX),
                                                             (int)(Data.sourceImage.Height * scaleY));

            EachPixel((height, width, pixel) =>
            {
                int newX = (int)(width * scaleX);
                int newY = (int)(height * scaleY);

                newImage[newY, newX] = pixel;
            });

            return newImage;
        }

        /// <summary>
        /// Reflect image
        /// </summary>
        /// <param name="rtype"> Reflection type</param>
        // Testing: OK
        public Image<Bgr, byte> Reflect(Data.ReflType rtype)
        {
            Image<Bgr, byte> newImage = new Image<Bgr, byte>(Data.sourceImage.Size);
            int[] param = ReflTypeToData(rtype);

            EachPixel((height, width, pixel) =>
            {
                int newX = width;
                int newY = height;

                if (param[0] == -1)
                {
                    newX = width * param[0] + Data.sourceImage.Width - 1;
                }
                if (param[1] == -1)
                {
                    newY = height * param[1] + Data.sourceImage.Height - 1;
                }
                newImage[newY, newX] = pixel;
            });

            return newImage;
        }

        public Image<Bgr, byte> BilinearInterp(Image<Bgr, byte> img, params float [] par)
        {
            Image<Bgr, byte> result = new Image<Bgr, byte>(img.Size);

            EachPixelChannel((channel, width, height, color) =>
            {
                int floorX = (int)Math.Floor(width / par[0]);
                int floorY = (int)Math.Floor(height * par[1]);
                double ratioX = width / par[0] - floorX;
                double ratioY = height / par[1] - floorY;
                double inversRatioX = 1 - ratioX;
                double inversRatioY = 1 - ratioY;

                byte invDataX = (byte)(Data.sourceImage.Data[width, height, channel] * inversRatioX);
                byte dataX = (byte)(Data.sourceImage.Data[width, height, channel] * ratioX);
                byte invDataY = (byte)(Data.sourceImage.Data[width, height, channel] * inversRatioY);
                byte dataY = (byte)(Data.sourceImage.Data[width, height, channel] * ratioY);

                if(img.Data[width, height, channel] == 0)
                {
                    img.Data[width, height, channel] = (byte)((invDataX + dataX) * inversRatioX + 
                                                                 (invDataY + dataY) * ratioY);
                }
                else
                {
                    result.Data[width, height, channel] = color;
                }
                                                             
            });

            return result;
        }

        #region Additional methods

        private int[] ReflTypeToData(Data.ReflType rtype)
        {
            bool isHoriz = rtype == Data.ReflType.Horizontal;
            bool isVert = rtype == Data.ReflType.Vertical;
            bool isDiag = rtype == Data.ReflType.Diagonal;

            return isHoriz ? new int[] { -1, 1 } : isVert ? 
                             new int[] { 1, -1 } : new int[] { -1, -1 };
        }

        #endregion
    }
}
