using System;
using Emgu.CV.UI;
using System.Windows.Forms;

namespace Introduction
{
    public partial class Brightness : Form
    {
        public double brValue = 0;
        Filter parent = null;
        
        public Brightness(Filter parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        public void BrTrackBarUI_Scroll(object sender, EventArgs e)
        {
            brValue = BrTrackBarUI.Value;
            parent.UpdateUI(brValue);
        }
    }
}
