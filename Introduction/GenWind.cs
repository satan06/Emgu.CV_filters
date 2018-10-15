using Emgu.CV.UI;
using System;
using System.Windows.Forms;
using static Introduction.Data;

namespace Introduction
{
    public partial class Filter : Form
    {
        private ImageFilter filter = new ImageFilter();
        private ImageTransform transform = new ImageTransform();
        private string filterParam = "File Image (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

        public int ImageWidth { get; set; } = 640;
        public int ImageHeight { get; set; } = 480;

        public int WindowPanelModeWidth { get; set; } = 840;
        public int WindowRelaxModeWidth { get; set; } = 690;

        private int BooleansIntens { get; set; } = 5;
        private int BlurIntensity { get; set; } = 5;

        public Filter()
        {
            InitializeComponent();

            Width = WindowRelaxModeWidth;
            FormBorderStyle = FormBorderStyle.FixedSingle;


        }

        private void LoaderCheck(string fileName, bool isSource)
        {
            if (isSource)
            {
                filter.OpenFile(fileName, ref sourceImage, ImageWidth, ImageHeight);
            }
            else
            {
                filter.OpenFile(fileName, ref tempImage, ImageWidth, ImageHeight);
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
                imageBox.Image = imageBoxRs.Image = sourceImage;
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
            imageBoxRs.Image = filter.HSVFilter(HueTrackbar.Value, HSV.Hue);
        }

        private void SaturScroll(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.HSVFilter(SaturTrackbar.Value, HSV.Saturation);
        }

        private void ValueScroll(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.HSVFilter(ValueTrackbar.Value, HSV.Value);
        }

        private void BrightScroll(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.Brightness(sourceImage, BrTrackbar.Value);
        }

        private void ContrScroll(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.Contrast(sourceImage, ContrTrackbar.Value);
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
            imageBoxRs.Image = filter.WindowFilter(Sharp);
        }

        private void WinFilterEmbos(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.WindowFilter(Embos);
        }

        private void WinFilterEdges(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.WindowFilter(Edges);
        }

        private void WinFilerCustomMatSend(object sender, EventArgs e)
        {
            int[,] edTemp = { { (int)MatArg0.Value, (int)MatArg1.Value, (int)MatArg2.Value },
                              { (int)MatArg3.Value, (int)MatArg4.Value, (int)MatArg5.Value },
                              { (int)MatArg6.Value, (int)MatArg7.Value, (int)MatArg8.Value } };

            Custom = edTemp;

            if(Custom == null)
            {
                return;
            }
            imageBoxRs.Image = filter.WindowFilter(Custom);
        }

        private void CartnFilterClose(object sender, EventArgs e)
        {
            Width = WindowRelaxModeWidth;
            CartnFilterPanel.Visible = false;
        }

        private void CartnFilter(object sender, EventArgs e)
        {
            Width = WindowPanelModeWidth;
            CartnFilterPanel.Visible = true;
            imageBoxRs.Image = filter.CartoonFilter(sourceImage, (int)CartFilterThreshold.Value);
        }

        private void CartFilterThresholdChanged(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.CartoonFilter(sourceImage, (int)CartFilterThreshold.Value);
        }

        private void BlurFIlter(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.MedianBlur(sourceImage, BlurIntensity);
        }

        private void WaterColorFilter(object sender, EventArgs e)
        {
            Width = WindowPanelModeWidth;

            WaterColorMaskLoad.Text = "Load Image";
            WaterColorPanel.Visible = true;
            WaterColorBr.Enabled = false;
            WaterColorCtr.Enabled = false;
            WaterColorMask.Enabled = false;
        }

        private void WaterColorMaskScroll(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.WaterColor(sourceImage, (double)WaterColorBr.Value, 
                                                 (double)WaterColorCtr.Value, WaterColorMask.Value);
        }

        private void WaterColorCtrChanged(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.WaterColor(sourceImage, (double)WaterColorBr.Value,
                                     (double)WaterColorCtr.Value, WaterColorMask.Value);
        }

        private void WaterColorBrChanged(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.WaterColor(sourceImage, (double)WaterColorBr.Value,
                                     (double)WaterColorCtr.Value, WaterColorMask.Value);
        }

        private void WaterColorMaskLoadDown(object sender, EventArgs e)
        {
            LoadI(false);
            WaterColorMaskLoad.Text = "Change Image";
            WaterColorBr.Enabled = true;
            WaterColorCtr.Enabled = true;
            WaterColorMask.Enabled = true;
            imageBoxRs.Image = filter.WaterColor(sourceImage, (double)WaterColorBr.Value,
                                     (double)WaterColorCtr.Value, WaterColorMask.Value);
        }

        private void WaterColorClose(object sender, EventArgs e)
        {
            Width = WindowRelaxModeWidth;
            WaterColorPanel.Visible = false;
        }

        private void BooleansMaskLoadDown(object sender, EventArgs e)
        {
            LoadI(false);
            BooleansMaskLoad.Text = "Change Image";
            BooleansMask.Enabled = true;
            BooleansAddBut.Enabled = true;
            BooleansSubstrBut.Enabled = true;
        }

        private void BooleansFilterClose(object sender, EventArgs e)
        {
            Width = WindowRelaxModeWidth;
            BooleansPanel.Visible = false;
        }

        private void BooleansFilter(object sender, EventArgs e)
        {
            Width = WindowPanelModeWidth;

            BooleansMaskLoad.Text = "Load Image";
            BooleansPanel.Visible = true;
            BooleansMask.Enabled = false;
            BooleansAddBut.Enabled = false;
            BooleansSubstrBut.Enabled = false;
        }

        private void BooleansMaskScroll(object sender, EventArgs e)
        {
            BooleansIntens = BooleansMask.Value;
        }

        private void BooleansAddDown(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.BooleanOperation(sourceImage, Data.Boolean.Add, BooleansIntens);
        }

        private void BooleansSubstrDown(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.BooleanOperation(sourceImage, Data.Boolean.Substract, BooleansIntens);
        }

        private void IntersectionFilter(object sender, EventArgs e)
        {
            LoadI(false);
            imageBoxRs.Image = filter.Intersection(sourceImage);
        }

        private void SepiaFilter(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.Sepia();
        }

        private void BlackWhiteFilter(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.ConvertToBW(sourceImage);
        }

        private void ChannelSplitRed(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.ChannelSplit(BGR.Red);
        }

        private void ChannelSplitBlue(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.ChannelSplit(BGR.Blue);
        }

        private void ChannelSplitGreen(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.ChannelSplit(BGR.Green);
        }

        /// <summary>
        /// Temporary event to test new Transform functional
        /// </summary>
        private void TestEvent(object sender, EventArgs e)
        {
            // Test functional here
            imageBoxRs.Image = transform.Shear(ShiftType.Vertical, 0.25f);
        }
    }
}
