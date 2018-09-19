using System;
using Emgu.CV.UI;
using System.Windows.Forms;

namespace Introduction
{
    public partial class Brightness : Form
    {
        public double brValue = 0;

        public Brightness()
        {
            InitializeComponent();
            BrTrackBarUI.Maximum = 20;
            BrTrackBarUI.Minimum = -20;
            BrTrackBarUI.SmallChange = 5;
        }

        public void BrTrackBarUI_Scroll(object sender, EventArgs e)
        {
            if (Owner is Filter parent)
            {
                brValue = BrTrackBarUI.Value;
                parent.UpdateUI(brValue);
            }
        }
    }
}
