using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction
{
    public class CaptureFrameEv : EventArgs
    {
        public Image<Bgr, byte> CurFrame { get; set; }
    }
}
