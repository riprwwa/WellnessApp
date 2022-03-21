using System.Configuration;

namespace Wellness.WinForms
{
    public partial class MainForm : Form
    {
        private ActiveWindowTitleLogger vm;

        public MainForm()
        {
            InitializeComponent();

            var folder = ConfigurationManager.AppSettings["ActiveWindowTitleLogger_LogFolder"];
            vm = new ActiveWindowTitleLogger(folder!);

            RefreshAddresses();
        }

        private void mainForm_Shown(object sender, EventArgs e)
        {
            ToggleLoggingOfWindowTitle(true, true);
            HideIt();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mainForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                HideIt();
        }

        private void ShowIt()
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void HideIt()
        {
            notifyIcon.Visible = true;
            WindowState = FormWindowState.Minimized;
            Hide();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowIt();
        }

        private void toggleWindowTitleLoggingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleLoggingOfWindowTitle(vm.IsEnabled, true);
        }

        private void ToggleLoggingOfWindowTitle(bool enabled, bool alsoUpdateCheckbox = false)
        {
            toggleWindowTitleLoggingToolStripMenuItem.Text = $@"{(enabled ? "Stop" : "Start")} logging active window title";

            if (alsoUpdateCheckbox)
            {
                chkLogActiveWindowTitle.Checked = enabled;
            }

            vm.TimerEnabled(enabled);
        }

        private void chkLogActiveWindowTitle_CheckedChanged(object sender, EventArgs e)
        {
            ToggleLoggingOfWindowTitle((sender as CheckBox).Checked);
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            ShowIt();
        }

        private void btnRefreshAddresses_Click(object sender, EventArgs e)
        {
            RefreshAddresses();
        }

        private void RefreshAddresses()
        {
            try
            {
                var addresses = new NetworkAddressRetriever().NetworkAddresses;
                lstAddresses.DataSource = addresses.ToList();
            }
            catch
            {
            }
        }
    }
}