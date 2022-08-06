namespace PsxVram_DotNet
{
    public partial class ModeForm : Form
    {
        public ModeForm()
        {
            InitializeComponent();
        }

        public void SetModeFormPictureBox(Bitmap modeBitmap)
        {
            var oldImage = ModePictureBox.Image;
            ModePictureBox.Image = modeBitmap;
            ModePictureBox.Width = modeBitmap.Width;
            ModePictureBox.Height = modeBitmap.Height;
            if (oldImage != null)
            {
                oldImage.Dispose();
            }
        }

        public void SetModeFormPictureBoxBackColor(Color newBackColor)
        {
            ModePictureBox.BackColor = newBackColor;
        }

        public void SavePictureBoxImage(string sourceFilePath)
        {
            var fileName = $"{Path.GetFileNameWithoutExtension(sourceFilePath)}_{DateTime.Now:yyyyMMdd_HH_mm_ss}.bmp";
            var fileDirectory = Path.GetDirectoryName(sourceFilePath) ?? "";
            var filePath = Path.Combine(fileDirectory, fileName);
            var image = ModePictureBox.Image;
            image.Save(filePath);
        }

        public void SetModeFormCaption()
        {
            Text = $"Mode window {ModePictureBox.Width}x{ModePictureBox.Height}";
        }
    }
}