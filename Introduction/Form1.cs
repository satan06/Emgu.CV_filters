using Emgu.CV.UI;
using System;
using System.Windows.Forms;

namespace Introduction
{
    public partial class Filter : Form
    {
        ImageFilter filter = new ImageFilter();

        public Filter()
        {
            InitializeComponent();

            imageBox.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
            imageBoxRs.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
        }
        private void OpenFileUI(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                openFileDialog.Filter = "Image files(*.jpg, *.jpeg, *.jpe, *.jfif, *.png) |*.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                filter.OpenFile(fileName);

                imageBox.Image = filter.sourceImage;
                imageBoxRs.Image = filter.sourceImage;
            }
        }
        private void BWToolUI(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.BWConvert();
        }
        #region ChannelsFilters
        private void RedChUI(object sender, EventArgs e)
        {
           imageBoxRs.Image = filter.Channel(2);
        }
        private void BlueChUI(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.Channel(0);
        }
        private void GreenChUI(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.Channel(1);
        }
        #endregion
        private void BrightToolUI(object sender, EventArgs e)
        {
            Brightness br = new Brightness
            {
                Owner = this
            };
            br.ShowDialog();
        }
        public void UpdateUI(int brValue)
        {
            imageBoxRs.Image = filter.Brightness(brValue);
        }
        private void SepiaUI(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.Sepia();
        }
        private void ContrastUI(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.Contrast(25);
        }
    }
}
