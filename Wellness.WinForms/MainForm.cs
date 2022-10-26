using System.ComponentModel;
using System.Configuration;
using Wellness.WinForms.WellnessPrompt;
using Timer = System.Threading.Timer;

namespace Wellness.WinForms
{
    public partial class MainForm : Form
    {
        private readonly ActiveWindowTitleLogger _activeWindowTitleLogger;
        private readonly WellnessPromptForm _wellnessPrompt;
        private readonly ShortMessageForm _shortMessageForm;

        private readonly Timer _wellnessPromptLabelTimer;
        private readonly Timer _shortMessageFormLabelTimer;

        public MainForm()
        {
            InitializeComponent();

            RefreshAddresses();

            var folder = ConfigurationManager.AppSettings["ActiveWindowTitleLogger_LogFolder"];

            var parsedInterval = int.TryParse(ConfigurationManager.AppSettings["ActiveWindowTitleLogger_TimeInterval_Seconds"], out var trackingInterval);
            if (!parsedInterval) trackingInterval = 10;
            if (trackingInterval < 0) trackingInterval = Timeout.Infinite;
            _activeWindowTitleLogger = new ActiveWindowTitleLogger(folder!, trackingInterval);
            
            if (!bool.TryParse(ConfigurationManager.AppSettings["WellnessCheckin_MakeReallyVisible"], out var makeReallyVisible))
                makeReallyVisible = false;
            parsedInterval = int.TryParse(ConfigurationManager.AppSettings["WellnessCheckin_TimeInterval_Minutes"], out var timerInterval);
            _wellnessPrompt = new WellnessPromptForm(folder!, parsedInterval ? timerInterval : null, makeReallyVisible);
            _wellnessPrompt.Closing += PromptOnClosing;
            _wellnessPrompt.Show();

            _wellnessPromptLabelTimer = new Timer(UpdateWellnessCheckinLabelText);
            _shortMessageFormLabelTimer = new Timer(UpdateShortMessageLabelText);

            txtShortMessage.Text = ConfigurationManager.AppSettings["ShortMessage"] ?? "";
            _shortMessageForm = new ShortMessageForm();
            _shortMessageForm.SetConfig(txtShortMessage.Text);
        }

        #region show hide resize
        private void mainForm_Shown(object sender, EventArgs e)
        {
            ToggleLoggingOfWindowTitle(true, true);
            ToggleWellnessPromptTimer(true, true);
            ToggleShortMessageTimer(true, true);
            HideMainForm();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowMainForm();
            Close();
        }

        private void mainForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized != WindowState) return;
            HideMainForm();
        }

        private void ShowMainForm()
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;

            UpdateWellnessCheckinLabelText();
            EnableTimerForGetTimeToNextCheckin(true);

            EnableTimerForGetTimeToNextShortMessage(true);
        }

        private void HideMainForm()
        {
            notifyIcon.Visible = true;
            WindowState = FormWindowState.Minimized;
            Hide();
            EnableTimerForGetTimeToNextCheckin(false);
            EnableTimerForGetTimeToNextShortMessage(false);
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowMainForm();
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            ShowMainForm();
        }
        private void notifyIcon_MouseMove(object sender, MouseEventArgs e)
        {
            notifyIcon.Text = GetTimeToNextCheckin();
        }
        #endregion show hide resize

        #region window logging
        private void toggleWindowTitleLoggingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleLoggingOfWindowTitle(!_activeWindowTitleLogger.IsEnabled, true);
        }

        private void ToggleLoggingOfWindowTitle(bool enabled, bool alsoUpdateCheckbox = false)
        {
            //toggleWindowTitleLoggingToolStripMenuItem.Text = $@"{(enabled ? "Stop" : "Start")} logging active window title";

            if (alsoUpdateCheckbox)
            {
                chkLogActiveWindowTitle.Checked = enabled;
            }

            toggleWindowTitleLoggingToolStripMenuItem.Checked = enabled;
            _activeWindowTitleLogger.TimerEnabled(enabled);
        }

        private void chkLogActiveWindowTitle_CheckedChanged(object sender, EventArgs e)
        {
            ToggleLoggingOfWindowTitle((sender as CheckBox)!.Checked);
        }
        #endregion window logging

        #region network address things
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
        #endregion network address things

        #region wellness prompt
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
            _wellnessPrompt.ShowIt();
        }

        private void PromptOnClosing(object? sender, CancelEventArgs e)
        {
            if (!_wellnessPrompt.Visible) return;
            EnableTimerForGetTimeToNextCheckin(true);
        }

        private void UpdateWellnessCheckinLabelText(object? state = null)
        {
            Invoke(() =>
            {
                lblTimeToNextCheckin.Text = GetTimeToNextCheckin();
                lblTimeToNextCheckin.Visible = true;
            });
        }

        private string GetTimeToNextCheckin()
        {
            var remaining = _wellnessPrompt.NextShow - DateTime.Now;
            var time = remaining.TotalHours > 1
                ? remaining.ToString(@"hh\hmm\mss\s")
                : remaining.TotalMinutes > 1
                    ? remaining.ToString(@"mm\mss\s")
                    : remaining.ToString(@"ss\s");
            return $@"{time} to next wellness check-in";
        }

        private void chkShowWellnessPrompt_CheckedChanged(object sender, EventArgs e)
        {
            ToggleWellnessPromptTimer((sender as CheckBox)!.Checked);
        }

        private void ToggleWellnessPromptTimer(bool enabled, bool alsoUpdateCheckbox = false)
        {
            if (alsoUpdateCheckbox)
            {
                chkShowWellnessPrompt.Checked = enabled;
            }
            
            _wellnessPrompt.TimerEnabled(enabled);
            
            EnableTimerForGetTimeToNextCheckin(enabled);
        }

        private void EnableTimerForGetTimeToNextCheckin(bool enabled)
        {
            if (enabled)
            {
                _wellnessPromptLabelTimer.Change(1 * 1000, 1 * 1000);
            }
            else
            {
                lblTimeToNextCheckin.Visible = false;
                _wellnessPromptLabelTimer.Change(Timeout.Infinite, Timeout.Infinite);
            }
            _wellnessPrompt.ResetNextShow();
        }
        #endregion wellness prompt

        #region short message
        private void btnTestShortMessage_Click(object sender, EventArgs e)
        {
            _shortMessageForm.Test();
        }

        private void TxtShortMessageOnTextChanged(object? sender, EventArgs e)
        {
            _shortMessageForm.SetConfig(txtShortMessage.Text);
        }
        private void chkShortMessageEnabled_CheckedChanged(object sender, EventArgs e)
        {
            ToggleShortMessageTimer((sender as CheckBox)!.Checked);
        }

        private void UpdateShortMessageLabelText(object? state = null)
        {
            Invoke(() =>
            {
                lblTimeToNextShortMessage.Text = GetTimeToNextShortMessage();
                lblTimeToNextShortMessage.Visible = true;
            });
        }

        private string GetTimeToNextShortMessage()
        {
            var remaining = _shortMessageForm.NextShow - DateTime.Now;
            var time = remaining.TotalHours > 1
                ? remaining.ToString(@"hh\hmm\mss\s")
                : remaining.TotalMinutes > 1
                    ? remaining.ToString(@"mm\mss\s")
                    : remaining.ToString(@"ss\s");
            return $@"{time} to next short message";
        }

        private void ToggleShortMessageTimer(bool enabled, bool alsoUpdateCheckbox = false)
        {
            if (alsoUpdateCheckbox)
            {
                chkShortMessageEnabled.Checked = enabled;
            }

            _shortMessageForm.TimerEnabled(enabled);

            EnableTimerForGetTimeToNextShortMessage(enabled);
        }

        private void EnableTimerForGetTimeToNextShortMessage(bool enabled)
        {
            if (enabled)
            {
                _shortMessageFormLabelTimer.Change(1 * 1000, 1 * 1000);
            }
            else
            {
                lblTimeToNextShortMessage.Visible = false;
                _shortMessageFormLabelTimer.Change(Timeout.Infinite, Timeout.Infinite);
            }
            _shortMessageForm.ResetNextShow();
        }
        #endregion short message
    }
}