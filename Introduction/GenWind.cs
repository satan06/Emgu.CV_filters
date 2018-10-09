using Emgu.CV.UI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Introduction
{
    public partial class Filter : Form
    {
        private ImageFilter filter = new ImageFilter();
        private string filterParam = "File Image (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

        public int ImageWidth { get; set; } = 640;
        public int ImageHeight { get; set; } = 480;

        public int WindowPanelModeWidth { get; set; } = 840;
        public int WindowRelaxModeWidth { get; set; } = 690;

        public Filter()
        {
            InitializeComponent();
            Width = WindowRelaxModeWidth;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            imageBox.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
            imageBoxRs.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
        }

        private void LoaderCheck(string fileName, bool isSource)
        {
            if (isSource)
            {
                filter.OpenFile(fileName, ref filter.sourceImage, ImageWidth, ImageHeight);
            }
            else
            {
                filter.OpenFile(fileName, ref filter.tempImage, ImageWidth, ImageHeight);
            }
        }

        private void LoadI(bool isSource)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = filterParam
            };
            var result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                LoaderCheck(fileName, isSource);
                imageBox.Image = imageBoxRs.Image = filter.sourceImage;
            }
        }

        private void OpenImage(object sender, EventArgs e)
        {
            LoadI(true);
        }

        private void HSVFIlter(object sender, EventArgs e)
        {
            Width = WindowPanelModeWidth;
            HSVPanel.Visible = true;
        }

        private void HSVClose(object sender, EventArgs e)
        {
            Width = WindowRelaxModeWidth;
            HSVPanel.Visible = false;
        }

        private void HueScroll(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.HSVFilter(HueTrackbar.Value, Data.HSV.Hue);
        }

        private void SaturScroll(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.HSVFilter(SaturTrackbar.Value, Data.HSV.Saturation);
        }

        private void ValueScroll(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.HSVFilter(ValueTrackbar.Value, Data.HSV.Value);
        }

        private void BrightScroll(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.Brightness(filter.sourceImage, BrTrackbar.Value);
        }

        private void ContrScroll(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.Contrast(filter.sourceImage, ContrTrackbar.Value);
        }

        private void BrContrFilter(object sender, EventArgs e)
        {
            Width = WindowPanelModeWidth;
            BrContrPanel.Visible = true;
        }

        private void BrContrClose(object sender, EventArgs e)
        {
            Width = WindowRelaxModeWidth;
            BrContrPanel.Visible = false;
        }

        private void WinFilter(object sender, EventArgs e)
        {
            Width = WindowPanelModeWidth;
            WindowFilterPanel.Visible = true;
        }

        private void WinFilterClose(object sender, EventArgs e)
        {
            Width = WindowRelaxModeWidth;
            WindowFilterPanel.Visible = false;
        }

        private void WinFilterSharpen(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.WindowFilter(Data.Sharp);
        }

        private void WinFilterEmbos(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.WindowFilter(Data.Embos);
        }

        private void WinFilterEdges(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.WindowFilter(Data.Edges);
        }

        private void WinFilerCustomMatSend(object sender, EventArgs e)
        {
            int[,] edTemp = { { (int)MatArg0.Value, (int)MatArg1.Value, (int)MatArg2.Value },
                              { (int)MatArg3.Value, (int)MatArg4.Value, (int)MatArg5.Value },
                              { (int)MatArg6.Value, (int)MatArg7.Value, (int)MatArg8.Value } };

            Data.Custom = edTemp;

            if(Data.Custom == null)
            {
                return;
            }
            imageBoxRs.Image = filter.WindowFilter(Data.Custom);
        }
    }
}
