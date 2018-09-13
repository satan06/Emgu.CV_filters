﻿using Emgu.CV;
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
        public Image<Gray, byte> Split(byte channelIndex)
        {
            if(sourceImage == null) { return null; }
            var channel = sourceImage.Split()[channelIndex];

            return channel;
        }
        public Image<Gray, byte> ChannelCombine()
        {
            VectorOfMat vm = new VectorOfMat();
            Image<Gray, byte> destImage = null;

            for (byte ch = 0; ch < 3; ch++) { vm.Push(Split(ch)); }
            CvInvoke.Merge(vm, destImage);

            return destImage;
        }
        public Image<Gray, byte> ChannelCombine(List<Image<Gray, byte>> channels)
        {
            VectorOfMat vm = new VectorOfMat();
            Image<Gray, byte> destImage = null;
                
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
        public Image<Gray, byte> Sepia()
        {
            Image<Gray, byte> destImage = null;
            List<Image<Gray, byte>> sepChannels = new List<Image<Gray, byte>>();

            Image<Gray, byte> redSep = Split(0) * 0.393 + Split(1) * 0.769 + Split(2) * 0.189; ;
            Image<Gray, byte> greenSep = Split(0) * 0.343 + Split(1) * 0.686 + Split(2) * 0.168; ;
            Image<Gray, byte> blueSep = Split(0) * 0.272 + Split(1) * 0.534 + Split(2) * 0.131; ;

            sepChannels.Add(redSep); sepChannels.Add(greenSep); sepChannels.Add(blueSep);
            destImage = ChannelCombine();

            return destImage;
        }
        public Image<Hsv, byte> ChangeBrightness(double brightness)
        {
            Image<Hsv, byte> hsvImage = sourceImage.Convert<Hsv, byte>();
            Image<Gray, Byte>[] channels;
            Image<Gray, Byte> imgval;
            Gray color = new Gray(brightness);
            VectorOfMat vm = new VectorOfMat();
            Image<Hsv, byte> destImage = null;

            channels = hsvImage.Split();  //split into components
            channels[2].SetValue(color);
            imgval = channels[2];         //hsv, so channels[2] is value.

            for (byte ch = 0; ch < channels.Length; ch++) { vm.Push(channels[ch]); }
            CvInvoke.Merge(vm, destImage);

            return destImage;
        }
    }
}
