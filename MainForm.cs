namespace PsxVram_DotNet
{
    using PsxVram_DotNet.Modes;

    public partial class MainForm : Form
    {
        private enum Modes
        {
            Mode16bpp,
            Mode8bpp,
            Mode4bpp,
            Mode24bpp
        }

        private Mode16bpp? _mode16bpp;
        private Mode8bpp? _mode8bpp;
        private Mode4bpp? _mode4bpp;
        private Mode24bpp? _mode24bpp;
        private Modes _currentMode = Modes.Mode16bpp;
        private Rectangle _mainRectangle = new();
        private bool _clutMode = false;
        private Rectangle _clutRectangle = new(0, 0, 0x10, 1);
        private readonly Pen _mainBorderPen = new(Color.Cyan, 1);
        private readonly Brush _mainFillBrush = new SolidBrush(Color.FromArgb(0x80, 0, 0xFF, 0xFF));
        private readonly Pen _clutPen = new(Color.Magenta, 1);
        public const int MaxWidth = 1024;
        public const int MaxHeight = 512;
        private readonly Size _default8BppRectangleSize = new(0x80, 0x100);//Default 256x256 rect on 16bpp main form
        private readonly Size _default4BppRectangleSize = new(0x40, 0x100);
        private const string DefaultFileName = "vram.bin";
        private string _currentFileName = DefaultFileName;
        private const string DefaultCursorString = "Cursor X:0000 Y:000 Offset:0x00000";

        private readonly ModeForm _modeForm = new();

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

        private string GetCurrentFileName()
        {
            if (File.Exists(DefaultFileName))
            {
                return DefaultFileName;
            }
            string[] arguments = Environment.GetCommandLineArgs();
            if (arguments.Length == 2)
            {
                return arguments[1];
            }
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }
            throw new InvalidOperationException("Input file was not specified");
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void ShowCurrentFile()
        {
            if (File.Exists(_currentFileName) == false)
            {
                throw new FileNotFoundException("Initially specified input file not found");
            }
            byte[] sourceBytes = File.ReadAllBytes(_currentFileName);
            if (sourceBytes.Length != 0x100000)
            {
                throw new FileFormatException("Specified input file is not 1MB in size");
            }
            _mode16bpp = new Mode16bpp(sourceBytes);
            _mode8bpp = new Mode8bpp(sourceBytes);
            _mode4bpp = new Mode4bpp(sourceBytes);
            _mode24bpp = new Mode24bpp(sourceBytes);
            MainPictureBox.Image = _mode16bpp.ModeBitmap;

            UpdateModeWindow();
            if (Application.OpenForms.OfType<ModeForm>().Any() == false)
            {
                _modeForm.Show(this);
            }
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(_mainBorderPen, _mainRectangle);
            e.Graphics.FillRectangle(_mainFillBrush, _mainRectangle);
            if (_clutMode)
            {
                e.Graphics.DrawLine(_clutPen, 0, 0, _clutRectangle.X, _clutRectangle.Y);
                e.Graphics.DrawRectangle(_clutPen, _clutRectangle.X, _clutRectangle.Y - 1, _clutRectangle.Width, 2);
            }
        }

        private Mode? GetCurrentMode()
        {
            Dictionary<Modes, Mode?> modeTagDictionary = new()
            {
                { Modes.Mode16bpp, _mode16bpp },
                { Modes.Mode8bpp, _mode8bpp },
                { Modes.Mode4bpp, _mode4bpp },
                { Modes.Mode24bpp, _mode24bpp }
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
                case Mode24bpp mode24:
                    updatedBitmap = mode24.GetTrimmedBitmap(_mainRectangle);
                    break;

                case Mode16bpp mode16:
                    updatedBitmap = mode16.GetTrimmedBitmap(_mainRectangle, checkBoxTransparency.Checked);
                    break;

                case Mode8bpp mode8bpp:
                    if (radioButtonClut.Checked)
                    {
                        var updatedClutColors = _mode16bpp?.GetClutColors(_clutRectangle);
                        updatedBitmap = mode8bpp.GetTrimmedBitmap(_mainRectangle, updatedClutColors);
                    }
                    else
                    {
                        updatedBitmap = mode8bpp.GetTrimmedBitmap(_mainRectangle, inverted: checkBoxGrayscaleInverted.Checked);
                    }
                    break;

                case Mode4bpp mode4bpp:
                    if (radioButtonClut.Checked)
                    {
                        var updatedClutColors = _mode16bpp?.GetClutColors(_clutRectangle);
                        updatedBitmap = mode4bpp.GetTrimmedBitmap(_mainRectangle, updatedClutColors);
                    }
                    else
                    {
                        updatedBitmap = mode4bpp.GetTrimmedBitmap(_mainRectangle, inverted: checkBoxGrayscaleInverted.Checked);
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
            int remainder = number % interval;
            return number - remainder;
        }

        private void SetMainRectangle(int x, int y, int width, int height)
        {
            _mainRectangle.Width = width;
            _mainRectangle.Height = height;
            _mainRectangle.X = x + width > MaxWidth ? MaxWidth - width : x;
            _mainRectangle.Y = y + height > MaxHeight ? MaxHeight - height : y;

            var offset = (_mainRectangle.Y * MaxWidth + _mainRectangle.X) * 2;
            statusLabelRectangle.Text = $"Rectangle W:{width:D4} H:{height:D3} X:{_mainRectangle.X:D4} Y:{_mainRectangle.Y:D3} Offset:0x{offset:X5}";
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
            statusLabelClut.Text = $"CLUT X:{_clutRectangle.X:D4} Y:{y:D3} Offset:0x{offset:X5}";
            numericUpDownClutX.Value = _clutRectangle.X;
            numericUpDownClutY.Value = _clutRectangle.Y;
        }

        private void mainPictureBox_mouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left://Main rectangle
                    //Due to PSX hardware specifications in indexed modes:
                    var newMainX = RoundOff(e.X, 0x40);
                    var newMainY = RoundOff(e.Y, 0x100);
                    SetMainRectangle(newMainX, newMainY, _mainRectangle.Width, _mainRectangle.Height);
                    break;

                case MouseButtons.Right://CLUT line
                    if (_clutMode)
                    {
                        var x = RoundOff(e.X, 0x10);//Due to PSX specification
                        SetClutRectangle(x, e.Y, _clutRectangle.Width);
                    }
                    break;
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
            comboBoxDefaultRectangleSize.Visible = radioButtonDefaultRectangle.Checked & isIndexedMode == false;
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
            _currentMode = Modes.Mode24bpp;
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
            _currentMode = Modes.Mode16bpp;
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
            _currentMode = Modes.Mode8bpp;
            SetClutRectangle(_clutRectangle.X, _clutRectangle.Y, 0x100);
            if (radioButtonDefaultRectangle.Checked)
            {
                SetMainRectangle(_mainRectangle.X, _mainRectangle.Y, _default8BppRectangleSize.Width, _default8BppRectangleSize.Height);
            }
            MainPictureBox.Refresh();
            UpdateModeWindow();
        }

        private void radioButton4bpp_CheckedChanged(object sender, EventArgs e)
        {
            _clutMode = radioButtonClut.Checked;
            HandleVisibility();
            _currentMode = Modes.Mode4bpp;
            SetClutRectangle(_clutRectangle.X, _clutRectangle.Y, 0x10);
            if (radioButtonDefaultRectangle.Checked)
            {
                SetMainRectangle(_mainRectangle.X, _mainRectangle.Y, _default4BppRectangleSize.Width, _default4BppRectangleSize.Height);
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
                case Modes.Mode4bpp:
                    SetMainRectangle(_mainRectangle.X, _mainRectangle.Y, _default4BppRectangleSize.Width, _default4BppRectangleSize.Height);
                    break;

                case Modes.Mode8bpp:
                    SetMainRectangle(_mainRectangle.X, _mainRectangle.Y, _default8BppRectangleSize.Width, _default8BppRectangleSize.Height);
                    break;

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

        private void MainForm_Load(object sender, EventArgs e)
        {
            checkBoxGrayscaleInverted.Location = labelClutX.Location;
            checkBoxTransparency.Location = radioButtonGrayscale.Location;
            buttonSelectTransparentColor.Location = labelClutX.Location;
            comboBoxDefaultRectangleSize.Location = labelRectangleX.Location;
            comboBoxDefaultRectangleSize.SelectedIndex = 1;//320x240 default resolution
            statusLabelCursor.Text = DefaultCursorString;
            _modeForm.Left = this.Left + 1050;
            _modeForm.Top = this.Top;
            try
            {
                _currentFileName = GetCurrentFileName();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Input file error");
                Environment.Exit(1);
            }
            ShowCurrentFile();
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
                {(Keys.None, Keys.W), (true, numericUpDownY, -1) },
                {(Keys.None, Keys.S), (true, numericUpDownY, 1) },
                {(Keys.None, Keys.A), (true, numericUpDownX, -1) },
                {(Keys.None, Keys.D), (true, numericUpDownX, 1) },
                {(Keys.Shift, Keys.W), (radioButtonCustomRectangle.Checked, numericUpDownHeight, -1) },
                {(Keys.Shift, Keys.S), (radioButtonCustomRectangle.Checked, numericUpDownHeight, 1) },
                {(Keys.Shift, Keys.A), (radioButtonCustomRectangle.Checked, numericUpDownWidth, -1) },
                {(Keys.Shift, Keys.D), (radioButtonCustomRectangle.Checked, numericUpDownWidth, 1) },
                {(Keys.Control, Keys.W), (_clutMode, numericUpDownClutY, -1)},
                {(Keys.Control, Keys.S), (_clutMode, numericUpDownClutY, 1)},
                {(Keys.Control, Keys.A), (_clutMode, numericUpDownClutX, -numericUpDownClutX.Increment)},
                {(Keys.Control, Keys.D), (_clutMode, numericUpDownClutX, numericUpDownClutX.Increment)}
            };
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
            ActiveControl = groupBoxRectangle;//To suppress ding sound on active NumericUpDown change
            numericUpDown.Value = Math.Max(numericUpDown.Minimum, Math.Min(numericUpDown.Value + increment, numericUpDown.Maximum));
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            ShowCurrentFile();
        }

        private void saveImageButton_Click(object sender, EventArgs e)
        {
            _modeForm.SavePictureBoxImage(_currentFileName);
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _currentFileName = openFileDialog.FileName;
                ShowCurrentFile();
            }
        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new();
            aboutBox.ShowDialog();
        }

        private void MainPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            var offset = (e.Y * MaxWidth + e.X) * 2;
            statusLabelCursor.Text = $"Cursor X:{e.X:D4} Y:{e.Y:D3} Offset:0x{offset:X5}";
        }

        private void MainPictureBox_MouseLeave(object sender, EventArgs e)
        {
            statusLabelCursor.Text = DefaultCursorString;
        }
    }
}