using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Introduction
{
    public partial class Brightness : Form
    {
        public double brValue = 0;

        public Brightness()
        {
            InitializeComponent();
        }

        private void BrTrackBarUI_Scroll(object sender, EventArgs e)
        {
            brValue = BrTrackBarUI.Value;
        }
    }
}
