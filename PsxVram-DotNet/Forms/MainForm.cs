using PsxVram_DotNet.Modes;

namespace PsxVram_DotNet.Forms;

public partial class MainForm : Form
{
    public const int MaxWidth = 1024;
    public const int MaxHeight = 512;
    private const string DefaultCursorString = "Cursor X:0000 Y:000 Offset:0x00000";
    private readonly Size _default4BppRectangleSize = new(0x40, 0x100);
    private readonly Size _default8BppRectangleSize = new(0x80, 0x100); //Default 256x256 rect on 16bpp main form

    private readonly List<Size> _defaultRectangleSizes = new()
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

    private readonly FileHelper _fileHelper;

    private readonly ModeForm _modeForm = new();
    private bool _clutMode;
    private Rectangle _clutRectangle = new(0, 0, 0x10, 1);

    private Modes _currentMode = Modes.Mode16Bpp;
    private Point _currentScrollPoint;
    private Rectangle _mainRectangle;

    private Mode16Bpp? _mode16Bpp;
    private Mode24Bpp? _mode24Bpp;
    private Mode4Bpp? _mode4Bpp;
    private Mode8Bpp? _mode8Bpp;
    private bool _panning;
    private Point _panningStartPoint;

    private int _zoomFactor = 1;

    public MainForm()
    {
        InitializeComponent();
        _fileHelper = new FileHelper(openFileDialog);
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        checkBoxGrayscaleInverted.Location = labelClutX.Location;
        checkBoxTransparency.Location = radioButtonGrayscale.Location;
        buttonSelectTransparentColor.Location = labelClutX.Location;
        comboBoxDefaultRectangleSize.Location = labelRectangleX.Location;
        comboBoxDefaultRectangleSize.SelectedIndex = 1; //320x240 default resolution
        statusLabelCursor.Text = DefaultCursorString;
        _modeForm.Left = Left + 1050;
        _modeForm.Top = Top;
        var startupSourceBytes = _fileHelper.ReadStartupFile();
        if (startupSourceBytes is null)
        {
            return;
        }

        EnableControls();
        DisplaySourceBytes(startupSourceBytes);
    }

    private void openFileButton_Click(object sender, EventArgs e)
    {
        var newSourceBytes = _fileHelper.ReadNewFile();
        if (newSourceBytes is null)
        {
            return;
        }

        EnableControls();
        DisplaySourceBytes(newSourceBytes);
        Activate();
    }

    private void refreshButton_Click(object sender, EventArgs e)
    {
        var updatedSourceBytes = _fileHelper.RefreshFile();
        if (updatedSourceBytes is null)
        {
            return;
        }

        DisplaySourceBytes(updatedSourceBytes);
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
    }


    private void DisplaySourceBytes(byte[] sourceBytes)
    {
        _mode16Bpp = new Mode16Bpp(sourceBytes);
        _mode8Bpp = new Mode8Bpp(sourceBytes);
        _mode4Bpp = new Mode4Bpp(sourceBytes);
        _mode24Bpp = new Mode24Bpp(sourceBytes);
        MainPictureBox.Image = _mode16Bpp.Bitmap;

        UpdateModeWindow();
        if (Application.OpenForms.OfType<ModeForm>().Any() == false)
        {
            _modeForm.Show(this);
        }
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
        Pen clutPen = new(Color.Magenta, 1);
        Pen mainBorderPen = new(Color.Cyan, 1);
        Brush mainFillBrush = new SolidBrush(Color.FromArgb(0x80, 0, 0xFF, 0xFF));
        var zoomedMainRectangle = GetZoomedRectangle(_mainRectangle);
        e.Graphics.DrawRectangle(mainBorderPen, zoomedMainRectangle);
        e.Graphics.FillRectangle(mainFillBrush, zoomedMainRectangle);
        if (_clutMode)
        {
            var zoomedClutRectangle = GetZoomedRectangle(_clutRectangle);
            e.Graphics.DrawLine(clutPen, 0, 0, zoomedClutRectangle.X, zoomedClutRectangle.Y);
            e.Graphics.DrawRectangle(clutPen, zoomedClutRectangle.X, zoomedClutRectangle.Y - 1,
                zoomedClutRectangle.Width, 2);
        }
    }

    private Mode? GetCurrentMode()
    {
        Dictionary<Modes, Mode?> modeTagDictionary = new()
        {
            { Modes.Mode16Bpp, _mode16Bpp },
            { Modes.Mode8Bpp, _mode8Bpp },
            { Modes.Mode4Bpp, _mode4Bpp },
            { Modes.Mode24Bpp, _mode24Bpp }
        };
        var currentMode = modeTagDictionary[_currentMode];
        return currentMode;
    }

    private void UpdateModeWindow()
    {
        var currentMode = GetCurrentMode();
        if (currentMode is null)
        {
            return;
        }

        Bitmap? updatedBitmap = null;
        switch (currentMode)
        {
            case Mode24Bpp mode24:
                updatedBitmap = mode24.GetTrimmedBitmap(_mainRectangle);
                break;

            case Mode16Bpp mode16:
                updatedBitmap = mode16.GetTrimmedBitmap(_mainRectangle, checkBoxTransparency.Checked);
                break;

            case Mode8Bpp mode8Bpp:
                if (radioButtonClut.Checked)
                {
                    var updatedClutColors = _mode16Bpp?.GetClutColors(_clutRectangle);
                    updatedBitmap = mode8Bpp.GetTrimmedBitmap(_mainRectangle, updatedClutColors);
                }
                else
                {
                    updatedBitmap =
                        mode8Bpp.GetTrimmedBitmap(_mainRectangle, inverted: checkBoxGrayscaleInverted.Checked);
                }

                break;

            case Mode4Bpp mode4Bpp:
                if (radioButtonClut.Checked)
                {
                    var updatedClutColors = _mode16Bpp?.GetClutColors(_clutRectangle);
                    updatedBitmap = mode4Bpp.GetTrimmedBitmap(_mainRectangle, updatedClutColors);
                }
                else
                {
                    updatedBitmap =
                        mode4Bpp.GetTrimmedBitmap(_mainRectangle, inverted: checkBoxGrayscaleInverted.Checked);
                }

                break;
        }

        if (updatedBitmap is not null)
        {
            _modeForm.SetModeFormPictureBox(updatedBitmap);
            _modeForm.SetModeFormCaption();
        }
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
                //Due to PSX hardware specifications in indexed modes:
                var newMainX = RoundOff(zoomedPoint.X, 0x40);
                var newMainY = RoundOff(zoomedPoint.Y, 0x100);
                SetMainRectangle(newMainX, newMainY, _mainRectangle.Width, _mainRectangle.Height);
                break;
            }
            case MouseButtons.Right:
            {
                if (_clutMode)
                {
                    var x = RoundOff(zoomedPoint.X, 0x10); //Due to PSX specification
                    SetClutRectangle(x, zoomedPoint.Y, _clutRectangle.Width);
                }

                break;
            }
        }

        MainPictureBox.Refresh();
        UpdateModeWindow();
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
        _currentMode = Modes.Mode24Bpp;
        if (radioButtonDefaultRectangle.Checked)
        {
            var newSize = _defaultRectangleSizes[comboBoxDefaultRectangleSize.SelectedIndex];
            SetMainRectangle(_mainRectangle.X, _mainRectangle.Y, newSize.Width, newSize.Height);
        }

        MainPictureBox.Refresh();
        UpdateModeWindow();
    }

    private void radioButton16bpp_CheckedChanged(object sender, EventArgs e)
    {
        _clutMode = false;
        HandleVisibility();
        _currentMode = Modes.Mode16Bpp;
        if (radioButtonDefaultRectangle.Checked)
        {
            var newSize = _defaultRectangleSizes[comboBoxDefaultRectangleSize.SelectedIndex];
            SetMainRectangle(_mainRectangle.X, _mainRectangle.Y, newSize.Width, newSize.Height);
        }

        MainPictureBox.Refresh();
        UpdateModeWindow();
    }

    private void radioButton8bpp_CheckedChanged(object sender, EventArgs e)
    {
        _clutMode = radioButtonClut.Checked;
        HandleVisibility();
        _currentMode = Modes.Mode8Bpp;
        SetClutRectangle(_clutRectangle.X, _clutRectangle.Y, 0x100);
        if (radioButtonDefaultRectangle.Checked)
        {
            SetMainRectangle(_mainRectangle.X, _mainRectangle.Y, _default8BppRectangleSize.Width,
                _default8BppRectangleSize.Height);
        }

        MainPictureBox.Refresh();
        UpdateModeWindow();
    }

    private void radioButton4bpp_CheckedChanged(object sender, EventArgs e)
    {
        _clutMode = radioButtonClut.Checked;
        HandleVisibility();
        _currentMode = Modes.Mode4Bpp;
        SetClutRectangle(_clutRectangle.X, _clutRectangle.Y, 0x10);
        if (radioButtonDefaultRectangle.Checked)
        {
            SetMainRectangle(_mainRectangle.X, _mainRectangle.Y, _default4BppRectangleSize.Width,
                _default4BppRectangleSize.Height);
        }

        MainPictureBox.Refresh();
        UpdateModeWindow();
    }

    private void radioButtonGrayscale_CheckedChanged(object sender, EventArgs e)
    {
        _clutMode = false;
        HandleVisibility();
        MainPictureBox.Refresh();
        UpdateModeWindow();
    }

    private void radioButtonClut_CheckedChanged(object sender, EventArgs e)
    {
        _clutMode = true;
        HandleVisibility();
        MainPictureBox.Refresh();
        UpdateModeWindow();
    }

    private void radioButtonDefaultRectangle_CheckedChanged(object sender, EventArgs e)
    {
        HandleVisibility();

        switch (_currentMode)
        {
            case Modes.Mode4Bpp:
                SetMainRectangle(_mainRectangle.X, _mainRectangle.Y, _default4BppRectangleSize.Width,
                    _default4BppRectangleSize.Height);
                break;

            case Modes.Mode8Bpp:
                SetMainRectangle(_mainRectangle.X, _mainRectangle.Y, _default8BppRectangleSize.Width,
                    _default8BppRectangleSize.Height);
                break;

            case Modes.Mode16Bpp:
            case Modes.Mode24Bpp:
            default:
                var newSize = _defaultRectangleSizes[comboBoxDefaultRectangleSize.SelectedIndex];
                SetMainRectangle(_mainRectangle.X, _mainRectangle.Y, newSize.Width, newSize.Height);
                break;
        }

        MainPictureBox.Refresh();
        UpdateModeWindow();
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
        var newSize = _defaultRectangleSizes[comboBoxDefaultRectangleSize.SelectedIndex];
        SetMainRectangle(_mainRectangle.X, _mainRectangle.Y, newSize.Width, newSize.Height);
        MainPictureBox.Refresh();
        UpdateModeWindow();
    }

    private void CheckBoxTransparency_CheckedChanged(object sender, EventArgs e)
    {
        UpdateModeWindow();
    }

    private void CheckBoxGrayscaleInverted_CheckedChanged(object sender, EventArgs e)
    {
        UpdateModeWindow();
    }

    private void numericUpDownClutX_ValueChanged(object sender, EventArgs e)
    {
        SetClutRectangle((int)numericUpDownClutX.Value, _clutRectangle.Y, _clutRectangle.Width);
        MainPictureBox.Refresh();
        UpdateModeWindow();
    }

    private void numericUpDownClutY_ValueChanged(object sender, EventArgs e)
    {
        SetClutRectangle(_clutRectangle.X, (int)numericUpDownClutY.Value, _clutRectangle.Width);
        MainPictureBox.Refresh();
        UpdateModeWindow();
    }

    private void numericUpDownX_ValueChanged(object sender, EventArgs e)
    {
        SetMainRectangle((int)numericUpDownX.Value, _mainRectangle.Y, _mainRectangle.Width, _mainRectangle.Height);
        MainPictureBox.Refresh();
        UpdateModeWindow();
    }

    private void numericUpDownY_ValueChanged(object sender, EventArgs e)
    {
        SetMainRectangle(_mainRectangle.X, (int)numericUpDownY.Value, _mainRectangle.Width, _mainRectangle.Height);
        MainPictureBox.Refresh();
        UpdateModeWindow();
    }

    private void numericUpDownWidth_ValueChanged(object sender, EventArgs e)
    {
        SetMainRectangle(_mainRectangle.X, _mainRectangle.Y, (int)numericUpDownWidth.Value, _mainRectangle.Height);
        MainPictureBox.Refresh();
        UpdateModeWindow();
    }

    private void numericUpDownHeight_ValueChanged(object sender, EventArgs e)
    {
        SetMainRectangle(_mainRectangle.X, _mainRectangle.Y, _mainRectangle.Width, (int)numericUpDownHeight.Value);
        MainPictureBox.Refresh();
        UpdateModeWindow();
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
        _fileHelper.SaveModeImage(_modeForm.ModeImage);
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
        if (e.Button == MouseButtons.Middle && _zoomFactor != 1)
        {
            _panning = true;
            Cursor.Current = Cursors.NoMove2D;
            _panningStartPoint.X = e.X;
            _panningStartPoint.Y = e.Y;
        }
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

    private enum Modes
    {
        Mode16Bpp,
        Mode8Bpp,
        Mode4Bpp,
        Mode24Bpp
    }
}