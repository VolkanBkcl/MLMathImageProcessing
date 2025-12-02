using System;
using System.Drawing;

namespace MLMathImageProcessing
{
    public class DigitRecognitionService
    {
        public DigitRecognitionService()
        {
        }

        public string RecognizeDigit(Bitmap image)
        {
            if (image == null)
                return "Görüntü yüklenmedi";

            try
            {
                Bitmap resized = ResizeImage(image, 28, 28);
                Bitmap grayScale = ConvertToGrayscale(resized);
                float[] pixels = ExtractPixels(grayScale);
                int predictedDigit = PredictDigit(pixels);
                
                resized.Dispose();
                grayScale.Dispose();
                
                return predictedDigit.ToString();
            }
            catch (Exception ex)
            {
                return $"Hata: {ex.Message}";
            }
        }

        private Bitmap ResizeImage(Bitmap image, int width, int height)
        {
            Bitmap resized = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(resized))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, 0, 0, width, height);
            }
            return resized;
        }

        private Bitmap ConvertToGrayscale(Bitmap original)
        {
            Bitmap gray = new Bitmap(original.Width, original.Height);
            for (int i = 0; i < original.Width; i++)
            {
                for (int j = 0; j < original.Height; j++)
                {
                    Color originalColor = original.GetPixel(i, j);
                    int grayScale = (int)((originalColor.R * 0.3) + (originalColor.G * 0.59) + (originalColor.B * 0.11));
                    gray.SetPixel(i, j, Color.FromArgb(grayScale, grayScale, grayScale));
                }
            }
            return gray;
        }

        private float[] ExtractPixels(Bitmap image)
        {
            float[] pixels = new float[28 * 28];
            int index = 0;
            for (int y = 0; y < 28; y++)
            {
                for (int x = 0; x < 28; x++)
                {
                    Color pixel = image.GetPixel(x, y);
                    pixels[index++] = (255 - pixel.R) / 255.0f;
                }
            }
            return pixels;
        }

        private int PredictDigit(float[] pixels)
        {
            float centerDensity = 0;
            int centerStart = 10 * 28 + 10;
            int centerEnd = 18 * 28 + 18;
            
            for (int i = centerStart; i < centerEnd; i++)
            {
                centerDensity += pixels[i];
            }
            centerDensity /= (8 * 8);
            
            float edgeDensity = 0;
            int edgeCount = 0;
            
            for (int i = 0; i < 28; i++)
            {
                edgeDensity += pixels[i];
                edgeCount++;
            }
            for (int i = 27 * 28; i < 28 * 28; i++)
            {
                edgeDensity += pixels[i];
                edgeCount++;
            }
            for (int i = 0; i < 28 * 28; i += 28)
            {
                edgeDensity += pixels[i];
                edgeCount++;
            }
            for (int i = 27; i < 28 * 28; i += 28)
            {
                edgeDensity += pixels[i];
                edgeCount++;
            }
            edgeDensity /= edgeCount;
            
            if (centerDensity < 0.3 && edgeDensity < 0.2)
                return 1;
            else if (centerDensity > 0.7)
                return 8;
            else if (centerDensity > 0.5 && edgeDensity > 0.4)
                return 0;
            else if (centerDensity > 0.5)
                return 6;
            else if (centerDensity > 0.4)
                return 9;
            else if (centerDensity > 0.3)
                return 3;
            else
                return 2;
        }

        public string RecognizeAllDigits(Bitmap image)
        {
            if (image == null)
                return "Görüntü yüklenmedi";

            try
            {
                string result = RecognizeDigit(image);
                return result;
            }
            catch (Exception ex)
            {
                return $"Hata: {ex.Message}";
            }
        }
    }
}

