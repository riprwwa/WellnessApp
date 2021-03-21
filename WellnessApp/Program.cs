using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WellnessApp
{
    static class Program
    {
        const string MutexName = "ASDLnup8c2342346@#$@$Esdf";

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            if (IsNew())
            {
                Application.Run(new MainForm(args.FirstOrDefault()));
            }
            else
            {
                MessageBox.Show("Already running?");
            }
        }

        static bool IsNew()
        {
            var mutex = new Mutex(true, MutexName, out bool createdNew);
            return createdNew;
        }
    }
}
