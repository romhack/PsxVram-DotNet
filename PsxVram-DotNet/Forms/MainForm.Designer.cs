namespace PsxVram_DotNet.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainPictureBox = new System.Windows.Forms.PictureBox();
            this.checkBoxTransparency = new System.Windows.Forms.CheckBox();
            this.groupBoxMode = new System.Windows.Forms.GroupBox();
            this.radioButton4bpp = new System.Windows.Forms.RadioButton();
            this.radioButton8bpp = new System.Windows.Forms.RadioButton();
            this.radioButton16bpp = new System.Windows.Forms.RadioButton();
            this.radioButton24bpp = new System.Windows.Forms.RadioButton();
            this.labelRectangleHeight = new System.Windows.Forms.Label();
            this.numericUpDownHeight = new System.Windows.Forms.NumericUpDown();
            this.labelRectangleX = new System.Windows.Forms.Label();
            this.numericUpDownX = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownY = new System.Windows.Forms.NumericUpDown();
            this.labelRectangleWidth = new System.Windows.Forms.Label();
            this.labelRectangleY = new System.Windows.Forms.Label();
            this.numericUpDownWidth = new System.Windows.Forms.NumericUpDown();
            this.comboBoxDefaultRectangleSize = new System.Windows.Forms.ComboBox();
            this.groupBoxPalette = new System.Windows.Forms.GroupBox();
            this.buttonSelectTransparentColor = new System.Windows.Forms.Button();
            this.checkBoxGrayscaleInverted = new System.Windows.Forms.CheckBox();
            this.radioButtonClut = new System.Windows.Forms.RadioButton();
            this.radioButtonGrayscale = new System.Windows.Forms.RadioButton();
            this.labelClutX = new System.Windows.Forms.Label();
            this.numericUpDownClutY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownClutX = new System.Windows.Forms.NumericUpDown();
            this.labelClutY = new System.Windows.Forms.Label();
            this.groupBoxRectangle = new System.Windows.Forms.GroupBox();
            this.radioButtonCustomRectangle = new System.Windows.Forms.RadioButton();
            this.radioButtonDefaultRectangle = new System.Windows.Forms.RadioButton();
            this.colorDialogTransparent = new System.Windows.Forms.ColorDialog();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.openFileButton = new System.Windows.Forms.ToolStripButton();
            this.refreshButton = new System.Windows.Forms.ToolStripButton();
            this.aboutButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveImageButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownSaveBinaryButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.firstScanlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rectangleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabelCursor = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelRectangle = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelClut = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.zoomTrackBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MainPictureBox)).BeginInit();
            this.groupBoxMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).BeginInit();
            this.groupBoxPalette.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClutY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClutX)).BeginInit();
            this.groupBoxRectangle.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPictureBox
            // 
            this.MainPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.MainPictureBox.Location = new System.Drawing.Point(0, 0);
            this.MainPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.MainPictureBox.Name = "MainPictureBox";
            this.MainPictureBox.Size = new System.Drawing.Size(1024, 512);
            this.MainPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.MainPictureBox.TabIndex = 0;
            this.MainPictureBox.TabStop = false;
            this.MainPictureBox.Visible = false;
            this.MainPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox1_Paint);
            this.MainPictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mainPictureBox_mouseClick);
            this.MainPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainPictureBox_MouseDown);
            this.MainPictureBox.MouseLeave += new System.EventHandler(this.MainPictureBox_MouseLeave);
            this.MainPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainPictureBox_MouseMove);
            this.MainPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainPictureBox_MouseUp);
            // 
            // checkBoxTransparency
            // 
            this.checkBoxTransparency.AutoSize = true;
            this.checkBoxTransparency.Location = new System.Drawing.Point(167, 24);
            this.checkBoxTransparency.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxTransparency.Name = "checkBoxTransparency";
            this.checkBoxTransparency.Size = new System.Drawing.Size(95, 19);
            this.checkBoxTransparency.TabIndex = 2;
            this.checkBoxTransparency.Text = "Transparency";
            this.checkBoxTransparency.UseVisualStyleBackColor = true;
            this.checkBoxTransparency.CheckedChanged += new System.EventHandler(this.CheckBoxTransparency_CheckedChanged);
            // 
            // groupBoxMode
            // 
            this.groupBoxMode.Controls.Add(this.radioButton4bpp);
            this.groupBoxMode.Controls.Add(this.radioButton8bpp);
            this.groupBoxMode.Controls.Add(this.radioButton16bpp);
            this.groupBoxMode.Controls.Add(this.radioButton24bpp);
            this.groupBoxMode.Enabled = false;
            this.groupBoxMode.Location = new System.Drawing.Point(8, 31);
            this.groupBoxMode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxMode.Name = "groupBoxMode";
            this.groupBoxMode.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxMode.Size = new System.Drawing.Size(304, 57);
            this.groupBoxMode.TabIndex = 4;
            this.groupBoxMode.TabStop = false;
            this.groupBoxMode.Text = "Mode";
            // 
            // radioButton4bpp
            // 
            this.radioButton4bpp.AutoSize = true;
            this.radioButton4bpp.Location = new System.Drawing.Point(224, 20);
            this.radioButton4bpp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButton4bpp.Name = "radioButton4bpp";
            this.radioButton4bpp.Size = new System.Drawing.Size(55, 19);
            this.radioButton4bpp.TabIndex = 3;
            this.radioButton4bpp.Tag = "4";
            this.radioButton4bpp.Text = "4 bpp";
            this.radioButton4bpp.UseVisualStyleBackColor = true;
            this.radioButton4bpp.CheckedChanged += new System.EventHandler(this.radioButton4bpp_CheckedChanged);
            // 
            // radioButton8bpp
            // 
            this.radioButton8bpp.AutoSize = true;
            this.radioButton8bpp.Location = new System.Drawing.Point(161, 21);
            this.radioButton8bpp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButton8bpp.Name = "radioButton8bpp";
            this.radioButton8bpp.Size = new System.Drawing.Size(55, 19);
            this.radioButton8bpp.TabIndex = 2;
            this.radioButton8bpp.Tag = "8";
            this.radioButton8bpp.Text = "8 bpp";
            this.radioButton8bpp.UseVisualStyleBackColor = true;
            this.radioButton8bpp.CheckedChanged += new System.EventHandler(this.radioButton8bpp_CheckedChanged);
            // 
            // radioButton16bpp
            // 
            this.radioButton16bpp.AutoSize = true;
            this.radioButton16bpp.Checked = true;
            this.radioButton16bpp.Location = new System.Drawing.Point(92, 20);
            this.radioButton16bpp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButton16bpp.Name = "radioButton16bpp";
            this.radioButton16bpp.Size = new System.Drawing.Size(61, 19);
            this.radioButton16bpp.TabIndex = 1;
            this.radioButton16bpp.TabStop = true;
            this.radioButton16bpp.Tag = "16";
            this.radioButton16bpp.Text = "16 bpp";
            this.radioButton16bpp.UseVisualStyleBackColor = true;
            this.radioButton16bpp.CheckedChanged += new System.EventHandler(this.radioButton16bpp_CheckedChanged);
            // 
            // radioButton24bpp
            // 
            this.radioButton24bpp.AutoSize = true;
            this.radioButton24bpp.Location = new System.Drawing.Point(23, 20);
            this.radioButton24bpp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButton24bpp.Name = "radioButton24bpp";
            this.radioButton24bpp.Size = new System.Drawing.Size(61, 19);
            this.radioButton24bpp.TabIndex = 0;
            this.radioButton24bpp.Tag = "24";
            this.radioButton24bpp.Text = "24 bpp";
            this.radioButton24bpp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.radioButton24bpp.UseVisualStyleBackColor = true;
            this.radioButton24bpp.CheckedChanged += new System.EventHandler(this.radioButton24bpp_CheckedChanged);
            // 
            // labelRectangleHeight
            // 
            this.labelRectangleHeight.AutoSize = true;
            this.labelRectangleHeight.Location = new System.Drawing.Point(301, 51);
            this.labelRectangleHeight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRectangleHeight.Name = "labelRectangleHeight";
            this.labelRectangleHeight.Size = new System.Drawing.Size(43, 15);
            this.labelRectangleHeight.TabIndex = 7;
            this.labelRectangleHeight.Text = "Height";
            this.labelRectangleHeight.Visible = false;
            // 
            // numericUpDownHeight
            // 
            this.numericUpDownHeight.InterceptArrowKeys = false;
            this.numericUpDownHeight.Location = new System.Drawing.Point(352, 48);
            this.numericUpDownHeight.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.numericUpDownHeight.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.numericUpDownHeight.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDownHeight.Name = "numericUpDownHeight";
            this.numericUpDownHeight.Size = new System.Drawing.Size(52, 23);
            this.numericUpDownHeight.TabIndex = 6;
            this.numericUpDownHeight.Tag = "";
            this.numericUpDownHeight.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDownHeight.Visible = false;
            this.numericUpDownHeight.ValueChanged += new System.EventHandler(this.numericUpDownHeight_ValueChanged);
            // 
            // labelRectangleX
            // 
            this.labelRectangleX.AutoSize = true;
            this.labelRectangleX.Location = new System.Drawing.Point(10, 51);
            this.labelRectangleX.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRectangleX.Name = "labelRectangleX";
            this.labelRectangleX.Size = new System.Drawing.Size(14, 15);
            this.labelRectangleX.TabIndex = 1;
            this.labelRectangleX.Text = "X";
            this.labelRectangleX.Visible = false;
            // 
            // numericUpDownX
            // 
            this.numericUpDownX.InterceptArrowKeys = false;
            this.numericUpDownX.Location = new System.Drawing.Point(34, 48);
            this.numericUpDownX.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.numericUpDownX.Maximum = new decimal(new int[] {
            1023,
            0,
            0,
            0});
            this.numericUpDownX.Name = "numericUpDownX";
            this.numericUpDownX.Size = new System.Drawing.Size(52, 23);
            this.numericUpDownX.TabIndex = 0;
            this.numericUpDownX.Tag = "";
            this.numericUpDownX.Visible = false;
            this.numericUpDownX.ValueChanged += new System.EventHandler(this.numericUpDownX_ValueChanged);
            // 
            // numericUpDownY
            // 
            this.numericUpDownY.InterceptArrowKeys = false;
            this.numericUpDownY.Location = new System.Drawing.Point(119, 48);
            this.numericUpDownY.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.numericUpDownY.Maximum = new decimal(new int[] {
            511,
            0,
            0,
            0});
            this.numericUpDownY.Name = "numericUpDownY";
            this.numericUpDownY.Size = new System.Drawing.Size(52, 23);
            this.numericUpDownY.TabIndex = 2;
            this.numericUpDownY.Tag = "";
            this.numericUpDownY.Visible = false;
            this.numericUpDownY.ValueChanged += new System.EventHandler(this.numericUpDownY_ValueChanged);
            // 
            // labelRectangleWidth
            // 
            this.labelRectangleWidth.AutoSize = true;
            this.labelRectangleWidth.Location = new System.Drawing.Point(194, 51);
            this.labelRectangleWidth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRectangleWidth.Name = "labelRectangleWidth";
            this.labelRectangleWidth.Size = new System.Drawing.Size(39, 15);
            this.labelRectangleWidth.TabIndex = 5;
            this.labelRectangleWidth.Text = "Width";
            this.labelRectangleWidth.Visible = false;
            // 
            // labelRectangleY
            // 
            this.labelRectangleY.AutoSize = true;
            this.labelRectangleY.Location = new System.Drawing.Point(96, 51);
            this.labelRectangleY.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRectangleY.Name = "labelRectangleY";
            this.labelRectangleY.Size = new System.Drawing.Size(14, 15);
            this.labelRectangleY.TabIndex = 3;
            this.labelRectangleY.Text = "Y";
            this.labelRectangleY.Visible = false;
            // 
            // numericUpDownWidth
            // 
            this.numericUpDownWidth.InterceptArrowKeys = false;
            this.numericUpDownWidth.Location = new System.Drawing.Point(241, 48);
            this.numericUpDownWidth.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.numericUpDownWidth.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numericUpDownWidth.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDownWidth.Name = "numericUpDownWidth";
            this.numericUpDownWidth.Size = new System.Drawing.Size(52, 23);
            this.numericUpDownWidth.TabIndex = 4;
            this.numericUpDownWidth.Tag = "";
            this.numericUpDownWidth.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDownWidth.Visible = false;
            this.numericUpDownWidth.ValueChanged += new System.EventHandler(this.numericUpDownWidth_ValueChanged);
            // 
            // comboBoxDefaultRectangleSize
            // 
            this.comboBoxDefaultRectangleSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDefaultRectangleSize.FormattingEnabled = true;
            this.comboBoxDefaultRectangleSize.Items.AddRange(new object[] {
            "256x240",
            "320x240",
            "368x240",
            "512x240",
            "640x240",
            "256x480",
            "320x480",
            "368x480",
            "512x480",
            "640x480"});
            this.comboBoxDefaultRectangleSize.Location = new System.Drawing.Point(163, 20);
            this.comboBoxDefaultRectangleSize.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxDefaultRectangleSize.MaxDropDownItems = 10;
            this.comboBoxDefaultRectangleSize.Name = "comboBoxDefaultRectangleSize";
            this.comboBoxDefaultRectangleSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.comboBoxDefaultRectangleSize.Size = new System.Drawing.Size(83, 23);
            this.comboBoxDefaultRectangleSize.TabIndex = 0;
            this.comboBoxDefaultRectangleSize.SelectedIndexChanged += new System.EventHandler(this.comboBoxDefaultRectangleSize_SelectedIndexChanged);
            // 
            // groupBoxPalette
            // 
            this.groupBoxPalette.Controls.Add(this.buttonSelectTransparentColor);
            this.groupBoxPalette.Controls.Add(this.checkBoxGrayscaleInverted);
            this.groupBoxPalette.Controls.Add(this.checkBoxTransparency);
            this.groupBoxPalette.Controls.Add(this.radioButtonClut);
            this.groupBoxPalette.Controls.Add(this.radioButtonGrayscale);
            this.groupBoxPalette.Controls.Add(this.labelClutX);
            this.groupBoxPalette.Controls.Add(this.numericUpDownClutY);
            this.groupBoxPalette.Controls.Add(this.numericUpDownClutX);
            this.groupBoxPalette.Controls.Add(this.labelClutY);
            this.groupBoxPalette.Enabled = false;
            this.groupBoxPalette.Location = new System.Drawing.Point(746, 31);
            this.groupBoxPalette.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxPalette.Name = "groupBoxPalette";
            this.groupBoxPalette.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxPalette.Size = new System.Drawing.Size(286, 81);
            this.groupBoxPalette.TabIndex = 11;
            this.groupBoxPalette.TabStop = false;
            this.groupBoxPalette.Text = "Color palette";
            // 
            // buttonSelectTransparentColor
            // 
            this.buttonSelectTransparentColor.Location = new System.Drawing.Point(267, 22);
            this.buttonSelectTransparentColor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonSelectTransparentColor.Name = "buttonSelectTransparentColor";
            this.buttonSelectTransparentColor.Size = new System.Drawing.Size(134, 27);
            this.buttonSelectTransparentColor.TabIndex = 12;
            this.buttonSelectTransparentColor.Text = "Transparent color";
            this.buttonSelectTransparentColor.UseVisualStyleBackColor = true;
            this.buttonSelectTransparentColor.Click += new System.EventHandler(this.buttonSelectTransparentColor_Click);
            // 
            // checkBoxGrayscaleInverted
            // 
            this.checkBoxGrayscaleInverted.AutoSize = true;
            this.checkBoxGrayscaleInverted.Location = new System.Drawing.Point(167, 48);
            this.checkBoxGrayscaleInverted.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxGrayscaleInverted.MinimumSize = new System.Drawing.Size(105, 0);
            this.checkBoxGrayscaleInverted.Name = "checkBoxGrayscaleInverted";
            this.checkBoxGrayscaleInverted.Size = new System.Drawing.Size(105, 19);
            this.checkBoxGrayscaleInverted.TabIndex = 8;
            this.checkBoxGrayscaleInverted.Text = "Inverted";
            this.checkBoxGrayscaleInverted.UseVisualStyleBackColor = true;
            this.checkBoxGrayscaleInverted.Visible = false;
            this.checkBoxGrayscaleInverted.CheckedChanged += new System.EventHandler(this.CheckBoxGrayscaleInverted_CheckedChanged);
            // 
            // radioButtonClut
            // 
            this.radioButtonClut.AutoSize = true;
            this.radioButtonClut.Location = new System.Drawing.Point(98, 23);
            this.radioButtonClut.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButtonClut.Name = "radioButtonClut";
            this.radioButtonClut.Size = new System.Drawing.Size(53, 19);
            this.radioButtonClut.TabIndex = 1;
            this.radioButtonClut.Text = "CLUT";
            this.radioButtonClut.UseVisualStyleBackColor = true;
            this.radioButtonClut.Visible = false;
            this.radioButtonClut.CheckedChanged += new System.EventHandler(this.radioButtonClut_CheckedChanged);
            // 
            // radioButtonGrayscale
            // 
            this.radioButtonGrayscale.AutoSize = true;
            this.radioButtonGrayscale.Checked = true;
            this.radioButtonGrayscale.Location = new System.Drawing.Point(7, 22);
            this.radioButtonGrayscale.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButtonGrayscale.Name = "radioButtonGrayscale";
            this.radioButtonGrayscale.Size = new System.Drawing.Size(75, 19);
            this.radioButtonGrayscale.TabIndex = 0;
            this.radioButtonGrayscale.TabStop = true;
            this.radioButtonGrayscale.Text = "Grayscale";
            this.radioButtonGrayscale.UseVisualStyleBackColor = true;
            this.radioButtonGrayscale.Visible = false;
            this.radioButtonGrayscale.CheckedChanged += new System.EventHandler(this.radioButtonGrayscale_CheckedChanged);
            // 
            // labelClutX
            // 
            this.labelClutX.AutoSize = true;
            this.labelClutX.Location = new System.Drawing.Point(6, 52);
            this.labelClutX.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelClutX.Name = "labelClutX";
            this.labelClutX.Size = new System.Drawing.Size(14, 15);
            this.labelClutX.TabIndex = 5;
            this.labelClutX.Text = "X";
            this.labelClutX.Visible = false;
            // 
            // numericUpDownClutY
            // 
            this.numericUpDownClutY.InterceptArrowKeys = false;
            this.numericUpDownClutY.Location = new System.Drawing.Point(107, 50);
            this.numericUpDownClutY.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.numericUpDownClutY.Maximum = new decimal(new int[] {
            511,
            0,
            0,
            0});
            this.numericUpDownClutY.Name = "numericUpDownClutY";
            this.numericUpDownClutY.Size = new System.Drawing.Size(52, 23);
            this.numericUpDownClutY.TabIndex = 6;
            this.numericUpDownClutY.Tag = "";
            this.numericUpDownClutY.Visible = false;
            this.numericUpDownClutY.ValueChanged += new System.EventHandler(this.numericUpDownClutY_ValueChanged);
            // 
            // numericUpDownClutX
            // 
            this.numericUpDownClutX.Increment = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numericUpDownClutX.InterceptArrowKeys = false;
            this.numericUpDownClutX.Location = new System.Drawing.Point(29, 50);
            this.numericUpDownClutX.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.numericUpDownClutX.Maximum = new decimal(new int[] {
            1023,
            0,
            0,
            0});
            this.numericUpDownClutX.Name = "numericUpDownClutX";
            this.numericUpDownClutX.Size = new System.Drawing.Size(52, 23);
            this.numericUpDownClutX.TabIndex = 4;
            this.numericUpDownClutX.Tag = "";
            this.numericUpDownClutX.Visible = false;
            this.numericUpDownClutX.ValueChanged += new System.EventHandler(this.numericUpDownClutX_ValueChanged);
            // 
            // labelClutY
            // 
            this.labelClutY.AutoSize = true;
            this.labelClutY.Location = new System.Drawing.Point(84, 52);
            this.labelClutY.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelClutY.Name = "labelClutY";
            this.labelClutY.Size = new System.Drawing.Size(14, 15);
            this.labelClutY.TabIndex = 7;
            this.labelClutY.Text = "Y";
            this.labelClutY.Visible = false;
            // 
            // groupBoxRectangle
            // 
            this.groupBoxRectangle.Controls.Add(this.labelRectangleHeight);
            this.groupBoxRectangle.Controls.Add(this.comboBoxDefaultRectangleSize);
            this.groupBoxRectangle.Controls.Add(this.radioButtonCustomRectangle);
            this.groupBoxRectangle.Controls.Add(this.numericUpDownHeight);
            this.groupBoxRectangle.Controls.Add(this.numericUpDownWidth);
            this.groupBoxRectangle.Controls.Add(this.radioButtonDefaultRectangle);
            this.groupBoxRectangle.Controls.Add(this.labelRectangleX);
            this.groupBoxRectangle.Controls.Add(this.numericUpDownY);
            this.groupBoxRectangle.Controls.Add(this.labelRectangleY);
            this.groupBoxRectangle.Controls.Add(this.labelRectangleWidth);
            this.groupBoxRectangle.Controls.Add(this.numericUpDownX);
            this.groupBoxRectangle.Enabled = false;
            this.groupBoxRectangle.Location = new System.Drawing.Point(320, 31);
            this.groupBoxRectangle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxRectangle.Name = "groupBoxRectangle";
            this.groupBoxRectangle.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxRectangle.Size = new System.Drawing.Size(419, 81);
            this.groupBoxRectangle.TabIndex = 10;
            this.groupBoxRectangle.TabStop = false;
            this.groupBoxRectangle.Text = "Rectangle";
            // 
            // radioButtonCustomRectangle
            // 
            this.radioButtonCustomRectangle.AutoSize = true;
            this.radioButtonCustomRectangle.Location = new System.Drawing.Point(86, 22);
            this.radioButtonCustomRectangle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButtonCustomRectangle.Name = "radioButtonCustomRectangle";
            this.radioButtonCustomRectangle.Size = new System.Drawing.Size(67, 19);
            this.radioButtonCustomRectangle.TabIndex = 1;
            this.radioButtonCustomRectangle.Text = "Custom";
            this.radioButtonCustomRectangle.UseVisualStyleBackColor = true;
            this.radioButtonCustomRectangle.CheckedChanged += new System.EventHandler(this.radioButtonCustomRectangle_CheckedChanged);
            // 
            // radioButtonDefaultRectangle
            // 
            this.radioButtonDefaultRectangle.AutoSize = true;
            this.radioButtonDefaultRectangle.Checked = true;
            this.radioButtonDefaultRectangle.Location = new System.Drawing.Point(10, 22);
            this.radioButtonDefaultRectangle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButtonDefaultRectangle.Name = "radioButtonDefaultRectangle";
            this.radioButtonDefaultRectangle.Size = new System.Drawing.Size(63, 19);
            this.radioButtonDefaultRectangle.TabIndex = 0;
            this.radioButtonDefaultRectangle.TabStop = true;
            this.radioButtonDefaultRectangle.Text = "Default";
            this.radioButtonDefaultRectangle.UseVisualStyleBackColor = true;
            this.radioButtonDefaultRectangle.CheckedChanged += new System.EventHandler(this.radioButtonDefaultRectangle_CheckedChanged);
            // 
            // colorDialogTransparent
            // 
            this.colorDialogTransparent.AnyColor = true;
            this.colorDialogTransparent.Color = System.Drawing.Color.Magenta;
            this.colorDialogTransparent.SolidColorOnly = true;
            // 
            // toolStripMain
            // 
            this.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileButton,
            this.refreshButton,
            this.aboutButton,
            this.toolStripSeparator1,
            this.saveImageButton,
            this.toolStripDropDownSaveBinaryButton});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(1049, 25);
            this.toolStripMain.TabIndex = 12;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // openFileButton
            // 
            this.openFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openFileButton.Image = ((System.Drawing.Image)(resources.GetObject("openFileButton.Image")));
            this.openFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(23, 22);
            this.openFileButton.Text = "Open file";
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshButton.Enabled = false;
            this.refreshButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshButton.Image")));
            this.refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(23, 22);
            this.refreshButton.Text = "Refresh file";
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // aboutButton
            // 
            this.aboutButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.aboutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.aboutButton.Image = ((System.Drawing.Image)(resources.GetObject("aboutButton.Image")));
            this.aboutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.aboutButton.Name = "aboutButton";
            this.aboutButton.Size = new System.Drawing.Size(23, 22);
            this.aboutButton.Text = "About";
            this.aboutButton.Click += new System.EventHandler(this.aboutButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // saveImageButton
            // 
            this.saveImageButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveImageButton.Enabled = false;
            this.saveImageButton.Image = ((System.Drawing.Image)(resources.GetObject("saveImageButton.Image")));
            this.saveImageButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveImageButton.Name = "saveImageButton";
            this.saveImageButton.Size = new System.Drawing.Size(23, 22);
            this.saveImageButton.Text = "Save mode image";
            this.saveImageButton.Click += new System.EventHandler(this.saveImageButton_Click);
            // 
            // toolStripDropDownSaveBinaryButton
            // 
            this.toolStripDropDownSaveBinaryButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownSaveBinaryButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.firstScanlineToolStripMenuItem,
            this.rectangleToolStripMenuItem,
            this.clutToolStripMenuItem});
            this.toolStripDropDownSaveBinaryButton.Enabled = false;
            this.toolStripDropDownSaveBinaryButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownSaveBinaryButton.Image")));
            this.toolStripDropDownSaveBinaryButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownSaveBinaryButton.Name = "toolStripDropDownSaveBinaryButton";
            this.toolStripDropDownSaveBinaryButton.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownSaveBinaryButton.Text = "Save binary dump";
            // 
            // firstScanlineToolStripMenuItem
            // 
            this.firstScanlineToolStripMenuItem.Name = "firstScanlineToolStripMenuItem";
            this.firstScanlineToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.firstScanlineToolStripMenuItem.Text = "First scanline";
            this.firstScanlineToolStripMenuItem.Click += new System.EventHandler(this.firstScanlineToolStripMenuItem_Click);
            // 
            // rectangleToolStripMenuItem
            // 
            this.rectangleToolStripMenuItem.Name = "rectangleToolStripMenuItem";
            this.rectangleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.rectangleToolStripMenuItem.Text = "Rectangle";
            this.rectangleToolStripMenuItem.Click += new System.EventHandler(this.rectangleToolStripMenuItem_Click);
            // 
            // clutToolStripMenuItem
            // 
            this.clutToolStripMenuItem.Enabled = false;
            this.clutToolStripMenuItem.Name = "clutToolStripMenuItem";
            this.clutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.clutToolStripMenuItem.Text = "CLUT";
            this.clutToolStripMenuItem.Click += new System.EventHandler(this.clutToolStripMenuItem_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.RestoreDirectory = true;
            this.openFileDialog.Title = "Select binary VRAM dump file";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabelCursor,
            this.statusLabelRectangle,
            this.statusLabelClut});
            this.statusStrip.Location = new System.Drawing.Point(0, 641);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1049, 23);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 14;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusLabelCursor
            // 
            this.statusLabelCursor.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusLabelCursor.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.statusLabelCursor.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.statusLabelCursor.Name = "statusLabelCursor";
            this.statusLabelCursor.Size = new System.Drawing.Size(249, 18);
            this.statusLabelCursor.Text = "Cursor X:0000 Y:000 Offset:0x00000";
            this.statusLabelCursor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusLabelRectangle
            // 
            this.statusLabelRectangle.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusLabelRectangle.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.statusLabelRectangle.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.statusLabelRectangle.Name = "statusLabelRectangle";
            this.statusLabelRectangle.Size = new System.Drawing.Size(361, 18);
            this.statusLabelRectangle.Text = "Rectangle W:0008 H:008 X:0000 Y:000 Offset:0x00000";
            // 
            // statusLabelClut
            // 
            this.statusLabelClut.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusLabelClut.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.statusLabelClut.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.statusLabelClut.Name = "statusLabelClut";
            this.statusLabelClut.Size = new System.Drawing.Size(235, 18);
            this.statusLabelClut.Text = "CLUT X:0000 Y:000 Offset:0x00000";
            this.statusLabelClut.Visible = false;
            // 
            // MainPanel
            // 
            this.MainPanel.AutoScroll = true;
            this.MainPanel.Controls.Add(this.MainPictureBox);
            this.MainPanel.Location = new System.Drawing.Point(12, 121);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1024, 512);
            this.MainPanel.TabIndex = 15;
            // 
            // zoomTrackBar
            // 
            this.zoomTrackBar.AutoSize = false;
            this.zoomTrackBar.Enabled = false;
            this.zoomTrackBar.LargeChange = 1;
            this.zoomTrackBar.Location = new System.Drawing.Point(53, 92);
            this.zoomTrackBar.Maximum = 4;
            this.zoomTrackBar.Minimum = 1;
            this.zoomTrackBar.Name = "zoomTrackBar";
            this.zoomTrackBar.Size = new System.Drawing.Size(259, 20);
            this.zoomTrackBar.TabIndex = 16;
            this.zoomTrackBar.Value = 1;
            this.zoomTrackBar.ValueChanged += new System.EventHandler(this.zoomTrackBar_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 17;
            this.label1.Text = "Zoom";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1049, 664);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.zoomTrackBar);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.groupBoxRectangle);
            this.Controls.Add(this.groupBoxPalette);
            this.Controls.Add(this.groupBoxMode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "PsxVram-DotNet";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.MainPictureBox)).EndInit();
            this.groupBoxMode.ResumeLayout(false);
            this.groupBoxMode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).EndInit();
            this.groupBoxPalette.ResumeLayout(false);
            this.groupBoxPalette.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClutY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClutX)).EndInit();
            this.groupBoxRectangle.ResumeLayout(false);
            this.groupBoxRectangle.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox MainPictureBox;
        private System.Windows.Forms.CheckBox checkBoxTransparency;
        private System.Windows.Forms.GroupBox groupBoxMode;
        private System.Windows.Forms.RadioButton radioButton24bpp;
        private System.Windows.Forms.RadioButton radioButton8bpp;
        private System.Windows.Forms.RadioButton radioButton16bpp;
        private System.Windows.Forms.RadioButton radioButton4bpp;
        private System.Windows.Forms.Label labelRectangleX;
        private System.Windows.Forms.NumericUpDown numericUpDownX;
        private System.Windows.Forms.Label labelRectangleHeight;
        private System.Windows.Forms.NumericUpDown numericUpDownHeight;
        private System.Windows.Forms.Label labelRectangleWidth;
        private System.Windows.Forms.NumericUpDown numericUpDownWidth;
        private System.Windows.Forms.Label labelRectangleY;
        private System.Windows.Forms.NumericUpDown numericUpDownY;
        private System.Windows.Forms.ComboBox comboBoxDefaultRectangleSize;
        private System.Windows.Forms.GroupBox groupBoxPalette;
        private System.Windows.Forms.CheckBox checkBoxGrayscaleInverted;
        private System.Windows.Forms.RadioButton radioButtonClut;
        private System.Windows.Forms.RadioButton radioButtonGrayscale;
        private System.Windows.Forms.Label labelClutX;
        private System.Windows.Forms.NumericUpDown numericUpDownClutY;
        private System.Windows.Forms.NumericUpDown numericUpDownClutX;
        private System.Windows.Forms.Label labelClutY;
        private System.Windows.Forms.GroupBox groupBoxRectangle;
        private System.Windows.Forms.RadioButton radioButtonCustomRectangle;
        private System.Windows.Forms.RadioButton radioButtonDefaultRectangle;
        private System.Windows.Forms.ColorDialog colorDialogTransparent;
        private System.Windows.Forms.Button buttonSelectTransparentColor;
        private ToolStrip toolStripMain;
        private ToolStripButton openFileButton;
        private ToolStripButton refreshButton;
        private ToolStripButton saveImageButton;
        private ToolStripButton aboutButton;
        private OpenFileDialog openFileDialog;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabelClut;
        private ToolStripStatusLabel statusLabelCursor;
        private ToolStripStatusLabel statusLabelRectangle;
        private Panel MainPanel;
        private TrackBar zoomTrackBar;
        private Label label1;
        private ToolStripDropDownButton toolStripDropDownSaveBinaryButton;
        private ToolStripMenuItem firstScanlineToolStripMenuItem;
        private ToolStripMenuItem rectangleToolStripMenuItem;
        private ToolStripMenuItem clutToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
    }
}

