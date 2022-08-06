using System.Drawing.Imaging;

namespace PsxVram_DotNet.Modes
{
    internal class Mode4bpp : Mode
    {
        private readonly Color[] _orderedColors = new Color[0x10];
        private readonly Color[] _invertedColors = new Color[0x10];

        private static byte[] SwitchDumpNibbles(byte[] dumpBytes)
        {
            var resultBytes = new byte[dumpBytes.Length];
            for (var i = 0; i < dumpBytes.Length; i++)
            {
                resultBytes[i] = (byte)(dumpBytes[i] >> 4 | (dumpBytes[i] & 0x0F) << 4);
            }
            return resultBytes;
        }

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

            _rectangle.X = mainRectangle.X * 4;
            _rectangle.Y = mainRectangle.Y;
            _rectangle.Width = mainRectangle.Width * 4;
            _rectangle.Height = mainRectangle.Height;
            return _bitmap.Clone(_rectangle, _bitmap.PixelFormat);
        }

        public Mode4bpp(byte[] sourceBytes)
        {
            var bytes4bpp = SwitchDumpNibbles(sourceBytes);
            _bitmap = CreateBitmapFromBytes(bytes4bpp, 4, PixelFormat.Format4bppIndexed);

            for (int i = 0; i <= 0xF; i++)
            {
                _orderedColors[i] = Color.FromArgb(i * 0x10, i * 0x10, i * 0x10);
            }
            _invertedColors = _orderedColors.Reverse().ToArray();
        }
    }
}