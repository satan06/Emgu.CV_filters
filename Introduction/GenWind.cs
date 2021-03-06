﻿using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Windows.Forms;
using static Introduction.Data;

namespace Introduction
{
    public partial class Filter : Form
    {
        private CaptureCamera cam = new CaptureCamera();
        private ImageFilter filter;
        private Capture capture;
        private ImageTransform transform;
        private PointManager manager = new PointManager();
        public Data Data = new Data();

        private string filterParam = "File Image (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

        public int ImageWidth { get; set; } = 640;
        public int ImageHeight { get; set; } = 480;

        public int WindowPanelModeWidth { get; set; } = 840;
        public int WindowRelaxModeWidth { get; set; } = 690;

        private int BooleansIntens { get; set; } = 5;
        private int BlurIntensity { get; set; } = 5;

        public bool IsSetCenterModeActive { get; set; } = false;

        public bool IsHomographyActive { get; set; } = false;
        public CstPoint RotateAnchor;
        public bool IsCameraActive = false;

        public Filter()
        {
            InitializeComponent();

            Width = WindowRelaxModeWidth;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            RotateCenterPoint.Visible = false;
            HomographyApplyButton.Visible = false;

            RotateAnchor = CstPoint.Factory.NewCenterPoint(imageBox.Width, imageBox.Height);

            Console.WriteLine($"Width: {imageBoxRs.Width}, Height: {imageBoxRs.Height}");
        }

        private void PrepareHomogrButton()
        {
            HomographyApplyButton.Text = $"Set Points";
            HomographyApplyButton.BackColor = Color.Red;
            HomographyApplyButton.Enabled = false;
        }

        private void LoaderCheck(string fileName, bool isSource)
        {
            Data.OpenFile(fileName, isSource);
            manager.Points.Clear();
            PrepareHomogrButton();

            transform = new ImageTransform(Data);
            filter = new ImageFilter(Data);
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
                imageBox.Image = imageBoxRs.Image = Data.SourceImage;

                FilterMenuStrip.Enabled = true;
                TransformMenuStrip.Enabled = true;
                DetectionTool.Enabled = true;
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
            imageBoxRs.Image = filter.Brightness(Data.SourceImage, BrTrackbar.Value);
        }

        private void ContrScroll(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.Contrast(Data.SourceImage, ContrTrackbar.Value);
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
            imageBoxRs.Image = filter.CartoonFilter(Data.SourceImage, (int)CartFilterThreshold.Value);
        }

        private void CartFilterThresholdChanged(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.CartoonFilter(Data.SourceImage, (int)CartFilterThreshold.Value);
        }

        private void BlurFIlter(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.MedianBlur(Data.SourceImage, BlurIntensity);
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
            imageBoxRs.Image = filter.WaterColor(Data.SourceImage, (double)WaterColorBr.Value, 
                                                 (double)WaterColorCtr.Value, 
                                                 WaterColorMask.Value);
        }

        private void WaterColorCtrChanged(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.WaterColor(Data.SourceImage, (double)WaterColorBr.Value,
                                     (double)WaterColorCtr.Value, WaterColorMask.Value);
        }

        private void WaterColorBrChanged(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.WaterColor(Data.SourceImage, (double)WaterColorBr.Value,
                                     (double)WaterColorCtr.Value, WaterColorMask.Value);
        }

        private void WaterColorMaskLoadDown(object sender, EventArgs e)
        {
            LoadI(false);
            WaterColorMaskLoad.Text = "Change Image";
            WaterColorBr.Enabled = true;
            WaterColorCtr.Enabled = true;
            WaterColorMask.Enabled = true;
            imageBoxRs.Image = filter.WaterColor(Data.SourceImage, (double)WaterColorBr.Value,
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
            imageBoxRs.Image = filter.BooleanOperation(Data.SourceImage, Data.Boolean.Add, 
                                                                    BooleansIntens);
        }

        private void BooleansSubstrDown(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.BooleanOperation(Data.SourceImage, Data.Boolean.Substract, 
                                                                    BooleansIntens);
        }

        private void IntersectionFilter(object sender, EventArgs e)
        {
            LoadI(false);
            imageBoxRs.Image = filter.Intersection(Data.SourceImage);
        }

        private void SepiaFilter(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.Sepia();
        }

        private void BlackWhiteFilter(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.ConvertToBW(Data.SourceImage);
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

        private void SetDotEvent(object sender, MouseEventArgs e)
        {
            int x = (int)(e.Location.X / imageBox.ZoomScale);
            int y = (int)(e.Location.Y / imageBox.ZoomScale);

            if (IsHomographyActive && !manager.IsFull)
            {
                HomographyApplyButton.Enabled = false;

                manager.AddPoint(new PointF(x, y));
                transform.DrawPoint(new Point(x, y), 1, 1);
                imageBox.Image = Data.SourceImage;

                if(manager.IsFull)
                {
                    HomographyApplyButton.Text = $"Apply";
                    HomographyApplyButton.BackColor = Color.Aquamarine;
                    HomographyApplyButton.Enabled = true;
                }
            }

            if (IsSetCenterModeActive)
            {
                RotateCenterPoint.Location = new Point(x + 10, y + 25); // hardcode, need fix here
                RotateAnchor = CstPoint.Factory.NewFreePoint(x, y);
            }
        }

        private void ScaleTransform(object sender, EventArgs e)
        {
            Width = WindowPanelModeWidth;
            ScalePanel.Visible = true;
        }

        private void ScalePanelClose(object sender, EventArgs e)
        {
            Width = WindowRelaxModeWidth;
            ScalePanel.Visible = false;
        }

        private void ScaleTransformApply(object sender, EventArgs e)
        {
            if(ScaleAmountX.Value != 0 && ScaleAmountX.Value != 0)
            {
                imageBoxRs.Image = transform.Scale((float)ScaleAmountX.Value, 
                                                   (float)ScaleAmountY.Value);
            }
        }

        private void ReflHorizTransform(object sender, EventArgs e)
        {
            imageBoxRs.Image = transform.Reflect(ReflType.Horizontal);
        }

        private void ReflVertTransform(object sender, EventArgs e)
        {
            imageBoxRs.Image = transform.Reflect(ReflType.Vertical);
        }

        private void ReflDiagTransform(object sender, EventArgs e)
        {
            imageBoxRs.Image = transform.Reflect(ReflType.Diagonal);
        }

        private void ShearTransform(object sender, EventArgs e)
        {
            Width = WindowPanelModeWidth;
            ShearPanel.Visible = true;
        }

        private void ShearTransformClose(object sender, EventArgs e)
        {
            Width = WindowRelaxModeWidth;
            ShearPanel.Visible = false;
        }

        private void ShearHorizApply(object sender, EventArgs e)
        {
            if (ShearAmount.Value != 0)
            {
                imageBoxRs.Image = transform.Shear(ShiftType.Horizontal,
                                                  (float)ShearAmount.Value);
            }
        }

        private void ShearVertApply(object sender, EventArgs e)
        {
            if (ShearAmount.Value != 0)
            {
                imageBoxRs.Image = transform.Shear(ShiftType.Vertical,
                                                  (float)ShearAmount.Value);
            }
        }

        private void RotateTransform(object sender, EventArgs e)
        {
            Width = WindowPanelModeWidth;
            RotatePanel.Visible = true;
        }

        private void RotateTransformApply(object sender, EventArgs e)
        {
            imageBoxRs.Image = transform.Rotate(RotateAnchor, double.Parse(RotateAngleInput.Text));
        }

        private void RotatePanelClose(object sender, EventArgs e)
        {
            Width = WindowRelaxModeWidth;
            RotatePanel.Visible = false;
        }

        private void RotateTransformSetCenter(object sender, EventArgs e)
        {
            if(!IsSetCenterModeActive)
            {
                RotateSetCenterButton.BackColor = Color.Red;
                IsSetCenterModeActive = !IsSetCenterModeActive;
                RotateTransformApplyButton.Enabled = false;
                RotateCenterPoint.Visible = true;
            }
            else
            {
                RotateSetCenterButton.BackColor = Color.PaleTurquoise;
                IsSetCenterModeActive = !IsSetCenterModeActive;
                RotateTransformApplyButton.Enabled = true;
                RotateCenterPoint.Visible = false;
            }
        }

        private void HomographTransform(object sender, EventArgs e)
        {
            PrepareHomogrButton();
            manager.Points.Clear();

            IsHomographyActive = true;

            HomographyApplyButton.Visible = true;
            HomographyApplyButton.Enabled = false;
        }

        private void HomogrApply(object sender, EventArgs e)
        {
            imageBoxRs.Image = transform.Homograph(
                transform.CoordSort(
                    manager.Points.ToArray()
                    )
                );
        }

        /// <summary>
        /// Temporary event to test new Transform functional
        /// </summary>
        private void TestEvent(object sender, EventArgs e)
        {
        }

        private void DetTrig(object sender, EventArgs e)
        {
            ThrsDetValue.Value = 80;
            MinAreaDetValue.Value = 256;

            Detector det = new Detector(Data)
                .GaussianBlur()
                .GetInterestStandart((double)ThrsDetValue.Value, (double)MinAreaDetValue.Value)
                .DetectContours()
                .Approx();

            DetectionAssembler.Triangle assembler = new DetectionAssembler.Triangle(det);
            imageBoxRs.Image = assembler.Detect(Data);

            MessageBox.Show($"Triangles detected: {assembler.PrimitiveCount}");
        }

        private void DetRect(object sender, EventArgs e)
        {
            ThrsDetValue.Value = 120;
            MinAreaDetValue.Value = 99;

            Detector det = new Detector(Data)
                .GaussianBlur()
                .GetInterestStandart((double)ThrsDetValue.Value, (double)MinAreaDetValue.Value)
                .DetectContours()
                .Approx();

            DetectionAssembler.Rectangle assembler = new DetectionAssembler.Rectangle(det);
            imageBoxRs.Image = assembler.Detect(Data);

            MessageBox.Show($"Rectangles detected: {assembler.PrimitiveCount}");
        }

        private void DetCircl(object sender, EventArgs e)
        {
            Detector det = new Detector(Data)
                .GaussianBlur()
                .DetectContours(minDistance: 250, acTreshold: 36, minRadius: 50, maxRadius: 150);
    

            DetectionAssembler.Circle assembler = new DetectionAssembler.Circle(det);
            imageBoxRs.Image = assembler.Detect(Data);

            MessageBox.Show($"Circles detected: {assembler.PrimitiveCount}");
        }

        private void DetByColor(object sender, EventArgs e)
        {
            Detector det = new Detector(Data)
                .GaussianBlur()
                .GetInterestByColor(color: 30)
                .RemoveArtefacts(iterations: 4)
                .DetectContours()
                .Approx();

            DetectionAssembler.Unfiltered assembler = new DetectionAssembler.Unfiltered(det);
            imageBoxRs.Image = assembler.Detect(Data);

            MessageBox.Show($"Primitives detected: {assembler.PrimitiveCount}");
        }

        private void DrawSelectedCaption(object sender, EventArgs e)
        {
            var item = (Image<Bgr, byte>)CaptionsList.SelectedItem;
            imageBoxRs.Image = item;

            string path = "C:\\githb\\tessdata";

            TextGrabber grabber = new TextGrabber(path, Language.ENG);
            grabber.Detect(item);
            grabber.GetText();

            MessageBox.Show(grabber.Text);
        }

        private void StartFaceDet(object sender, EventArgs e)
        {
            IsCameraActive = !IsCameraActive;

            if (!IsCameraActive)
            {
                cam.Stop();
            }
            else
            {
                cam.GrabCamera();
                cam.FrameGrabbed += Cam_ImageGrabbed;
            }
        }

        private void Cam_ImageGrabbed(object sender, CaptureFrameEv e)
        {
            imageBoxRs.Image = new FaceGrabber(e.CurFrame).GetFrontal().DrawFaces();
        }

        private void StartTextDet(object sender, EventArgs e)
        {
            cam.GrabCamera();
            cam.FrameGrabbed += Cam_TextOnImgGrab;
        }

        private void Cam_TextOnImgGrab(object sender, CaptureFrameEv e)
        {
            capture = new Capture(e.CurFrame)
                .Binary()
                .Impact(iterations: 5)
                .GetContours();

            imageBoxRs.Image = capture.DrawContours();
        }

        private void DetFacesImg(object sender, EventArgs e)
        {
            imageBoxRs.Image = new FaceGrabber(Data).GetFrontal().DrawFaces();
        }

        private void DetTextImg(object sender, EventArgs e)
        {
            CompleteDisplayDetText.Visible = true;
            CaptionsList.Visible = true;
            ClearCaptElems.Visible = true;

            capture = new Capture(Data)
                .Binary()
                .Impact(iterations: 5)
                .GetContours()
                .GetCaptures();

            imageBoxRs.Image = capture.DrawContours();
            CaptionsList.DataSource = capture.Captions;

            MessageBox.Show($"Captions detected: {capture.Rects.Count}");
        }

        private void GetTextDispayComplete(object sender, EventArgs e)
        {
            imageBoxRs.Image = capture.DrawContours();
        }

        private void DeleteElems(object sender, EventArgs e)
        {
            CaptionsList.DataSource = new string[1] { string.Empty };
            capture.Captions.Clear();
        }

        private void CaptureObjectsFromFile(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "MP4|*.mp4"

            };
            var result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;

                CaptureCamera capture = new CaptureCamera();
                capture.GrabCameraSimple(fileName);
                capture.FrameGrabbed += CaptureVideo;
            }
        }

        private void CaptureVideo(object sender, CaptureFrameEv e)
        {
            imageBox.Image = e.CurFrame;
            imageBoxRs.Image = new Capture(e.CurFrame, e.CurFrameGray)
                .FindContoursOnRetrivedFrame()
                .DrawContours();
        }

        private void GetStabPoints(object sender, EventArgs e)
        {
            LoadI(true);
            LoadI(false);
            ImageStabilizator stabilizator = new ImageStabilizator(Data)
                .DetectPoints()
                .GetBasePointsOnTwisted();

            imageBox.Image = stabilizator.DrawPoints(frame: CurrentFrame.BaseImage);
            imageBoxRs.Image = stabilizator.DrawPoints(CurrentFrame.TwistedImage);
        }

        private void DrawStabMatches(object sender, EventArgs e)
        {
            LoadI(true);
            LoadI(false);
            imageBoxRs.Image = new ImageStabilizator(Data)
                .DetectCharactPoints()
                .DrawMatches();
        }

        private void StabilizeFrame(object sender, EventArgs e)
        {
            LoadI(true);
            LoadI(false);
            imageBoxRs.Image = new ImageStabilizator(Data)
                .DetectCharactPoints()
                .StabilizedImage;
        }
    }
}
