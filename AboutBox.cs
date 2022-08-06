using System.Diagnostics;


namespace PsxVram_DotNet
{
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
        }


        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = "https://github.com/romhack", UseShellExecute = true });
        }
    }
}
