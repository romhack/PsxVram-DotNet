using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace PsxVram_DotNet.Modes
{
    internal abstract class Mode
    {
        protected Bitmap _bitmap;
        protected Rectangle _rectangle;

        protected static Bitmap CreateBitmapFromBytes(byte[] sourceBytes, int widthScale, PixelFormat pixelFormat)
        {
            var bitmap = new Bitmap(MainForm.MaxWidth * widthScale, MainForm.MaxHeight, pixelFormat);
            var bitmapData = bitmap.LockBits(new Rectangle(0, 0, MainForm.MaxWidth * widthScale, MainForm.MaxHeight), ImageLockMode.WriteOnly, pixelFormat);
            Marshal.Copy(sourceBytes, 0, bitmapData.Scan0, sourceBytes.Length);
            bitmap.UnlockBits(bitmapData);
            return bitmap;
        }
    }
}