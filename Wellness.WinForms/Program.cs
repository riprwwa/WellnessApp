namespace Wellness.WinForms
{
    internal static class Program
    {
        const string MutexName = "ASDLnup8c2342346@#$@$Esdf";

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
                MessageBox.Show("Already running?");
            }
        }

        static bool IsNew()
        {
            var _ = new Mutex(true, MutexName, out bool createdNew);
            return createdNew;
        }
    }
}