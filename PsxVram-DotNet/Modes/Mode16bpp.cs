using System.Drawing.Imaging;

namespace PsxVram_DotNet.Modes;

internal class Mode16Bpp : Mode
{

    private readonly Bitmap _transparentBitmap;

    public Bitmap GetBitmap()
    {
        return Bitmap;
    }


    public Mode16Bpp(byte[] sourceBytes)
    {
        var argbBytes = ConvertDumpToArgb(sourceBytes);
        Bitmap = CreateBitmapFromBytes(argbBytes, 1, PixelFormat.Format16bppRgb555);
        _transparentBitmap = CreateBitmapFromBytes(argbBytes, 1, PixelFormat.Format16bppArgb1555);
    }



    private static byte[] ConvertDumpToArgb(byte[] dumpBytes)
    {
        var bytesSize = dumpBytes.Length;
        var wordsSize = bytesSize / 2;
        var pixelWords = new ushort[wordsSize];
        Buffer.BlockCopy(dumpBytes, 0, pixelWords, 0, bytesSize);
        for (var i = 0; i < wordsSize; i++)
        {
            var pixelWord = pixelWords[i];
            var r = pixelWord & 0x1F;
            var g = (pixelWord & 0x3E0) >> 5;
            var b = (pixelWord & 0x7C00) >> 10;
            var t = (pixelWord >> 15) ^ 1;
            pixelWords[i] = (ushort)(b | (g << 5) | (r << 10) | (t << 15));
        }

        var resultBytes = new byte[bytesSize];
        Buffer.BlockCopy(pixelWords, 0, resultBytes, 0, bytesSize);
        return resultBytes;
    }

    public Color[] GetClutColors(Rectangle clutRectangle)
    {
        var clutColors = new Color[clutRectangle.Width];
        for (var i = 0; i < clutRectangle.Width; i++)
        {
            clutColors[i] = Bitmap.GetPixel(clutRectangle.X + i, clutRectangle.Y);
        }

        return clutColors;
    }

    public override Bitmap GetTrimmedBitmap(TrimConfiguration trimConfiguration)
    {
        return trimConfiguration.IsTransparent
            ? _transparentBitmap.Clone(trimConfiguration.Rectangle, _transparentBitmap.PixelFormat)
            : Bitmap.Clone(trimConfiguration.Rectangle, Bitmap.PixelFormat);
    }
}