using PsxVram_DotNet.Forms;

namespace PsxVram_DotNet.Modes;

internal class ModeSet
{
    internal enum Modes

    {
        Mode24Bpp,
        Mode16Bpp,
        Mode8Bpp,
        Mode4Bpp
    }

    private Modes _currentModeIndex;
    private Dictionary<Modes, Mode> _modesDictionary;
    private byte[] _bytes = Array.Empty<byte>();

    public Mode CurrentMode => _modesDictionary[_currentModeIndex];

    public Mode16Bpp Mode16Bpp
    {
        get
        {
            if (_modesDictionary[Modes.Mode16Bpp] is not Mode16Bpp mode16Bpp)
            {
                throw new InvalidOperationException("Mode 16 bpp is not found");
            }
            return mode16Bpp;
        }
    }
    
    public ModeSet(byte[] sourceBytes)
    {
        _modesDictionary = CreateModesDictionary(sourceBytes);
        _currentModeIndex = Modes.Mode16Bpp;
    }

    public Color[] GetClutColors(Rectangle clutRectangle)
    {
        return Mode16Bpp.GetClutColors(clutRectangle);
    }

    public void SetCurrentModeIndex(Modes newCurrentModeIndex)
    {
        _currentModeIndex = newCurrentModeIndex;
    }


    public void Reload(byte[] sourceBytes)
    {
        _modesDictionary = CreateModesDictionary(sourceBytes);
    }

    private Dictionary<Modes, Mode> CreateModesDictionary(byte[] sourceBytes)
    {
        _bytes = sourceBytes;
        return new Dictionary<Modes, Mode>
        {
            {Modes.Mode24Bpp, new Mode24Bpp(sourceBytes)},
            {Modes.Mode16Bpp, new Mode16Bpp(sourceBytes)},
            {Modes.Mode8Bpp, new Mode8Bpp(sourceBytes)},
            {Modes.Mode4Bpp, new Mode4Bpp(sourceBytes)}
        };
    }
    /// <summary>
    /// Get binary chunk of loaded dump, defined by rectangle in 16bpp
    /// </summary>
    public byte[] GetTrimmedBytes(Rectangle trimRectangle)
    {
        var chunk = new List<byte>();
        for (var dy = 0; dy < trimRectangle.Height; dy++)
        {
            var startByteIndex = ((dy + trimRectangle.Y) * MainForm.MaxWidth + trimRectangle.X) * 2;
            var endByteIndex = startByteIndex + trimRectangle.Width * 2;
            chunk.AddRange(_bytes[startByteIndex..endByteIndex]);

        }
        return chunk.ToArray();
        
    }

}