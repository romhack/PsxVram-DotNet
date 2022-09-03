using PsxVram_DotNet.Forms;

namespace PsxVram_DotNet;

internal static class Program
{
    /// <summary>
    ///     Главная точка входа для приложения.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MainForm());
    }
}