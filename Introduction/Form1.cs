using Emgu.CV.UI;
using System;
using System.Windows.Forms;

namespace Introduction
{
    public partial class Filter : Form
    {
        private ImageFilter filter = new ImageFilter();
        private string filterParam = "File Image (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

        public Filter()
        {
            InitializeComponent();

            imageBox.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
            imageBoxRs.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
        }

        private void LoaderCheck(string fileName, bool isSource)
        {
            int width = 640, height = 480; 

            if (isSource)
            {
                filter.OpenFile(fileName, ref filter.sourceImage);
                filter.ResizeImage(filter.sourceImage, width, height);
            }
            else
            {
                filter.OpenFile(fileName, ref filter.tempImage);
                filter.ResizeImage(filter.tempImage, width, height);

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

        private void TestEvent(object sender, EventArgs e)
        {
            // Test functional here
            //LoadI(false);
            imageBoxRs.Image = filter.Brightness();
        }

        private void OpenNewImage(object sender, EventArgs e)
        {
            LoadI(true);
        }
    }
}
