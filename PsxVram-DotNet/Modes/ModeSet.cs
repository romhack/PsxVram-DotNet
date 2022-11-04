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
        
    public Color[] GetClutColors(Rectangle clutRectangle)
    {
        return Mode16Bpp.GetClutColors(clutRectangle);
    }

    public void SetCurrentModeIndex(Modes newCurrentModeIndex)
    {
        _currentModeIndex = newCurrentModeIndex;
    }



    public ModeSet(byte[] sourceBytes)
    {
        _modesDictionary = CreateModesDictionary(sourceBytes);
        _currentModeIndex = Modes.Mode16Bpp;
    }

    public void Reload(byte[] sourceBytes)
    {
        _modesDictionary = CreateModesDictionary(sourceBytes);
    }

    private static Dictionary<Modes, Mode> CreateModesDictionary(byte[] sourceBytes)
    {
        return new Dictionary<Modes, Mode>
        {
            {Modes.Mode24Bpp, new Mode24Bpp(sourceBytes)},
            {Modes.Mode16Bpp, new Mode16Bpp(sourceBytes)},
            {Modes.Mode8Bpp, new Mode8Bpp(sourceBytes)},
            {Modes.Mode4Bpp, new Mode4Bpp(sourceBytes)}
        };
    }


}