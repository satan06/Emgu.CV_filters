﻿using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;

namespace Introduction
{
    /// <summary>
    /// Represents class with simple image filter effects
    /// </summary>
    public class ImageFilter
    {
        public Data Data;

        public ImageFilter(Data data)
        {
            Data = data;
        }

        public ImageFilter() { }

        public delegate void Func<Targ0, Targ1, Targ2, Targ3>(Targ0 channel, Targ1 width, Targ2 height, Targ3 color);

        // Pixel image traversal
        private void EachPixel(Func<int, int, int, byte> action)
        {
            for (int channel = 0; channel < Data.SourceImage.NumberOfChannels; channel++)
            {
                for (int x = 0; x < Data.SourceImage.Width; x++)
                {
                    for (int y = 0; y < Data.SourceImage.Height; y++)
                    {
                        action(channel, y, x, Data.SourceImage.Data[y, x, channel]);
                    }
                }
            }
        }

        private Image<Gray, byte> ToGray()
        {
            if (Data.SourceImage == null)
            {
                return null;
            }

            Image<Gray, byte> grayImage = Data.SourceImage.Convert<Gray, byte>();
            return grayImage;
        }

        /// <summary>
        /// Reduces noises on the image
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
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
        public Image<Gray, byte> CannyFilter(double threshold = 80.0, double thresholdLinking = 40.0)
        {
            if (Data.SourceImage == null)
            {
                return null;
            }
            var cannyEdges = Data.SourceImage.Canny(threshold, thresholdLinking);

            return cannyEdges;
        }

        /// <summary>
        /// Imposes a cell shading effect on the image
        /// </summary>
        public Image<Bgr, byte> CellShading()
        {
            if (Data.SourceImage == null)
            {
                return null;
            }

            var cannyEdgesBgr = CannyFilter().Convert<Bgr, byte>();
            var resultImage = Data.SourceImage.Sub(cannyEdgesBgr);

            EachPixel((channel, width, height, color) =>
            {
                resultImage.Data[width, height, channel] = CellShadingCheck(resultImage.Data[width, height, channel]);
            });

            Denoise(resultImage);
            return resultImage;
        }

        public Image<Gray, byte> ChannelSplit(Data.BGR channel)
        {
            if(Data.SourceImage == null)
            {
                return null;
            }

            var ch = Data.SourceImage.Split()[(int)channel];
            return ch;
        }

        /// <summary>
        /// Combines image channels all together in one particular image
        /// </summary>
        /// <param name="channels">Array of image channels</param>
        public Image<Bgr, byte> ChannelCombine(List<Image<Gray, byte>> channels)
        {
            VectorOfMat vm = new VectorOfMat();
            Image<Bgr, byte> destImage = new Image<Bgr, byte>(Data.SourceImage.Size);
                
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
        public Image<Gray, byte> ConvertToBW(Image<Bgr, byte> img)
        {
            if (img == null)
            {
                return null;
            }
            Image<Gray, byte> grayImage = new Image<Gray, byte>(img.Size);

            EachPixel((channel, width, height, color) => 
            {
                grayImage.Data[width, height, 0] = Convert.ToByte(0.299 * img.Data[width, height, 2] + 
                                                                  0.587 * img.Data[width, height, 1] + 
                                                                  0.114 * img.Data[width, height, 0]);
            });

            return grayImage;
        }

        /// <summary>
        /// Imposes a sepia effect on the image
        /// </summary>
        public Image<Bgr, byte> Sepia() 
        {
            Image<Bgr, byte> destImage = new Image<Bgr, byte>(Data.SourceImage.Size);
            byte blue, green, red;

            EachPixel((channel, width, height, color) => 
            {
                blue = Data.SourceImage.Data[width, height, 0];
                green = Data.SourceImage.Data[width, height, 1];
                red = Data.SourceImage.Data[width, height, 2];

                destImage.Data[width, height, 0] = ColorCheck(red * 0.272 + green * 0.534 + blue * 0.131, 0, 255);
                destImage.Data[width, height, 1] = ColorCheck(red * 0.349 + green * 0.686 + blue * 0.168, 0, 255);
                destImage.Data[width, height, 2] = ColorCheck(red * 0.393 + green * 0.769 + blue * 0.189, 0, 255);
            });

            return destImage;
        }

        /// <summary>
        /// Changes constrast of the image
        /// </summary>
        /// <typeparam name="T">Image type</typeparam>
        /// <param name="image">Image to work with</param>
        /// /// <param name="value">Intensity value.</param>
        public Image<T, byte> Contrast<T>(Image<T, byte> img, double value = 5.0) where T : struct, IColor
        {
            Image<T, byte> destImage = new Image<T, byte>(Data.SourceImage.Size);
            double pixel;

            EachPixel((channel, width, height, color) =>
            {
                pixel = img.Data[width, height, channel] * value;
                destImage.Data[width, height, channel] = ColorCheck(pixel, 0, 255);
            });

            return destImage;
        }

        /// <summary>
        /// Changes brightness of the image
        /// </summary>
        /// <typeparam name="T">Image type</typeparam>
        /// <param name="image">Image to work with</param>
        /// /// <param name="value">Intensity value.</param>
        public Image<T, byte> Brightness<T>(Image<T, byte> img, double value = 25.0) where T : struct, IColor
        {
            Image<T, byte> destImage = new Image<T, byte>(Data.SourceImage.Size);
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
        public Image<Hsv, byte> HSVFilter(double value, Data.HSV hsv)
        {
            Image<Hsv, byte> destImage = Data.SourceImage.Convert<Hsv, byte>();

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
        /// <param name="value">Effect intensity</param>
        public Image<T, byte> BooleanOperation<T>(Image<T, byte> img, Data.Boolean b, int value) where T : struct, IColor
        {
            Image<T, byte> result = new Image<T, byte>(Data.SourceImage.Size);

            EachPixel((channel, width, height, color) =>
            {
                color = SetOperaton(b, img.Data[width, height, channel] * Math.Abs(Normalize(value) - 1),
                        Data.TempImage.Data[width, height, channel] * Normalize(value));
                result.Data[width, height, channel] = color;
            });

            return result;
        }

        /// <summary>
        /// Performs intersection operation with another image
        /// </summary>
        /// <typeparam name="T">Image type</typeparam>
        /// <param name="image">Image to work with</param>
        public Image<T, byte> Intersection<T>(Image<T, byte> img) where T : struct, IColor
        {
            Image<T, byte> result = new Image<T, byte>(img.Size);

            EachPixel((channel, width, height, color) =>
            {
                if (Data.TempImage.Data[width, height, channel] == 0)
                {
                    result.Data[width, height, channel] = 0;
                }
                else if(Data.TempImage.Data[width, height, channel] > 0)
                {
                    result.Data[width, height, channel] = Data.SourceImage.Data[width, height, channel];
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
        public Image<T, byte> WaterColor<T>(Image<T, byte> img, double brightness, double contrast, int intens) where T : struct, IColor
        {
            Image<T, byte> result = new Image<T, byte>(img.Size);

            result = MedianBlur(result);
            result = Brightness(img, brightness);
            result = Contrast(result, contrast);
            result = BooleanOperation(result, Data.Boolean.Add, intens);

            return result;
        }

        /// <summary>
        /// Performs median blur effect on the image
        /// </summary>
        /// <typeparam name="T">Image type</typeparam>
        /// <param name="img">Image to work with</param>
        /// /// <param name="intens">Blur intensity</param>
        public Image<T, byte> MedianBlur<T>(Image<T, byte> img, int intens = 3) where T : struct, IColor
        {
            List<byte> pixels = new List<byte>();
            int index = ThresholdNormalize(intens) / 2;
            int coreInd = ThresholdNormalize(intens) * index;

            for (byte channel = 0; channel < img.NumberOfChannels; channel++)
            {
                for (int x = index; x < img.Width - index; x++)
                {
                    for (int y = index; y < img.Height - index; y++)
                    {
                        for (int i = -(index); i <= index; i++)
                        {
                            for (int j = -(index); j <= index; j++)
                            {
                                pixels.Add(Data.SourceImage.Data[y + j, x + i, channel]);
                            }
                        }

                        pixels.Sort();
                        img.Data[y, x, channel] = pixels[coreInd];

                        pixels.Clear();
                    }
                }
            }

            return img;
        }

        /// <summary>
        /// Performs window filter effect
        /// </summary>
        /// <param name="matrix">Specified matrix</param>
        public Image<Bgr, byte> WindowFilter(int[,] matrix)
        {
            Image<Gray, byte> result = ConvertToBW(Data.SourceImage);
            Image<Gray, byte> temp = ConvertToBW(Data.SourceImage);
            double value = 0;

            if(matrix == null)
            {
                throw new Exception("Matrix is empty!");
            }
            for (byte channel = 0; channel < result.NumberOfChannels; channel++)
            {
                for (int x = 1; x < result.Width - 1; x++)
                {
                    for (int y = 1; y < result.Height - 1; y++)
                    {
                        for (sbyte i = -1; i < 2; i++)
                        {
                            for (sbyte j = -1; j < 2; j++)
                            {
                                value += temp.Data[y + j, x + i, channel] * matrix[i + 1, j + 1];
                            }
                        }

                        result.Data[y, x, channel] = ColorCheck(value, 0, 255);
                        value = 0;
                    }
                }
            }

            return result.Convert<Bgr, byte>();
        }

        /// <summary>
        /// Performs cartoon filter effect on the image
        /// </summary>
        /// <param name="img">Source image</param>
        /// <param name="thresholdValue">Threshold value</param>
        public Image<Bgr, byte> CartoonFilter(Image<Bgr, byte> img, int thresholdValue = 10)
        {
            int blurIntensity = 11;

            var bwImage= ConvertToBW(img);
            var blurImage = MedianBlur(bwImage, blurIntensity);
            var binImage = blurImage.ThresholdAdaptive(new Gray(255), AdaptiveThresholdType.MeanC, 
                                                       ThresholdType.Binary, ThresholdNormalize(thresholdValue), 
                                                       new Gray(0.03))
                                                       .Dilate(1);
            //data.TempImage = binImage.Convert<Bgr, byte>();
            var result = Intersection(Data.SourceImage);

            return result;
        }

        #region Additional methods

        public byte SetOperaton(Data.Boolean b, double val, double subval)
        {
            if (b == Data.Boolean.Add)
            {
                return ColorCheck(val + subval, 0, 255);
            }
            else if (b == Data.Boolean.Substract)
            {
                return ColorCheck(val - subval, 0, 255);
            }
            throw new Exception("Wrong operation");
        }

        public byte CellShadingCheck(byte color)
        {
            var condArg0 = (color <= 50) ? 0 :
                            (color <= 100) ? 25 :
                            (color <= 150) ? 180 :
                            (color <= 200) ? 210 : 255;

            return (byte)condArg0;
        }

        public byte ColorCheck(double color, double min, double max)
        {
            var condition = (color < min) ? min : (color > max) ? max : color;
            return (byte)condition;
        }

        public int ThresholdNormalize(int value)
        {
            return (value <= 1) ? 3 : (value % 2 != 1) ? ++value : value;
        }

        public float Normalize(int value)
        {
            return (value <= 1) ? 0.1f : (value >= 9) ? 0.9f : (value * 0.1f);
        }

        #endregion
    }
}
