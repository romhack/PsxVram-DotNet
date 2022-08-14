using System.Drawing.Imaging;

namespace PsxVram_DotNet.Modes;

internal class Mode8Bpp : Mode
{
    private readonly Bitmap _bitmap;
    private readonly Color[] _invertedColors;
    private readonly Color[] _orderedColors = new Color[0x100];

    public Mode8Bpp(byte[] sourceBytes)
    {
        _bitmap = CreateBitmapFromBytes(sourceBytes, 2, PixelFormat.Format8bppIndexed);
        for (var i = 0; i <= 0xFF; i++)
        {
            _orderedColors[i] = Color.FromArgb(i, i, i);
        }

        _invertedColors = _orderedColors.Reverse().ToArray();
    }

    public Bitmap GetTrimmedBitmap(Rectangle mainRectangle, Color[]? clutColors = null, bool inverted = false)
    {
        Color[] currentColors;
        if (clutColors is not null) //CLUT mode
        {
            currentColors = clutColors;
        }
        else //Normal/Inverted mode
        {
            currentColors = inverted ? _invertedColors : _orderedColors;
        }

        var currentPalette = _bitmap.Palette;
        for (var i = 0; i < _bitmap.Palette.Entries.Length; i++)
        {
            currentPalette.Entries[i] = currentColors[i];
        }

        _bitmap.Palette = currentPalette;

        Rectangle.X = mainRectangle.X * 2;
        Rectangle.Y = mainRectangle.Y;
        Rectangle.Width = mainRectangle.Width * 2;
        Rectangle.Height = mainRectangle.Height;
        return _bitmap.Clone(Rectangle, _bitmap.PixelFormat);
    }
}