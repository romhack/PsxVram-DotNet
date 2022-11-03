using System.Drawing.Imaging;

namespace PsxVram_DotNet.Modes;

internal class Mode4Bpp : Mode
{
    private readonly Color[] _invertedColors;
    private readonly Color[] _orderedColors = new Color[0x10];

    public Mode4Bpp(IReadOnlyList<byte> sourceBytes)
    {
        var bytes4Bpp = SwitchDumpNibbles(sourceBytes);
        Bitmap = CreateBitmapFromBytes(bytes4Bpp, 4, PixelFormat.Format4bppIndexed);

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

    public override Bitmap GetTrimmedBitmap(TrimConfiguration trimConfiguration)
    {
        Color[] currentColors;
        if (trimConfiguration.ClutColors.Any()) //CLUT mode
        {
            currentColors = trimConfiguration.ClutColors;
        }
        else //Normal/Inverted mode
        {
            currentColors = trimConfiguration.IsInverted ? _invertedColors : _orderedColors;
        }

        var currentPalette = Bitmap.Palette;
        for (var i = 0; i < Bitmap.Palette.Entries.Length; i++)
        {
            currentPalette.Entries[i] = currentColors[i];
        }

        Bitmap.Palette = currentPalette;

        var rectangle = trimConfiguration.Rectangle
            with { Width = trimConfiguration.Rectangle.Width * 4, X = trimConfiguration.Rectangle.X * 4 };

        return Bitmap.Clone(rectangle, Bitmap.PixelFormat);
    }

    public override Size GetDefaultSize(int _)
    {
        return new Size(0x40, 0x100);
    }
}