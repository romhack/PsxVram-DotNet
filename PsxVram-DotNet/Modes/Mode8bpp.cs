using System.Drawing.Imaging;

namespace PsxVram_DotNet.Modes;

internal class Mode8Bpp : Mode
{
    private readonly Color[] _invertedColors;
    private readonly Color[] _orderedColors = new Color[0x100];

    public Mode8Bpp(byte[] sourceBytes)
    {
        Bitmap = CreateBitmapFromBytes(sourceBytes, 2, PixelFormat.Format8bppIndexed);
        for (var i = 0; i <= 0xFF; i++)
        {
            _orderedColors[i] = Color.FromArgb(i, i, i);
        }

        _invertedColors = _orderedColors.Reverse().ToArray();
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
            with {Width = trimConfiguration.Rectangle.Width * 2, X = trimConfiguration.Rectangle.X * 2};
        return Bitmap.Clone(rectangle, Bitmap.PixelFormat);
    }

    public override Size GetDefaultSize(int _)
    {
        return new Size(0x80, 0x100); //Default 256x256 rect on 16bpp main form;
    }
}