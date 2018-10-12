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

        public Image<Bgr, byte> BilinearInterp(Image<Bgr, byte> img)
        {
            Image<Bgr, byte> result = new Image<Bgr, byte>(img.Size);

            EachPixelChannel((channel, width, height, color) =>
            {
                int floorX = (int)Math.Floor((double)width); // I don't get it! 
                int floorY = (int)Math.Floor((double)height);
                int ratioX = width - floorX;
                int ratioY = height - floorY;
                int inversRatioX = 1 - ratioX;
                int inversRatioY = 1 - ratioY;

                // Pixel writing logic here 
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
