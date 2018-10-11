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
    }
}
