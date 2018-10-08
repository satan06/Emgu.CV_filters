using Emgu.CV;
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
        /// /// <param name="value">Intensity value.</param>
        // Testing: OK
        public Image<Bgr, byte> Contrast(double value = 5.0)
        {
            Image<Bgr, byte> destImage = new Image<Bgr, byte>(sourceImage.Size);
            double pixel;

            EachPixel((channel, width, height, color) =>
            {
                pixel = sourceImage.Data[width, height, channel] * value;
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
        /// /// <param name="value">Intensity value.</param>
        // Testing: OK
        public Image<Bgr, byte> Brightness(double value = 25.0)
        {
            Image<Bgr, byte> destImage = new Image<Bgr, byte>(sourceImage.Size);
            double pixel;

            EachPixel((channel, width, height, color) =>
            {
                pixel = sourceImage.Data[width, height, channel] + value;
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
        /// <param name="ch">Addition or substraction operator</param>
        /// <param name="image">Second image for operation</param>
        public Image<Bgr, byte> BooleanOperation(char ch)
        {
            Image<Bgr, byte> result = new Image<Bgr, byte>(sourceImage.Size);

            EachPixel((channel, width, height, color) =>
            {
                color = SetOperaton(ch, sourceImage.Data[width, height, channel]* 0.7, 
                        tempImage.Data[width, height, channel]* 0.7);
                result.Data[width, height, channel] = color;
            });

            return result;
        }

        /// <summary>
        /// Performs intersection operation with another image
        /// </summary>
        /// <param name="image">Second image for operation</param>
        public Image<Bgr, byte> Intersection(Image<Bgr, byte> image)
        {
            Image<Bgr, byte> result = new Image<Bgr, byte>(sourceImage.Size);

            EachPixel((channel, width, height, color) =>
            {
                if (image.Data[width, height, channel] == 0)
                {
                    result.Data[width, height, channel] = 0;
                }
            });

            return result;
        }

        private byte SetOperaton(char ch, double val, double subval)
        {
            if (ch == '+')
            {
                return ColorCheck(val + subval, 0, 255);
            }
            else if (ch == '-')
            {
                return ColorCheck(val - subval, 0, 255);
            }
            throw new Exception("Wrong operation");
        }
    }
}
