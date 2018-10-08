﻿using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Introduction
{
    /// <summary>
    /// Represents class with simple image filter effects
    /// </summary>
    public class ImageFilter
    {
        public Image<Bgr, byte> sourceImage;
        public Image<Bgr, byte> tempImage;

        public enum HSV : byte
        {
            Hue,
            Saturation,
            Value
        }

        public enum Boolean
        {
            Add,
            Substract
        }

        private delegate void Func<Targ0, Targ1, Targ2, Targ3>(Targ0 channel, Targ1 width, Targ2 height, Targ3 color);

        private void EachPixel(Func<int, int, int, byte> action)
        {
            for (int channel = 0; channel < sourceImage.NumberOfChannels; channel++)
            {
                for (int x = 0; x < sourceImage.Width; x++)
                {
                    for (int y = 0; y < sourceImage.Height; y++)
                    {
                        action(channel, y, x, sourceImage.Data[y, x, channel]);
                    }
                }
            }
        }

        /// <summary>
        /// Opens new image
        /// </summary>
        /// <param name="fileName">Path to image</param>
        /// <param name="container">Where to put image. Use
        /// <see cref="sourceImage"></see>
        /// to work with main sourse image. Use
        /// <see cref="tempImage"></see>
        /// to load temp image and work with booleans
        /// </param>
        /// <overload>Opens new image and resizes it</overload>
        public void OpenFile(string fileName, ref Image<Bgr, byte> container)
        {
            container = new Image<Bgr, byte>(fileName);
        }

        public void OpenFile(string fileName, ref Image<Bgr, byte> container, int width, int height)
        {
            container = new Image<Bgr, byte>(fileName).Resize(width, height, Inter.Linear);
        }

        private Image<Gray, byte> ToGray()
        {
            if (sourceImage == null)
            {
                return null;
            }

            Image<Gray, byte> grayImage = sourceImage.Convert<Gray, byte>();
            return grayImage;
        }

        /// <summary>
        /// Reduces noises on the image
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        // Testing: OK
        public Image<Bgr, byte> Denoise(Image<Bgr, byte> image)
        {
            if (image == null)
            {
                return null;
            }

            var destImage = image.PyrDown().PyrUp();
            return destImage;
        }

        /// <summary>
        /// Imposes a canny effect on the image
        /// </summary>
        /// <param name="threshold">Result image threshold value</param>
        /// <param name="thresholdLinking">Intensity of linking edges</param>
        /// <returns></returns>
        // Testing: OK
        public Image<Gray, byte> CannyFilter(double threshold = 80.0, double thresholdLinking = 40.0)
        {
            if (sourceImage == null)
            {
                return null;
            }
            var cannyEdges = sourceImage.Canny(threshold, thresholdLinking);

            return cannyEdges;
        }
        /// <summary>
        /// Imposes a cell shading effect on the image
        /// </summary>
        // Testing: OK
        public Image<Bgr, byte> CellShading()
        {
            if (sourceImage == null)
            {
                return null;
            }

            var cannyEdgesBgr = CannyFilter().Convert<Bgr, byte>();
            var resultImage = sourceImage.Sub(cannyEdgesBgr);

            EachPixel((channel, width, height, color) =>
            {
                resultImage.Data[width, height, channel] = CellShadingCheck(resultImage.Data[width, height, channel]);
            });

            Denoise(resultImage);
            return resultImage;
        }

        private byte CellShadingCheck(byte color)
        {
            var condArg0 = (color <= 50) ? 0 : 
                            (color <= 100) ? 25 : 
                            (color <= 150) ? 180 : 
                            (color <= 200) ? 210 : 255;

            return (byte)condArg0; 
        }

        public Image<Gray, byte> ChannelSplit(byte channelIndex)
        {
            if(sourceImage == null)
            {
                return null;
            }

            var channel = sourceImage.Split()[channelIndex];
            return channel;
        }

        /// <summary>
        /// Combines image channels all together in one particular image
        /// </summary>
        /// <param name="channels">Array of image channels</param>
        // Testing: OK
        public Image<Bgr, byte> ChannelCombine(List<Image<Gray, byte>> channels)
        {
            VectorOfMat vm = new VectorOfMat();
            Image<Bgr, byte> destImage = new Image<Bgr, byte>(sourceImage.Size);
                
            for (byte ch = 0; ch < channels.Count; ch++)
            {
                vm.Push(channels[ch]);
            }
            CvInvoke.Merge(vm, destImage);
            
            return destImage;
        }

        /// <summary>
        /// Converts this image into black and white copy
        /// </summary>
        // Testing: OK
        public Image<Gray, byte> ConvertToBW()
        {
            if (sourceImage == null)
            {
                return null;
            }
            Image<Gray, byte> grayImage = new Image<Gray, byte>(sourceImage.Size);

            EachPixel((channel, width, height, color) => 
            {
                grayImage.Data[width, height, 0] = Convert.ToByte(0.299 * sourceImage.Data[width, height, 2] + 0.587 *
                sourceImage.Data[width, height, 1] + 0.114 * sourceImage.Data[width, height, 0]);
            });

            return grayImage;
        }

        /// <summary>
        /// Imposes a sepia effect on the image
        /// </summary>
        // Testing: OK
        public Image<Bgr, byte> Sepia() 
        {
            Image<Bgr, byte> destImage = new Image<Bgr, byte>(sourceImage.Size);
            byte blue, green, red;

            EachPixel((channel, width, height, color) => 
            {
                blue = sourceImage.Data[width, height, 0];
                green = sourceImage.Data[width, height, 1];
                red = sourceImage.Data[width, height, 2];

                destImage.Data[width, height, 0] = ColorCheck(blue * 0.272 + green * 0.534 + blue * 0.131, 0, 255);
                destImage.Data[width, height, 1] = ColorCheck(blue * 0.349 + green * 0.686 + blue * 0.168, 0, 255);
                destImage.Data[width, height, 2] = ColorCheck(blue * 0.393 + green * 0.769 + blue * 0.189, 0, 255);
            });

            return destImage;
        }

        /// <summary>
        /// Changes constrast of the image
        /// </summary>
        /// <typeparam name="T">Image type</typeparam>
        /// <param name="image">Image to work with</param>
        /// /// <param name="value">Intensity value.</param>
        // Testing: OK
        public Image<T, byte> Contrast<T>(Image<T, byte> img, double value = 5.0) where T : struct, IColor
        {
            Image<T, byte> destImage = new Image<T, byte>(sourceImage.Size);
            double pixel;

            EachPixel((channel, width, height, color) =>
            {
                pixel = img.Data[width, height, channel] * value;
                destImage.Data[width, height, channel] = ColorCheck(pixel, 0, 255);
            });

            return destImage;
        }

        private byte ColorCheck(double color, double min, double max)
        {
            var condition = (color < min) ? min : (color > max) ? max : color;
            return (byte)condition;
        }

        /// <summary>
        /// Changes brightness of the image
        /// </summary>
        /// <typeparam name="T">Image type</typeparam>
        /// <param name="image">Image to work with</param>
        /// /// <param name="value">Intensity value.</param>
        // Testing: OK
        public Image<T, byte> Brightness<T>(Image<T, byte> img, double value = 25.0) where T : struct, IColor
        {
            Image<T, byte> destImage = new Image<T, byte>(sourceImage.Size);
            double pixel;

            EachPixel((channel, width, height, color) =>
            {
                pixel = img.Data[width, height, channel] + value;
                destImage.Data[width, height, channel] = ColorCheck(pixel, 0, 255);
            });

            return destImage;
        }

        /// <summary>
        /// Changes selected HSV channel of the image
        /// </summary>
        /// <param name="value">Intensity value</param>
        /// <param name="hsv">The HSV channel</param>
        // Testing: OK
        public Image<Hsv, byte> HSVFilter(double value, HSV hsv)
        {
            Image<Hsv, byte> destImage = sourceImage.Convert<Hsv, byte>();

            EachPixel((channel, width, height, color) =>
            {
                if (hsv == 0)
                {
                    destImage.Data[width, height, (int)hsv] =
                    ColorCheck(destImage.Data[width, height, (int)hsv] + value, 0, 180);
                }
                else
                {
                    destImage.Data[width, height, (int)hsv] =
                    ColorCheck(destImage.Data[width, height, (int)hsv] + value, 0, 100);
                }
            });

            return destImage;
        }

        /// <summary>
        /// Performs boolean operation with another image
        /// </summary>
        /// <typeparam name="T">Image type</typeparam>
        /// <param name="img">Image to work with</param>
        /// <param name="b">Addition or substruction operation</param>
        /// <param name="value">Sub image intensity</param>
        // Testing: OK
        public Image<T, byte> BooleanOperation<T>(Image<T, byte> img, Boolean b, double value) where T : struct, IColor
        {
            Image<T, byte> result = new Image<T, byte>(sourceImage.Size);

            EachPixel((channel, width, height, color) =>
            {
                color = SetOperaton(b, img.Data[width, height, channel], 
                        tempImage.Data[width, height, channel] * value);
                result.Data[width, height, channel] = color;
            });

            return result;
        }

        /// <summary>
        /// Performs intersection operation with another image
        /// </summary>
        /// <typeparam name="T">Image type</typeparam>
        /// <param name="image">Image to work with</param>
        /// <returns></returns>
        // Testing: OK
        public Image<T, byte> Intersection<T>(Image<T, byte> img) where T : struct, IColor
        {
            Image<T, byte> result = new Image<T, byte>(img.Size);

            EachPixel((channel, width, height, color) =>
            {
                if (tempImage.Data[width, height, channel] == 0)
                {
                    result.Data[width, height, channel] = 0;
                }
                else if(tempImage.Data[width, height, channel] > 0)
                {
                    result.Data[width, height, channel] = sourceImage.Data[width, height, channel];
                }
            });

            return result;
        }

        /// <summary>
        /// Performs watercolor effect in the image
        /// </summary>
        /// <typeparam name="T">Image type</typeparam>
        /// <param name="img">Image to work with</param>
        /// <param name="brightness">Brightness value</param>
        /// <param name="contrast">Contrast value</param>
        /// <param name="intens">Sub Image intensity</param>
        // Testing: OK
        public Image<T, byte> WaterColor<T>(Image<T, byte> img, double brightness, double contrast, double intens) where T : struct, IColor
        {
            Image<T, byte> result = new Image<T, byte>(img.Size);

            //Add Median Blur effect

            result = Brightness(img, brightness);
            result = Contrast(result, contrast);
            result = BooleanOperation(result, Boolean.Add, intens);

            return result;
        }

        private byte SetOperaton(Boolean b, double val, double subval)
        {
            if (b == Boolean.Add)
            {
                return ColorCheck(val + subval, 0, 255);
            }
            else if (b == Boolean.Substract)
            {
                return ColorCheck(val - subval, 0, 255);
            }
            throw new Exception("Wrong operation");
        }
    }
}
