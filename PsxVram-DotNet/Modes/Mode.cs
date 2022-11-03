#nullable disable
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using PsxVram_DotNet.Forms;

namespace PsxVram_DotNet.Modes;

internal abstract class Mode
{
    public static readonly List<Size> DefaultRectangleSizes = new()
    {
        new Size(256, 240),
        new Size(320, 240),
        new Size(368, 240),
        new Size(512, 240),
        new Size(640, 240),
        new Size(256, 480),
        new Size(320, 480),
        new Size(368, 480),
        new Size(512, 480),
        new Size(640, 480)
    };

    private protected Bitmap Bitmap;

    public abstract Bitmap GetTrimmedBitmap(TrimConfiguration trimConfiguration);

    protected static Bitmap CreateBitmapFromBytes(byte[] sourceBytes, int widthScale, PixelFormat pixelFormat)
    {
        var bitmap = new Bitmap(MainForm.MaxWidth * widthScale, MainForm.MaxHeight, pixelFormat);
        var bitmapData = bitmap.LockBits(new Rectangle(0, 0, MainForm.MaxWidth * widthScale, MainForm.MaxHeight),
            ImageLockMode.WriteOnly, pixelFormat);
        Marshal.Copy(sourceBytes, 0, bitmapData.Scan0, sourceBytes.Length);
        bitmap.UnlockBits(bitmapData);
        return bitmap;
    }
    public virtual Size GetDefaultSize(int selectedIndex)
    {
        return DefaultRectangleSizes[selectedIndex];
    }
}