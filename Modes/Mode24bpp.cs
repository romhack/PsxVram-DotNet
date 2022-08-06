using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace PsxVram_DotNet.Modes
{
    internal class Mode24bpp : Mode
    {
        private const int Max24bppWidth = 682;//(1024 - 1) * 2 / 3

        private static byte[] ConvertDumpToRgb(byte[] sourceBytes)//Swap R and B bytes, as 24bgr is not supported
        {
            var resultBytes = new byte[sourceBytes.Length];
            for (var y = 0; y < MainForm.MaxHeight; y++)
            {
                for (var x = 0; x < Max24bppWidth; x++)
                {
                    var pixelStartOffset = (y * MainForm.MaxWidth * 2) + x * 3;//3 bytes per pixel
                    var b = sourceBytes[pixelStartOffset];
                    var g = sourceBytes[pixelStartOffset + 1];
                    var r = sourceBytes[pixelStartOffset + 2];
                    resultBytes[pixelStartOffset] = r;
                    resultBytes[pixelStartOffset + 1] = g;
                    resultBytes[pixelStartOffset + 2] = b;
                }
            }

            return resultBytes;
        }

        public Bitmap GetTrimmedBitmap(Rectangle mainRectangle)
        {
            _rectangle = mainRectangle;
            _rectangle.X = mainRectangle.X * 2 / 3;
            _rectangle.Width = mainRectangle.Width * 2 / 3;
            return _bitmap.Clone(_rectangle, _bitmap.PixelFormat);
        }

        public Mode24bpp(byte[] sourceBytes)
        {
            var rgbBytes = ConvertDumpToRgb(sourceBytes);

            _bitmap = new Bitmap(Max24bppWidth, MainForm.MaxHeight, PixelFormat.Format24bppRgb);
            var bitmapData = _bitmap.LockBits(new Rectangle(0, 0, _bitmap.Width, _bitmap.Height), ImageLockMode.WriteOnly, _bitmap.PixelFormat);
            Marshal.Copy(rgbBytes, 0, bitmapData.Scan0, rgbBytes.Length);
            _bitmap.UnlockBits(bitmapData);
        }
    }
}