using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;

namespace Introduction
{
    public class ImageFilter
    {
        public Image<Bgr, byte> sourceImage;

        public enum HSV : byte
        {
            hue,
            saturation,
            value
        }

        private delegate void Func<T1, T2, T3, T4>(T1 channel, T2 width, T3 height, T4 color);

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

        public void OpenFile (string fileNmae)
        {
            sourceImage = new Image<Bgr, byte>(fileNmae).Resize(640, 480, Inter.Linear);
        }

        public Image<Gray, byte> ToGray()
        {
            if (sourceImage == null) { return null; }

            Image<Gray, byte> grayImage = sourceImage.Convert<Gray, byte>();
            return grayImage;
        }

        public Image<Bgr, byte> Denoise()
        {
            if (sourceImage == null) { return null; }

            var destImage = sourceImage.PyrDown().PyrUp();

            return destImage;
        }

        public Image<Gray, byte> CannyFilter(double threshold = 80.0, double thresholdLinking = 40.0)
        {
            if (sourceImage == null) { return null; }

            var cannyEdges = sourceImage.Canny(threshold, thresholdLinking);
            Denoise();

            return cannyEdges;
        }

        public Image<Bgr, byte> CellShading()
        {
            if (sourceImage == null) { return null; }

            var cannyEdgesBgr = CannyFilter().Convert<Bgr, byte>();
            var resultImage = sourceImage.Sub(cannyEdgesBgr);

            for (int channel = 0; channel < resultImage.NumberOfChannels; channel++)
            {
                for (int x = 0; x < resultImage.Width; x++)
                {
                    for (int y = 0; y < resultImage.Height; y++)
                    {
                        byte color = resultImage.Data[y, x, channel];

                        if (color <= 50) { color = 0; }
                        else if (color <= 100) { color = 25; }
                        else if (color <= 150) { color = 180; }
                        else if (color <= 200) { color = 210; }
                        else { color = 255; }

                        resultImage.Data[y, x, channel] = color;
                    }
                }
            }
                Denoise();
                return resultImage;
        }

        public Image<Gray, byte> Channel(byte channelIndex)
        {
            if(sourceImage == null) { return null; }
            var channel = sourceImage.Split()[channelIndex];

            return channel;
        }

        public Image<Bgr, byte> ChannelCombine(List<Image<Gray, byte>> channels)
        {
            VectorOfMat vm = new VectorOfMat();
            Image<Bgr, byte> destImage = new Image<Bgr, byte>(sourceImage.Size);
                
            for (byte ch = 0; ch < channels.Count; ch++) { vm.Push(channels[ch]); }
            CvInvoke.Merge(vm, destImage);
            
            return destImage;
        }

        public Image<Gray, byte> ConvertToBW()
        {
            if (sourceImage == null) { return null; }
            Image<Gray, byte> grayImage = new Image<Gray, byte>(sourceImage.Size);

            EachPixel((channel, width, height, color) => 
            {
                grayImage.Data[width, height, 0] = Convert.ToByte(0.299 * sourceImage.Data[width, height, 2] + 0.587 *
                sourceImage.Data[width, height, 1] + 0.114 * sourceImage.Data[width, height, 0]);
            });

            return grayImage;
        }

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

        public Image<Hsv, byte> Contrast(int value = 5)
        {
            Image<Hsv, byte> destImage = sourceImage.Convert<Hsv, byte>();

            EachPixel((channel, width, height, color) =>
            {
                color = destImage.Data[width, height, channel];
                color *= ColorCheck(value, 0, 255);
                destImage.Data[width, height, channel] = color;
            });

            return destImage;
        }

        private byte ColorCheck(double color, double min, double max)
        {
            if (color < min)
            {
                return (byte)min;
            }
            else if (color > max)
            {
                return (byte)max;
            }
            else
            {
                return (byte)color;
            }
        }

        public Image<Bgr, byte> Brightness(int value = 25)
        {
            Image<Bgr, byte> destImage = new Image<Bgr, byte>(sourceImage.Size);

            EachPixel((channel, width, height, color) =>
            {
                color = destImage.Data[width, height, channel];
                color += ColorCheck(value, 0, 255);                
                destImage.Data[width, height, channel] = color;
            });

            return destImage;
        }

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

        public Image<Bgr, byte> Operation(char ch, Image<Bgr, byte> image)
        {
            Image<Bgr, byte> result = new Image<Bgr, byte>(sourceImage.Size);

            EachPixel((channel, width, height, color) =>
            {
                color = SplitOperaton(ch, result.Data[width, height, channel], image.Data[width, height, channel]);
                result.Data[width, height, channel] = color;
            });

            return result;
        }

        private byte SplitOperaton(char ch, double val, double subval)
        {
            if (ch == '+')
            {
                return ColorCheck(val + subval, 0, 255);
            }
            else if (ch == '-')
            {
                return ColorCheck(val + subval, 0, 255);
            }
            else
            {
                return ColorCheck(val + subval, 0, 255);
            }
        }
    }
}
