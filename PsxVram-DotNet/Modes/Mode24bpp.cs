using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using PsxVram_DotNet.Forms;

namespace PsxVram_DotNet.Modes;

internal class Mode24Bpp : Mode
{
    private const int Max24BppWidth = 682; //(1024 - 1) * 2 / 3

    public Mode24Bpp(IReadOnlyList<byte> sourceBytes)
    {
        var rgbBytes = ConvertDumpToRgb(sourceBytes);

        Bitmap = new Bitmap(Max24BppWidth, MainForm.MaxHeight, PixelFormat.Format24bppRgb);
        var bitmapData = Bitmap.LockBits(new Rectangle(0, 0, Bitmap.Width, Bitmap.Height), ImageLockMode.WriteOnly,
            Bitmap.PixelFormat);
        Marshal.Copy(rgbBytes, 0, bitmapData.Scan0, rgbBytes.Length);
        Bitmap.UnlockBits(bitmapData);
    }

    private static byte[]
        ConvertDumpToRgb(IReadOnlyList<byte> sourceBytes) //Swap R and B bytes, as 24bgr is not supported
    {
        var resultBytes = new byte[sourceBytes.Count];
        for (var y = 0; y < MainForm.MaxHeight; y++)
        {
            for (var x = 0; x < Max24BppWidth; x++)
            {
                var pixelStartOffset = y * MainForm.MaxWidth * 2 + x * 3; //3 bytes per pixel
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

    public override Bitmap GetTrimmedBitmap(TrimConfiguration trimConfiguration)
    {
        var rectangle = trimConfiguration.Rectangle
            with { Width = trimConfiguration.Rectangle.Width * 2 / 3, X = trimConfiguration.Rectangle.X * 2 / 3 };

        return Bitmap.Clone(rectangle, Bitmap.PixelFormat);
    }
}