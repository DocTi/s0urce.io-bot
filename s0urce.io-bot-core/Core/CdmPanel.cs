using System;
using System.Drawing;
using System.Diagnostics;
using s0urce.io_bot_core.ImageProcessing;
using Emgu.CV;
using Emgu.CV.OCR;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace s0urce.io_bot_core.Core
{
    public class CdmPanel
    {
        private Process Process { get; set; }
        private Rectangle Location { get; set; }
        private Tesseract Tesseract { get; set; }

        public static CdmPanel AutoInit(Process process)
        {
            ScreenCapture.DisableAnimationWindow(process);

            return new CdmPanel
            {
                Process = process,
                Location = FindLocation(process),
                Tesseract = new Tesseract(string.Empty, "eng", OcrEngineMode.TesseractOnly)
            };
        }

        public string RecognizeText()
        {
            using (var screen = new Image<Bgr, byte>(ScreenCapture.Capture(Process)))
            {
                screen.ROI = Location;

                var textRoi = GetTextRoi(screen);

                var roi = new Mat(screen.Mat, textRoi).ToImage<Bgr, byte>().Resize(3, Inter.Area);
                var blur = new Mat();
                var binary = new Mat();

                CvInvoke.GaussianBlur(roi, blur, new Size(9, 9), 3, 3, BorderType.Default);
                var green = blur.ToImage<Bgr, byte>()[1];
                CvInvoke.Threshold(green, binary, 145, 255, ThresholdType.Binary);

                Tesseract.SetImage(binary);
                Tesseract.Recognize();

                return Tesseract.GetUTF8Text(); ;
            }
        }

        private Rectangle GetTextRoi(Image<Bgr, byte> source)
        {
            using (var image = source.Clone())
            {
                var chanels = new VectorOfMat();
                var conturs = new VectorOfVectorOfPoint();

                CvInvoke.Split(source, chanels);
                CvInvoke.InRange(chanels[0], new Image<Gray, byte>(1, 1, new Gray(160)), new Image<Gray, byte>(1, 1, new Gray(170)), chanels[0]);
                CvInvoke.InRange(chanels[1], new Image<Gray, byte>(1, 1, new Gray(185)), new Image<Gray, byte>(1, 1, new Gray(255)), chanels[1]);
                CvInvoke.InRange(chanels[2], new Image<Gray, byte>(1, 1, new Gray(80)), new Image<Gray, byte>(1, 1, new Gray(100)), chanels[2]);

                CvInvoke.BitwiseAnd(chanels[2], chanels[1], image);
                CvInvoke.BitwiseAnd(image, chanels[0], image);
                CvInvoke.FindContours(image, conturs, new Mat(), RetrType.External, ChainApproxMethod.ChainApproxNone);

                return CvInvoke.BoundingRectangle(conturs[0]);
            }

            throw new Exception("Not found text roi");
        }

        private static Rectangle FindLocation(Process process)
        {
            using (var screen = new Image<Gray, byte>(ScreenCapture.Capture(process)))
            {
                var k = 10000;
                var kh = 35;
                var binary = new Mat();
                var contours = new VectorOfVectorOfPoint();
                var fillContours = new Image<Gray, byte>(screen.Width, screen.Height, new Gray(0));

                CvInvoke.Threshold(screen, binary, 70, 255, ThresholdType.Binary);
                CvInvoke.FindContours(binary, contours, new Mat(), RetrType.External, ChainApproxMethod.ChainApproxNone);
                CvInvoke.FillPoly(fillContours, contours, new MCvScalar(255));
                CvInvoke.FindContours(fillContours, contours, new Mat(), RetrType.List, ChainApproxMethod.ChainApproxNone);

                var filtredContours = new VectorOfVectorOfPoint();
                for (int i = 0; i < contours.Size; i++)
                {
                    var area = CvInvoke.ContourArea(contours[i], false);
                    if (area > k)
                    {
                        filtredContours.Push(contours[i]);
                    }
                }

                var tesseract = new Tesseract(string.Empty, "eng", OcrEngineMode.TesseractOnly);
                for (int i = 0; i < filtredContours.Size; i++)
                {
                    var contourRoi = CvInvoke.BoundingRectangle(filtredContours[i]);
                    var roi = new Mat(screen.Mat, new Rectangle(contourRoi.Location, new Size(contourRoi.Width, screen.Height / kh)));

                    tesseract.SetImage(roi);
                    tesseract.Recognize();

                    var result = tesseract.GetUTF8Text();
                    if (result.ToLower().Contains("cdm"))
                    {
                        return contourRoi;
                    }
                }
            }

            throw new Exception("Not found \"cdm\" panel");
        }
    }
}
