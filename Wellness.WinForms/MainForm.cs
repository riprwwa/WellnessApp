using System.ComponentModel;
using System.Configuration;
using Wellness.WinForms.WellnessPrompt;
using Timer = System.Threading.Timer;

namespace Wellness.WinForms
{
    public partial class MainForm : Form
    {
        private ActiveWindowTitleLogger vm;
        private WellnessPromptForm prompt;
        private Timer timer;

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
            prompt.Closing += PromptOnClosing;
            prompt.Show();

            RefreshAddresses();

            timer = new Timer(UpdateWellnessCheckinTimer);
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
            UpdateWellnessCheckinTimer();
            EnableTimerForGetTimeToNextCheckin(true);
        }

        private void HideIt()
        {
            notifyIcon.Visible = true;
            WindowState = FormWindowState.Minimized;
            Hide();
            EnableTimerForGetTimeToNextCheckin(false);
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

        private void btnLaunchWellnessPrompt_Click(object sender, EventArgs e)
        {
            LaunchWellnessPrompt();
        }

        private void showWellnessPromptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LaunchWellnessPrompt();
        }

        private void LaunchWellnessPrompt()
        {
            EnableTimerForGetTimeToNextCheckin(false);
            prompt.ShowIt();
        }

        private void PromptOnClosing(object? sender, CancelEventArgs e)
        {
            if (!Visible) return;
            EnableTimerForGetTimeToNextCheckin(true);
        }

        private void notifyIcon_MouseMove(object sender, MouseEventArgs e)
        {
            notifyIcon.Text = GetTimeToNextCheckin();
        }

        private void UpdateWellnessCheckinTimer(object? state = null)
        {
            Invoke(() => lblTimeToNextCheckin.Text = GetTimeToNextCheckin());
        }

        private string GetTimeToNextCheckin()
        {
            var remaining = prompt.NextShow - DateTime.Now;
            var time = remaining.TotalHours > 1
                ? remaining.ToString(@"hh\hmm\mss\s")
                : remaining.TotalMinutes > 1
                    ? remaining.ToString(@"mm\mss\s")
                    : remaining.ToString(@"ss\s");
            return $@"{time} to next wellness check-in";
        }

        private void EnableTimerForGetTimeToNextCheckin(bool enabled)
        {
            if (enabled)
            {
                timer.Change(1 * 1000, 1 * 1000);
            }
            else
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite);
            }
        }
    }
}