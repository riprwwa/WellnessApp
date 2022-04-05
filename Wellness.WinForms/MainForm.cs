using System.Configuration;
using Wellness.WinForms.WellnessPrompt;

namespace Wellness.WinForms
{
    public partial class MainForm : Form
    {
        private ActiveWindowTitleLogger vm;
        private WellnessPromptForm prompt;

        public MainForm()
        {
            InitializeComponent();

            var folder = ConfigurationManager.AppSettings["ActiveWindowTitleLogger_LogFolder"];

            var parsedInterval = int.TryParse(ConfigurationManager.AppSettings["ActiveWindowTitleLogger_TimeInterval_Seconds"], out var trackingInterval);
            if (!parsedInterval) trackingInterval = 10;
            if (trackingInterval < 0) trackingInterval = Timeout.Infinite;
            vm = new ActiveWindowTitleLogger(folder!, trackingInterval);
            
            if (!bool.TryParse(ConfigurationManager.AppSettings["WellnessCheckin_MakeReallyVisible"], out var makeReallyVisible))
                makeReallyVisible = false;
            parsedInterval = int.TryParse(ConfigurationManager.AppSettings["WellnessCheckin_TimeInterval_Minutes"], out var timerInterval);
            prompt = new WellnessPromptForm(folder!, parsedInterval ? timerInterval : null, makeReallyVisible);
            prompt.Show();

            RefreshAddresses();
        }

        private void mainForm_Shown(object sender, EventArgs e)
        {
            ToggleLoggingOfWindowTitle(true, true);
            HideIt();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowIt();
            Close();
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
            ToggleLoggingOfWindowTitle(!vm.IsEnabled, true);
        }

        private void ToggleLoggingOfWindowTitle(bool enabled, bool alsoUpdateCheckbox = false)
        {
            toggleWindowTitleLoggingToolStripMenuItem.Text = $@"{(enabled ? "Stop" : "Start")} logging active window title";

            if (alsoUpdateCheckbox)
            {
                chkLogActiveWindowTitle.Checked = enabled;
            }

            toggleWindowTitleLoggingToolStripMenuItem.Checked = enabled;
            vm.TimerEnabled(enabled);
        }

        private void chkLogActiveWindowTitle_CheckedChanged(object sender, EventArgs e)
        {
            ToggleLoggingOfWindowTitle((sender as CheckBox)!.Checked);
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
                // yes
            }
        }

        private void LaunchWellnessPrompt()
        {
            prompt.ShowIt();
        }

        private void btnLaunchWellnessPrompt_Click(object sender, EventArgs e)
        {
            LaunchWellnessPrompt();
        }

        private void showWellnessPromptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LaunchWellnessPrompt();
        }

        private void notifyIcon_MouseMove(object sender, MouseEventArgs e)
        {
            var remaining = prompt.NextShow - DateTime.Now;
            var time = remaining.TotalMinutes > 1
                ? remaining.ToString(@"mm\mss\s")
                : remaining.ToString(@"ss\s");
            notifyIcon.Text = $@"{time} to next wellness check-in";
        }
    }
}