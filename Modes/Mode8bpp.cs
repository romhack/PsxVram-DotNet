using System.Drawing.Imaging;

namespace PsxVram_DotNet.Modes
{
    internal class Mode8bpp : Mode
    {
        private readonly Color[] _orderedColors = new Color[0x100];
        private readonly Color[] _invertedColors = new Color[0x100];

        public Bitmap GetTrimmedBitmap(Rectangle mainRectangle, Color[]? clutColors = null, bool inverted = false)
        {
            Color[] currentColors;
            if (clutColors is not null)//CLUT mode
            {
                currentColors = clutColors;
            }
            else//Normal/Inverted mode
            {
                currentColors = inverted ? _invertedColors : _orderedColors;
            }
            var currentPalette = _bitmap.Palette;
            for (int i = 0; i < _bitmap.Palette.Entries.Length; i++) currentPalette.Entries[i] = currentColors[i];
            _bitmap.Palette = currentPalette;

            _rectangle.X = mainRectangle.X * 2;
            _rectangle.Y = mainRectangle.Y;
            _rectangle.Width = mainRectangle.Width * 2;
            _rectangle.Height = mainRectangle.Height;
            return _bitmap.Clone(_rectangle, _bitmap.PixelFormat);
        }

        public Mode8bpp(byte[] sourceBytes)
        {
            _bitmap = CreateBitmapFromBytes(sourceBytes, 2, PixelFormat.Format8bppIndexed);
            for (int i = 0; i <= 0xFF; i++)
            {
                _orderedColors[i] = Color.FromArgb(i, i, i);
            }
            _invertedColors = _orderedColors.Reverse().ToArray();
        }
    }
}