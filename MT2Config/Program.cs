using System;
using System.Windows.Forms;

namespace MT2Config
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Show unexpected errors as a message instead of the .NET crash dialog.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (sender, e) => ShowUnexpectedError(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => ShowUnexpectedError(e.ExceptionObject as Exception);

            Application.Run(new MainForm());
        }

        static void ShowUnexpectedError(Exception exception)
        {
            MessageBox.Show(string.Format(Language.Current.UnexpectedError, exception), Language.Current.Error,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
