using System;
using Emgu.CV.UI;
using System.Windows.Forms;

namespace Introduction
{
    public partial class Brightness : Form
    {
        public Brightness()
        {
            InitializeComponent();

            BrTrackBarUI.Minimum = -100;
            BrTrackBarUI.Maximum = 100;
            BrTrackBarUI.SmallChange = 10;
        }

        public void BrTrackBarUI_Scroll(object sender, EventArgs e)
        {
            if (Owner is Filter parent)
            {
                parent.UpdateUI(BrTrackBarUI.Value);
            }
        }
    }
}
