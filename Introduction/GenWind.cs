using Emgu.CV.UI;
using System;
using System.Windows.Forms;

namespace Introduction
{
    public partial class Filter : Form
    {
        private ImageFilter filter = new ImageFilter();
        private string filterParam = "File Image (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

        public int width = 640;
        public int height = 480;

        public int windowPanelModeWidth = 840;
        public int windowRelaxModeWidth = 685;

        public Filter()
        {
            InitializeComponent();

            imageBox.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
            imageBoxRs.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
            Width = windowRelaxModeWidth;
        }

        private void LoaderCheck(string fileName, bool isSource)
        {
            if (isSource)
            {
                filter.OpenFile(fileName, ref filter.sourceImage, width, height);
            }
            else
            {
                filter.OpenFile(fileName, ref filter.tempImage, width, height);
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
            Width = windowPanelModeWidth;
            HSVPanel.Visible = true;
        }

        private void HSVClose(object sender, EventArgs e)
        {
            Width = windowRelaxModeWidth;
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
            Width = windowPanelModeWidth;
            BrContrPanel.Visible = true;
        }

        private void BrContrClose(object sender, EventArgs e)
        {
            Width = windowRelaxModeWidth;
            BrContrPanel.Visible = false;
        }
    }
}
