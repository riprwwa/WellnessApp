using System.Threading;
using System.Windows.Forms;
using System;

namespace Wellness.WinForms
{
    internal static class Program
    {
#if DEBUG
        static readonly string MutexName = "Wellness.WinForms.Mutex.Debug";
#else
        static readonly string MutexName = "Wellness.WinForms.Mutex";
#endif

        [STAThread]
        static void Main()
        {
            if (IsNew())
            {
                var form = new MainForm();
                Application.Run(form);
            }
            else
            {
                MessageBox.Show(@"Already running?");
            }
        }

        static bool IsNew()
        {
            var _ = new Mutex(true, MutexName, out bool createdNew);
            return createdNew;
        }
    }
}