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
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

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