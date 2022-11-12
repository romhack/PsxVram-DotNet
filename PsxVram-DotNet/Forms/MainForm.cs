using PsxVram_DotNet.Modes;
using System.Drawing.Drawing2D;

namespace PsxVram_DotNet.Forms;

public partial class MainForm : Form
{
    public const int MaxWidth = 1024;
    public const int MaxHeight = 512;
    private const string DefaultCursorString = "Cursor X:0000 Y:000 Offset:0x00000";
    private const int DefaultSizeSelectedIndex = 1;
    private const int ModeFormOffset = 1050;
    private const int ClutWidth4Bpp = 0x10;
    private const int ClutWidth8Bpp = 0x100;

    private static readonly Pen ClutPen = new(Color.Magenta, 1);
    private static readonly Pen MainBorderPen = new(Color.Cyan, 1);
    private static readonly Brush MainFillBrush = new SolidBrush(Color.FromArgb(0x80, 0, 0xFF, 0xFF));


    private readonly BinaryHelper _binaryHelper;

    private readonly ModeForm _modeForm = new();
    private bool _clutMode;



    private Point _currentScrollPoint;
    private Rectangle _mainRectangle;
    private Rectangle _clutRectangle = new(0, 0, ClutWidth4Bpp, 1);
    private bool _panning;
    private Point _panningStartPoint;
    private int _zoomFactor = 1;
    private ModeSet? _modeSet;


    public MainForm()
    {
        InitializeComponent();
        _binaryHelper = new BinaryHelper(openFileDialog);
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        checkBoxGrayscaleInverted.Location = labelClutX.Location;
        checkBoxTransparency.Location = radioButtonGrayscale.Location;
        buttonSelectTransparentColor.Location = labelClutX.Location;
        comboBoxDefaultRectangleSize.Location = labelRectangleX.Location;
        comboBoxDefaultRectangleSize.SelectedIndex = DefaultSizeSelectedIndex; //320x240 default resolution
        var defaultSize = Mode.DefaultRectangleSizes[comboBoxDefaultRectangleSize.SelectedIndex];
        SetMainRectangle(_mainRectangle.X, _mainRectangle.Y, defaultSize.Width, defaultSize.Height);
        statusLabelCursor.Text = DefaultCursorString;
        _modeForm.Left = Left + ModeFormOffset;
        _modeForm.Top = Top;
        var startupSourceBytes = _binaryHelper.ReadStartupFile();
        if (startupSourceBytes is null)
        {
            return;
        }

        EnableControls();
        CreateModeSet(startupSourceBytes);
        SetMainFormCaption();
    }

    private void openFileButton_Click(object sender, EventArgs e)
    {
        var newSourceBytes = _binaryHelper.ReadNewFile();
        if (newSourceBytes is null)
        {
            return;
        }
        EnableControls();
        if (_modeSet is null)
        {
            CreateModeSet(newSourceBytes);
        }
        else
        {
            ReloadModeSet(newSourceBytes);
        }
        Activate();
        SetMainFormCaption();
    }

    private void SetMainFormCaption()
    {
        var fileName = Path.GetFileName(_binaryHelper.CurrentFileName);
        Text = $@"{fileName} - PsxVram-DotNet";
    }

    private void refreshButton_Click(object sender, EventArgs e)
    {
        var updatedSourceBytes = _binaryHelper.RefreshFile();
        if (updatedSourceBytes is null)
        {
            return;
        }
        ReloadModeSet(updatedSourceBytes);
    }

    private void EnableControls()
    {
        refreshButton.Enabled = true;
        zoomTrackBar.Enabled = true;
        saveImageButton.Enabled = true;
        groupBoxMode.Enabled = true;
        groupBoxRectangle.Enabled = true;
        groupBoxPalette.Enabled = true;
        MainPictureBox.Visible = true;
        toolStripDropDownSaveBinaryButton.Enabled = true;
    }


    private void CreateModeSet(byte[] sourceBytes)
    {
        _modeSet = new ModeSet(sourceBytes);
        RefreshWindows();
        _modeForm.Show(this);
    }

    private void ReloadModeSet(byte[] sourceBytes)
    {
        _modeSet?.Reload(sourceBytes);
        RefreshWindows();
    }

    private Rectangle GetZoomedRectangle(Rectangle originalRectangle)
    {
        return new Rectangle(originalRectangle.X * _zoomFactor,
            originalRectangle.Y * _zoomFactor,
            originalRectangle.Width * _zoomFactor,
            originalRectangle.Height * _zoomFactor);
    }

    private void PictureBox1_Paint(object sender, PaintEventArgs e)
    {
        e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;//Not possible to disable interpolation - redraw bitmap every paint
        e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
        if (_modeSet is null)
        {
            return;
        }
        e.Graphics.DrawImage(
            _modeSet.Mode16Bpp.GetBitmap(),
            new Rectangle(0, 0, MainPictureBox.Width, MainPictureBox.Height),
            0, 0, MaxWidth, MaxHeight,
            GraphicsUnit.Pixel);

        var zoomedMainRectangle = GetZoomedRectangle(_mainRectangle);
        e.Graphics.DrawRectangle(MainBorderPen, zoomedMainRectangle);
        e.Graphics.FillRectangle(MainFillBrush, zoomedMainRectangle);
        if (_clutMode)
        {
            var zoomedClutRectangle = GetZoomedRectangle(_clutRectangle);
            e.Graphics.DrawLine(ClutPen, 0, 0, zoomedClutRectangle.X, zoomedClutRectangle.Y);
            e.Graphics.DrawRectangle(ClutPen, zoomedClutRectangle.X, zoomedClutRectangle.Y,
                zoomedClutRectangle.Width + 1, zoomedClutRectangle.Height + 1);
        }
    }

    private void RefreshWindows()
    {
        MainPictureBox.Refresh();
        RefreshModeWindow();
    }

    private void RefreshModeWindow()
    {
        if (_modeSet is null)
        {
            return;
        }
        var trimConfiguration = new TrimConfiguration
        {
            Rectangle = _mainRectangle,
            IsTransparent = checkBoxTransparency.Checked,
            ClutColors = radioButtonClut.Checked ? _modeSet.GetClutColors(_clutRectangle) : Array.Empty<Color>(),
            IsInverted = checkBoxGrayscaleInverted.Checked
        };

        var updatedBitmap = _modeSet.CurrentMode.GetTrimmedBitmap(trimConfiguration);
        _modeForm.SetModeFormPictureBox(updatedBitmap);
        _modeForm.SetModeFormCaption();
    }

    /// <summary>
    /// Sets new rectangles, updates mode window bitmap according to new mode
    /// </summary>
    private void UpdateRectangles()
    {
        if (_modeSet is null)
        {
            return;
        }
        if (radioButtonDefaultRectangle.Checked)
        {
            var newSize = _modeSet.CurrentMode.GetDefaultSize(comboBoxDefaultRectangleSize.SelectedIndex);
            SetMainRectangle(_mainRectangle.X, _mainRectangle.Y, newSize.Width, newSize.Height);
        }

        RefreshWindows();
    }

    private static int RoundOff(int number, int interval)
    {
        var remainder = number % interval;
        return number - remainder;
    }

    private Point GetZoomedPoint(MouseEventArgs mousePoint)
    {
        return new Point(mousePoint.X / _zoomFactor, mousePoint.Y / _zoomFactor);
    }

    private void SetMainRectangle(int x, int y, int width, int height)
    {
        _mainRectangle.Width = width;
        _mainRectangle.Height = height;
        _mainRectangle.X = x + width > MaxWidth ? MaxWidth - width : x;
        _mainRectangle.Y = y + height > MaxHeight ? MaxHeight - height : y;

        var offset = (_mainRectangle.Y * MaxWidth + _mainRectangle.X) * 2;
        statusLabelRectangle.Text =
            $@"Rectangle W:{width:D4} H:{height:D3} X:{_mainRectangle.X:D4} Y:{_mainRectangle.Y:D3} Offset:0x{offset:X5}";
        numericUpDownX.Value = _mainRectangle.X;
        numericUpDownY.Value = _mainRectangle.Y;
        numericUpDownWidth.Value = _mainRectangle.Width;
        numericUpDownHeight.Value = _mainRectangle.Height;
    }

    private void SetClutRectangle(int x, int y, int width)
    {
        _clutRectangle.Y = y;
        _clutRectangle.Width = width;
        _clutRectangle.X = x + width > MaxWidth ? MaxWidth - width : x;
        var offset = (y * MaxWidth + _clutRectangle.X) * 2;
        statusLabelClut.Text = $@"CLUT X:{_clutRectangle.X:D4} Y:{y:D3} Offset:0x{offset:X5}";
        numericUpDownClutX.Value = _clutRectangle.X;
        numericUpDownClutY.Value = _clutRectangle.Y;
    }

    private void mainPictureBox_mouseClick(object sender, MouseEventArgs e)
    {
        var zoomedPoint = GetZoomedPoint(e);
        switch (e.Button)
        {
            case MouseButtons.Left:
            {
                //Snap to grid, caused by PSX hardware specifications in indexed modes:
                var newMainX = RoundOff(zoomedPoint.X, 0x40);
                var newMainY = RoundOff(zoomedPoint.Y, 0x100);
                SetMainRectangle(newMainX, newMainY, _mainRectangle.Width, _mainRectangle.Height);
                break;
            }
            case MouseButtons.Right:
            {
                if (_clutMode)
                {
                    var x = RoundOff(zoomedPoint.X, ClutWidth4Bpp); //Due to PSX specification
                    SetClutRectangle(x, zoomedPoint.Y, _clutRectangle.Width);
                }

                break;
            }
        }

        RefreshWindows();
    }

    private void HandleVisibility()
    {
        var isIndexedMode = radioButton8bpp.Checked | radioButton4bpp.Checked;
        groupBoxPalette.Visible = radioButton24bpp.Checked == false;
        radioButtonGrayscale.Visible = isIndexedMode;
        radioButtonClut.Visible = isIndexedMode;
        checkBoxTransparency.Visible = radioButton16bpp.Checked;
        buttonSelectTransparentColor.Visible = checkBoxTransparency.Visible;
        checkBoxGrayscaleInverted.Visible = radioButtonGrayscale.Checked & isIndexedMode;
        labelClutX.Visible = radioButtonClut.Checked & isIndexedMode;
        numericUpDownClutX.Visible = radioButtonClut.Checked & isIndexedMode;
        labelClutY.Visible = radioButtonClut.Checked & isIndexedMode;
        numericUpDownClutY.Visible = radioButtonClut.Checked & isIndexedMode;
        clutToolStripMenuItem.Enabled = radioButtonClut.Checked & isIndexedMode;
        comboBoxDefaultRectangleSize.Visible = radioButtonDefaultRectangle.Checked & (isIndexedMode == false);
        labelRectangleX.Visible = radioButtonCustomRectangle.Checked;
        labelRectangleY.Visible = radioButtonCustomRectangle.Checked;
        labelRectangleWidth.Visible = radioButtonCustomRectangle.Checked;
        labelRectangleHeight.Visible = radioButtonCustomRectangle.Checked;
        numericUpDownX.Visible = radioButtonCustomRectangle.Checked;
        numericUpDownY.Visible = radioButtonCustomRectangle.Checked;
        numericUpDownWidth.Visible = radioButtonCustomRectangle.Checked;
        numericUpDownHeight.Visible = radioButtonCustomRectangle.Checked;
        statusLabelClut.Visible = _clutMode;
    }

    private void radioButton24bpp_CheckedChanged(object sender, EventArgs e)
    {
        _clutMode = false;
        HandleVisibility();
        _modeSet!.SetCurrentModeIndex(ModeSet.Modes.Mode24Bpp);
        UpdateRectangles();
    }

    private void radioButton16bpp_CheckedChanged(object sender, EventArgs e)
    {
        _clutMode = false;
        HandleVisibility();
        _modeSet!.SetCurrentModeIndex(ModeSet.Modes.Mode16Bpp);
        UpdateRectangles();
    }

    private void radioButton8bpp_CheckedChanged(object sender, EventArgs e)
    {
        _clutMode = radioButtonClut.Checked;
        HandleVisibility();
        _modeSet!.SetCurrentModeIndex(ModeSet.Modes.Mode8Bpp);
        SetClutRectangle(_clutRectangle.X, _clutRectangle.Y, ClutWidth8Bpp);
        UpdateRectangles();
    }

    private void radioButton4bpp_CheckedChanged(object sender, EventArgs e)
    {
        _clutMode = radioButtonClut.Checked;
        HandleVisibility();
        _modeSet!.SetCurrentModeIndex(ModeSet.Modes.Mode4Bpp);
        SetClutRectangle(_clutRectangle.X, _clutRectangle.Y, ClutWidth4Bpp);
        UpdateRectangles();
    }

    private void radioButtonGrayscale_CheckedChanged(object sender, EventArgs e)
    {
        _clutMode = false;
        HandleVisibility();
        RefreshWindows();
    }

    private void radioButtonClut_CheckedChanged(object sender, EventArgs e)
    {
        _clutMode = true;
        HandleVisibility();
        RefreshWindows();
    }

    private void radioButtonDefaultRectangle_CheckedChanged(object sender, EventArgs e)
    {
        HandleVisibility();
        UpdateRectangles();
    }



    private void radioButtonCustomRectangle_CheckedChanged(object sender, EventArgs e)
    {
        HandleVisibility();
        numericUpDownX.Value = _mainRectangle.X;
        numericUpDownY.Value = _mainRectangle.Y;
        numericUpDownWidth.Value = _mainRectangle.Width;
        numericUpDownHeight.Value = _mainRectangle.Height;
    }


    private void buttonSelectTransparentColor_Click(object sender, EventArgs e)
    {
        colorDialogTransparent.Color = MainPictureBox.BackColor;
        if (colorDialogTransparent.ShowDialog() == DialogResult.OK)
        {
            _modeForm.SetModeFormPictureBoxBackColor(colorDialogTransparent.Color);
        }
    }

    private void comboBoxDefaultRectangleSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateRectangles();
    }

    private void CheckBoxTransparency_CheckedChanged(object sender, EventArgs e)
    {
        RefreshModeWindow();
    }

    private void CheckBoxGrayscaleInverted_CheckedChanged(object sender, EventArgs e)
    {
        RefreshModeWindow();
    }

    private void numericUpDownClutX_ValueChanged(object sender, EventArgs e)
    {
        SetClutRectangle((int)numericUpDownClutX.Value, _clutRectangle.Y, _clutRectangle.Width);
        RefreshWindows();
    }

    private void numericUpDownClutY_ValueChanged(object sender, EventArgs e)
    {
        SetClutRectangle(_clutRectangle.X, (int)numericUpDownClutY.Value, _clutRectangle.Width);
        RefreshWindows();
    }

    private void numericUpDownX_ValueChanged(object sender, EventArgs e)
    {
        SetMainRectangle((int)numericUpDownX.Value, _mainRectangle.Y, _mainRectangle.Width, _mainRectangle.Height);
        RefreshWindows();
    }

    private void numericUpDownY_ValueChanged(object sender, EventArgs e)
    {
        SetMainRectangle(_mainRectangle.X, (int)numericUpDownY.Value, _mainRectangle.Width, _mainRectangle.Height);
        RefreshWindows();
    }

    private void numericUpDownWidth_ValueChanged(object sender, EventArgs e)
    {
        SetMainRectangle(_mainRectangle.X, _mainRectangle.Y, (int)numericUpDownWidth.Value, _mainRectangle.Height);
        RefreshWindows();
    }

    private void numericUpDownHeight_ValueChanged(object sender, EventArgs e)
    {
        SetMainRectangle(_mainRectangle.X, _mainRectangle.Y, _mainRectangle.Width, (int)numericUpDownHeight.Value);
        RefreshWindows();
    }

    private void MainForm_KeyDown(object sender, KeyEventArgs e)
    {
        var incrementsDictionary = new Dictionary<(Keys, Keys), (bool, NumericUpDown, decimal)>
        {
            { (Keys.None, Keys.W), (true, numericUpDownY, -1) },
            { (Keys.None, Keys.S), (true, numericUpDownY, 1) },
            { (Keys.None, Keys.A), (true, numericUpDownX, -1) },
            { (Keys.None, Keys.D), (true, numericUpDownX, 1) },
            { (Keys.Shift, Keys.W), (radioButtonCustomRectangle.Checked, numericUpDownHeight, -1) },
            { (Keys.Shift, Keys.S), (radioButtonCustomRectangle.Checked, numericUpDownHeight, 1) },
            { (Keys.Shift, Keys.A), (radioButtonCustomRectangle.Checked, numericUpDownWidth, -1) },
            { (Keys.Shift, Keys.D), (radioButtonCustomRectangle.Checked, numericUpDownWidth, 1) },
            { (Keys.Control, Keys.W), (_clutMode, numericUpDownClutY, -1) },
            { (Keys.Control, Keys.S), (_clutMode, numericUpDownClutY, 1) },
            { (Keys.Control, Keys.A), (_clutMode, numericUpDownClutX, -numericUpDownClutX.Increment) },
            { (Keys.Control, Keys.D), (_clutMode, numericUpDownClutX, numericUpDownClutX.Increment) }
        };
        if (MainPictureBox.Visible == false) //On startup moving is disabled
        {
            return;
        }

        var hasValue = incrementsDictionary.TryGetValue((e.Modifiers, e.KeyCode), out var incrementTuple);
        if (hasValue == false)
        {
            return;
        }

        var (term, numericUpDown, increment) = incrementTuple;
        if (term == false)
        {
            return;
        }

        ActiveControl = groupBoxRectangle; //To suppress ding sound on active NumericUpDown change
        numericUpDown.Value = Math.Max(numericUpDown.Minimum,
            Math.Min(numericUpDown.Value + increment, numericUpDown.Maximum));
    }


    private void saveImageButton_Click(object sender, EventArgs e)
    {
        _binaryHelper.SaveModeImage(_modeForm.ModeImage);
    }


    private void aboutButton_Click(object sender, EventArgs e)
    {
        AboutBox aboutBox = new();
        aboutBox.ShowDialog();
    }

    private void MainPictureBox_MouseMove(object sender, MouseEventArgs e)
    {
        if (_panning)
        {
            _currentScrollPoint.X = _currentScrollPoint.X + _panningStartPoint.X - e.X;
            _currentScrollPoint.Y = _currentScrollPoint.Y + _panningStartPoint.Y - e.Y;
            MainPanel.AutoScrollPosition = _currentScrollPoint;
        }

        var zoomedPoint = GetZoomedPoint(e);
        var offset = (zoomedPoint.Y * MaxWidth + zoomedPoint.X) * 2;
        statusLabelCursor.Text = $@"Cursor X:{zoomedPoint.X:D4} Y:{zoomedPoint.Y:D3} Offset:0x{offset:X5}";
    }

    private void MainPictureBox_MouseLeave(object sender, EventArgs e)
    {
        statusLabelCursor.Text = DefaultCursorString;
    }

    private void MainPictureBox_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Middle || _zoomFactor == 1)
        {
            return;
        }
        _panning = true;
        Cursor.Current = Cursors.NoMove2D;
        _panningStartPoint.X = e.X;
        _panningStartPoint.Y = e.Y;
    }

    private void MainPictureBox_MouseUp(object sender, MouseEventArgs e)
    {
        _panning = false;
        Cursor.Current = Cursors.Default;
    }

    private void zoomTrackBar_ValueChanged(object sender, EventArgs e)
    {
        _zoomFactor = zoomTrackBar.Value;
        MainPictureBox.Width = MaxWidth * _zoomFactor;
        MainPictureBox.Height = MaxHeight * _zoomFactor;
    }

    private void firstScanlineToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (_modeSet is null)
        {
            return;
        }
        var firstScanlineRectangle = _mainRectangle with {Height = 1};
        var bytes = _modeSet.GetTrimmedBytes(firstScanlineRectangle);
        _binaryHelper.SetClipboard(bytes);
        _binaryHelper.SaveBinary(bytes, "scanline_pixels");
    }

    private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (_modeSet is null)
        {
            return;
        }
        var bytes = _modeSet.GetTrimmedBytes(_mainRectangle);
        _binaryHelper.SetClipboard(bytes);
        _binaryHelper.SaveBinary(bytes, "selected_pixels");
    }

    private void clutToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (_modeSet is null)
        {
            return;
        }
        var bytes = _modeSet.GetTrimmedBytes(_clutRectangle);
        _binaryHelper.SetClipboard(bytes);
        _binaryHelper.SaveBinary(bytes, "clut");
    }
}