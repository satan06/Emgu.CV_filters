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
        #region CannyThreshold
        public double cannyThreshold = 80.0;
        public double cannyThresholdLinking = 40.0;
        #endregion

        public enum HSV : byte
        {
            hue,
            saturation,
            value
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

            var tempImage = sourceImage.PyrDown();
            var destImage = tempImage.PyrUp();

            return destImage;
        }
        public Image<Gray, byte> CannyFilter()
        {
            if (sourceImage == null) { return null; }

            var cannyEdges = sourceImage.Canny(cannyThreshold, cannyThresholdLinking);
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
        public Image<Gray, byte> BWConvert()
        {
            if (sourceImage == null) { return null; }
            Image<Gray, byte> grayImage = new Image<Gray, byte>(sourceImage.Size);

            for (int x = 0; x < grayImage.Width; x++)
            {
                for (int y = 0; y < grayImage.Height; y++)
                {
                    grayImage.Data[y, x, 0] = Convert.ToByte(0.299 * sourceImage.Data[y, x, 2] + 0.587 *
                    sourceImage.Data[y, x, 1] + 0.114 * sourceImage.Data[y, x, 0]);
                }
            }

            return grayImage;
        }
        public Image<Bgr, byte> Sepia()
        {
            Image<Bgr, byte> destImage = sourceImage.Clone();
            byte blue, green, red;

            for (int x = 0; x < destImage.Width; x++)
            {
                for (int y = 0; y < destImage.Height; y++)
                {
                    blue = destImage.Data[y, x, 0];
                    green = destImage.Data[y, x, 1];
                    red = destImage.Data[y, x, 2];

                    destImage.Data[y, x, 0] = ColorCheck(blue * 0.272 + green * 0.534 + blue * 0.131, 0 , 255);
                    destImage.Data[y, x, 1] = ColorCheck(blue * 0.349 + green * 0.686 + blue * 0.168, 0, 255);
                    destImage.Data[y, x, 2] = ColorCheck(blue * 0.393 + green * 0.769 + blue * 0.189, 0, 255);
                }
            }

            return destImage;
        }
        public Image<Hsv, byte> BrightnessHSV(int brightness = 25)
        {
            Image<Hsv, byte> destImage = sourceImage.Convert<Hsv, byte>();

            for (int x = 0; x < destImage.Width; x++)
            {
                for (int y = 0; y < destImage.Height; y++)
                {
                    int color = destImage.Data[y, x, 2];
                    color += brightness;
                    destImage.Data[y, x, 2] = ColorCheck(color, 0, 100);
                }
            }
            return destImage;
        }
        public Image<Hsv, byte> Contrast(int contrast = 5)
        {
            Image<Hsv, byte> destImage = sourceImage.Convert<Hsv, byte>();

            for (int x = 0; x < destImage.Width; x++)
            {
                for (int y = 0; y < destImage.Height; y++)
                {
                    int color = destImage.Data[y, x, 2];
                    color *= contrast;
                    destImage.Data[y, x, 1] = ColorCheck(color, 0, 255);
                }
            }
            return destImage;
        }
        private byte ColorCheck(double color, double min, double max)
        {
            if (color < min) { return (byte)min; }
            else if (color > max) { return (byte)max; }
            else { return (byte)color; }
        }
        public Image<Bgr, byte> Brightness(int brightness = 25)
        {
            Image<Bgr, byte> destImage = sourceImage.Clone();

            for (int channel = 0; channel < destImage.NumberOfChannels; channel++)
            {
                for (int x = 0; x < destImage.Width; x++)
                {
                    for (int y = 0; y < destImage.Height; y++)
                    {
                        int color = destImage.Data[y, x, channel];
                        color += brightness;
                        destImage.Data[y, x, channel] = ColorCheck(color, 0, 255);
                    }
                }
            }
            return destImage;
        }
        public Image<Hsv, byte> HSVFilter(double value, HSV channel)
        {
            Image<Hsv, byte> destImage = sourceImage.Convert<Hsv, byte>();

            for (int x = 0; x < destImage.Width; x++)
            {
                for (int y = 0; y < destImage.Height; y++)
                {
                    double color = destImage.Data[y, x, (int)channel] + value;  

                    if (channel == 0)
                        destImage.Data[y, x, (int)channel] = ColorCheck(color, 0, 180);
                    else
                        destImage.Data[y, x, (int)channel] = ColorCheck(color, 0, 100);

                }
            }
            return destImage;
        }
        public Image<Bgr, byte> Addition(Image<Bgr, byte> image)
        {
            Image<Bgr, byte> result = sourceImage.Clone();
            int color;

            for (int channel = 0; channel < result.NumberOfChannels; channel++)
            {
                for (int x = 0; x < result.Width; x++)
                {
                    for (int y = 0; y < result.Height; y++)
                    {
                        color = result.Data[y, x, channel] + image.Data[y, x, channel];
                        result.Data[y, x, channel] = ColorCheck(color, 0, 255);
                    }
                }
            }
            return result;
        }
        private byte SplitOperaton(char ch, double val, double subval)
        {
            if (ch == '+') { return (byte)(val + subval); }
            else if (ch == '-') { return (byte)(val - subval); }
            else { return (byte)(val * subval); }
        }
    }
}
