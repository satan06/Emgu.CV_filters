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

        private void SepiaToolUI(object sender, EventArgs e)
        {
            imageBoxRs.Image = filter.Sepia();
        }
    }
}
