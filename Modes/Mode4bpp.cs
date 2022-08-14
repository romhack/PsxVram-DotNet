using System.Drawing.Imaging;

namespace PsxVram_DotNet.Modes;

internal class Mode4Bpp : Mode
{
    private readonly Bitmap _bitmap;
    private readonly Color[] _invertedColors;
    private readonly Color[] _orderedColors = new Color[0x10];

    public Mode4Bpp(IReadOnlyList<byte> sourceBytes)
    {
        var bytes4Bpp = SwitchDumpNibbles(sourceBytes);
        _bitmap = CreateBitmapFromBytes(bytes4Bpp, 4, PixelFormat.Format4bppIndexed);

        for (var i = 0; i <= 0xF; i++)
        {
            _orderedColors[i] = Color.FromArgb(i * 0x10, i * 0x10, i * 0x10);
        }

        _invertedColors = _orderedColors.Reverse().ToArray();
    }

    private static byte[] SwitchDumpNibbles(IReadOnlyList<byte> dumpBytes)
    {
        var resultBytes = new byte[dumpBytes.Count];
        for (var i = 0; i < dumpBytes.Count; i++)
        {
            resultBytes[i] = (byte)((dumpBytes[i] >> 4) | ((dumpBytes[i] & 0x0F) << 4));
        }

        return resultBytes;
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

        Rectangle.X = mainRectangle.X * 4;
        Rectangle.Y = mainRectangle.Y;
        Rectangle.Width = mainRectangle.Width * 4;
        Rectangle.Height = mainRectangle.Height;
        return _bitmap.Clone(Rectangle, _bitmap.PixelFormat);
    }
}