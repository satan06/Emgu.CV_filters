using System;
using Emgu.CV;
using Emgu.CV.OCR;
using Emgu.CV.Structure;

namespace Introduction
{
    public class TextGrabber
    {
        private readonly Tesseract ocr;
        private string _text;
        private Tesseract.Character[] _words;

        public string Text => _text;
        public Tesseract.Character[] Words => _words;

        public TextGrabber(string path, Data.Language lang)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("String cattot be null or empty", nameof(path));
            }
            
            ocr = new Tesseract(path, lang.ToString().ToLower(), OcrEngineMode.Default);
        }

        public void GetText() => _text = ocr.GetUTF8Text();
        public void GetWords() => _words = ocr.GetCharacters();

        public void Detect(Image<Bgr, byte> roiImage)
        {
            ocr.SetImage(roiImage);
            ocr.Recognize();
        }
    }
}
