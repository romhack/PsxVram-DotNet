namespace PsxVram_DotNet.Forms;

public partial class ModeForm : Form
{
    public ModeForm()
    {
        InitializeComponent();
    }

    public Image ModeImage => ModePictureBox.Image;

    public void SetModeFormPictureBox(Bitmap modeBitmap)
    {
        var oldImage = ModePictureBox.Image;
        ModePictureBox.Image = modeBitmap;
        ModePictureBox.Width = modeBitmap.Width;
        ModePictureBox.Height = modeBitmap.Height;
        oldImage?.Dispose();
    }

    public void SetModeFormPictureBoxBackColor(Color newBackColor)
    {
        ModePictureBox.BackColor = newBackColor;
    }

    public void SetModeFormCaption()
    {
        Text = $@"Mode window {ModePictureBox.Width}x{ModePictureBox.Height}";
    }
}