using System.Drawing.Imaging;
using System.IO.Compression;
using System.Text;

namespace PsxVram_DotNet;

internal class BinaryHelper
{
    private const string DefaultFileName = "vram.bin";
    private const int VramDumpSize = 0x100000;
    private readonly OpenFileDialog _openFileDialog;
    public string CurrentFileName { get; private set; }


    public BinaryHelper(OpenFileDialog openFileDialog)
    {
        _openFileDialog = openFileDialog;
        CurrentFileName = DefaultFileName;
    }

    /// <summary>
    ///     Read file at startup check for command line or default file
    /// </summary>
    /// <returns>null if file is not found or wrong format</returns>
    public byte[]? ReadStartupFile()
    {
        byte[]? startupBytes = null;
        var arguments = Environment.GetCommandLineArgs();
        if (arguments.Length == 2)
        {
            startupBytes = ReadFile(arguments[1]);
            if (startupBytes is not null)
            {
                CurrentFileName = arguments[1];
            }
        }
        else if (File.Exists(DefaultFileName))
        {
            startupBytes = ReadFile(DefaultFileName);
            if (startupBytes is not null)
            {
                CurrentFileName = DefaultFileName;
            }
        }

        if (startupBytes is not null)
        {
            var initialDirectory = Path.GetDirectoryName(CurrentFileName);
            if (string.IsNullOrWhiteSpace(initialDirectory))
            {
                initialDirectory = AppContext.BaseDirectory;
            }

            _openFileDialog.InitialDirectory = initialDirectory;
        }

        return startupBytes;
    }

    public byte[]? ReadNewFile()
    {
        if (_openFileDialog.ShowDialog() == DialogResult.Cancel)
        {
            return null;
        }

        var newFileName = _openFileDialog.FileName;
        var newFileBytes = ReadFile(newFileName);
        if (newFileBytes is null)
        {
            ShowFormatErrorMessage();
        }
        else
        {
            CurrentFileName = newFileName;
        }

        return newFileBytes;
    }

    public byte[]? RefreshFile()
    {
        var refreshedBytes = ReadFile(CurrentFileName);
        if (refreshedBytes is null)
        {
            ShowFormatErrorMessage();
        }

        return refreshedBytes;
    }

    private static void ShowFormatErrorMessage()
    {
        MessageBox.Show(@"File format is not recognized", @"Input file error", MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }

    private static byte[]? ReadFile(string fileName)
    {
        if (File.Exists(fileName) == false)
        {
            throw new FileNotFoundException("Initially specified input file not found");
        }

        using var fileBinaryReader = new BinaryReader(File.OpenRead(fileName));
        {
            var rawHeaders = new[] //Headers for non-compressed savestates
            {
                ("NO$PSX SNAPSHOT", 0x289070),
                ("RASTATE", 0x20AA29) //Retroarch
            };

            var rawDump = ReadFirstDumpFromHeaders(fileBinaryReader, rawHeaders);
            if (rawDump is not null)
            {
                return rawDump;
            }

            var gzDump = ReadGz(fileBinaryReader);
            return gzDump ?? ReadRawDump(fileBinaryReader);
        }
    }

    private static byte[]? ReadRawDump(BinaryReader rawDumpBinaryReader)
    {
        if (rawDumpBinaryReader.BaseStream.Length != VramDumpSize)
        {
            return null;
        }

        rawDumpBinaryReader.BaseStream.Seek(0, SeekOrigin.Begin);
        return rawDumpBinaryReader.ReadBytes(VramDumpSize);
    }

    /// <summary>
    ///     Reads dump bytes from snap of the first format it finds correct
    /// </summary>
    /// <param name="snapBinaryReader"></param>
    /// <param name="headerTuples"></param>
    /// <returns>null, if no formats fit current snap</returns>
    private static byte[]? ReadFirstDumpFromHeaders(BinaryReader snapBinaryReader,
        IEnumerable<(string, int)> headerTuples)
    {
        foreach (var (headerString, offset) in headerTuples)
        {
            snapBinaryReader.BaseStream.Seek(0, SeekOrigin.Begin);
            var currentHeaderString = Encoding.ASCII.GetString(snapBinaryReader.ReadBytes(headerString.Length));
            if (string.Equals(headerString, currentHeaderString))
            {
                snapBinaryReader.BaseStream.Seek(offset, SeekOrigin.Begin);
                return snapBinaryReader.ReadBytes(VramDumpSize);
            }
        }

        return null;
    }


    private static byte[]? ReadGz(BinaryReader rawSnapBinaryReader)
    {
        const ushort gzHeaderWord = 0x8B1F;
        rawSnapBinaryReader.BaseStream.Seek(0, SeekOrigin.Begin);
        var currentHeaderWord = rawSnapBinaryReader.ReadUInt16();
        if (currentHeaderWord != gzHeaderWord)
        {
            return null;
        }

        var uncompressedGzBinaryReader = UncompressGz(rawSnapBinaryReader);
        var gzHeaders = new[] //Headers for GZ compressed states
        {
            ("ePSXe", 0x2733df),
            ("STv3 PCSX v1.5", 0x2996c0),
            ("STv4 PCSXR v1.9", 0x29B749)
        };
        return ReadFirstDumpFromHeaders(uncompressedGzBinaryReader, gzHeaders);
    }

    private static BinaryReader UncompressGz(BinaryReader compressedBinaryReader)
    {
        compressedBinaryReader.BaseStream.Seek(0, SeekOrigin.Begin);
        var uncompressed = new GZipStream(compressedBinaryReader.BaseStream, CompressionMode.Decompress);
        var helperMemoryStream = new MemoryStream();
        uncompressed.CopyTo(helperMemoryStream);
        var uncompressedBinaryReader = new BinaryReader(helperMemoryStream);
        return uncompressedBinaryReader;
    }


    public void SaveModeImage(Image modeImage)
    {
        var fileName = $"{Path.GetFileNameWithoutExtension(CurrentFileName)}_{DateTime.Now:yyyyMMdd_HH_mm_ss}.bmp";
        var fileDirectory = Path.GetDirectoryName(CurrentFileName) ?? "";
        var filePath = Path.Combine(fileDirectory, fileName);
        try
        {
            modeImage.Save(filePath, ImageFormat.Bmp);
        }
        catch (Exception)
        {
            MessageBox.Show(@"Could not write image", @"Write file error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    public void SaveBinary(byte[] bytes, string postfix)
    {
        var fileName = $"{Path.GetFileNameWithoutExtension(CurrentFileName)}_{DateTime.Now:yyyyMMdd_HH_mm_ss}_{postfix}.bin";
        var fileDirectory = Path.GetDirectoryName(CurrentFileName) ?? "";
        var filePath = Path.Combine(fileDirectory, fileName);
        try
        {
            File.WriteAllBytes(filePath, bytes);
        }
        catch (Exception)
        {
            MessageBox.Show(@"Could not write binary file", @"Write file error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}