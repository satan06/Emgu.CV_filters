using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using static Introduction.Data;

namespace Introduction
{
    public class ImageStabilizator
    {
        private class CurrentFrameObject
        {
            public Image<Bgr, byte> Frame;
            public PointF[] Points;

            public CurrentFrameObject(Image<Bgr, byte> img, PointF[] points)
            {
                Frame = img;
                Points = points;
            }
        }

        private readonly Image<Bgr, byte> _image;
        private readonly Image<Bgr, byte> _twistedImage;
        private readonly GFTTDetector _detector = new GFTTDetector(40, 0.01, 5, 3, true);
        private readonly Brisk _descriptor = new Brisk();
        private MKeyPoint[] _keyPoints;
        private List<CurrentFrameObject> FrameObjects;
        private VectorOfVectorOfDMatch matches;
        private Mat mask;

        private MatPacker [] Packers;
        private MatPacker BasePacker => Packers[0];
        private MatPacker TwistedPacker => Packers[0];

        private PointF[] _srcPoints;
        private PointF[] _destPoints;
        private byte[] _status;
        private float[] _trackErrors;

        public byte[] Status => _status;
        public float[] TrackErrors => _trackErrors;

        public ImageStabilizator(Data data)
        {
            if(data.SourceImage != null && data.TempImage != null)
            {
                _image = data.SourceImage;
                _twistedImage = data.TempImage;
            }
        }

        public ImageStabilizator DetectPoints()
        {
            _keyPoints = _detector.Detect(_image.Convert<Gray, byte>().Mat);
            return this;
        }

        public Image<Bgr, byte> DrawPoints(CurrentFrame frame, int radius = 3, int thickness = 2)
        {
            var output = (frame == CurrentFrame.BaseImage) ? FrameObjects[0] : FrameObjects[1];

            if (_keyPoints != null)
            {
                foreach (PointF p in output.Points)
                {
                    CvInvoke.Circle(output.Frame, Point.Round(p), radius, 
                        new Bgr(Color.Blue).MCvScalar, thickness);
                }
            }
            return output.Frame;
        }

        public ImageStabilizator GetBasePointsOnTwisted(int size = 20, int level = 5)
        {
            _srcPoints = new PointF[_keyPoints.Length];

            for (int i = 0; i < _keyPoints.Length; i++)
            {
                _srcPoints[i] = _keyPoints[i].Point;
            }

            CvInvoke.CalcOpticalFlowPyrLK(
                _image.Convert<Gray, byte>().Mat,
                _twistedImage.Convert<Gray, byte>().Mat,
                _srcPoints,
                new Size(size, size),
                level,
                new MCvTermCriteria(20, 1),
                out _destPoints,
                out _status,
                out _trackErrors
                );

            FrameObjects = new List<CurrentFrameObject>(2)
            {
                new CurrentFrameObject(_image, _srcPoints),
                new CurrentFrameObject(_twistedImage, _destPoints)
            };

            return this;
        }

        private class MatPacker
        {
            public VectorOfKeyPoint KeyPoints = new VectorOfKeyPoint();
            public UMat ModelDescriptor = new UMat();
            public UMat OutputImage;

            public MatPacker(Image<Bgr, byte> image)
            {
                if (image == null)
                {
                    throw new ArgumentNullException(nameof(image));
                }

                OutputImage = image
                    .Convert<Gray, byte>()
                    .Mat
                    .GetUMat(AccessType.Read);
            }

            public MatPacker ComputePoints(GFTTDetector detector, Brisk descriptor)
            {
                detector.DetectRaw(OutputImage, KeyPoints);
                descriptor.Compute(OutputImage, KeyPoints, ModelDescriptor);

                return this;
            }
        }

        public ImageStabilizator DetectCharactPoints()
        {
            Packers = new MatPacker[2]
            {
                new MatPacker(_twistedImage).ComputePoints(_detector, _descriptor),
                new MatPacker(_image).ComputePoints(_detector, _descriptor)
            };

            GetMask(out matches, out mask);
            //Features2DToolbox.VoteForUniqueness(matches, 0.8, mask);

            Features2DToolbox.VoteForSizeAndOrientation(
                BasePacker.KeyPoints,
                TwistedPacker.KeyPoints,
                matches,
                mask,
                scaleIncrement: 1.5,
                rotationBins: 20);

            return this;
        }

        public Image<Bgr, byte> DrawMatches()
        {
            var res = new Image<Bgr, byte>(_image.Width + _twistedImage.Width, _image.Height);

            Features2DToolbox.DrawMatches(
                _twistedImage,
                BasePacker.KeyPoints,
                _image,
                TwistedPacker.KeyPoints,
                matches,
                res,
                new MCvScalar(255, 0, 0),
                new MCvScalar(255, 0, 0),
                mask
            );

            return res;
        }

        public Image<Bgr, byte> StabilizedImage
        {
            get
            {
                var destImage = new Image<Bgr, byte>(_image.Size);
                Mat homography = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(
                    Packers[0].KeyPoints,
                    Packers[1].KeyPoints,
                    matches,
                    mask,
                    2
                );

                CvInvoke.WarpPerspective(_image, destImage, homography, destImage.Size);

                return destImage;
            }
        }

        private void GetMask(out VectorOfVectorOfDMatch matches, out Mat mask)
        {
            matches = new VectorOfVectorOfDMatch();
            MatchFrames(new BFMatcher(DistanceType.L2), matches, Packers);

            mask = new Mat(matches.Size, 1, DepthType.Cv8U, 1);
            mask.SetTo(new MCvScalar(255));
        }

        private void MatchFrames(BFMatcher matcher, VectorOfVectorOfDMatch matches, MatPacker [] packers)
        {
            if(packers.IsFixedSize && packers.Length == 2)
            {
                matcher.Add(packers[0].ModelDescriptor);
                matcher.KnnMatch(packers[1].ModelDescriptor, matches, 2, null);
            }
        }
    }
}
