using System.Text.RegularExpressions;
using Timer = System.Threading.Timer;

namespace Wellness.WinForms
{
    public class ShortMessageSettings
    {
        public static ShortMessageSettings FromString(string config)
        {
            // 00:15:00|1|Cool stuff
            var regex = new Regex(@"^(?<periodicity>[^\|]+)\|(?<duration>[^\|]+)\|(?<message>.+)$");
            var match = regex.Match(config);
            if (!match.Success) return null;
            return new ShortMessageSettings
            {
                Periodicity = TimeSpan.Parse(match.Groups["periodicity"].Value),
                Duration = int.Parse(match.Groups["duration"].Value),
                Message = match.Groups["message"].Value
            };
        }

        public TimeSpan Periodicity { get; set; }
        public int Duration { get; set; }
        public string Message { get; set; }
    }

    public partial class ShortMessageForm : Form, IDisposable
    {
        private readonly int _timerInterval;

        public ShortMessageForm(string config)
        {
            InitializeComponent();

            settings = ShortMessageSettings.FromString(config);;

            var mouseScreen = Screen.FromPoint(MousePosition);
            var pointX = (mouseScreen.Bounds.Width) -50;
            var pointY = (mouseScreen.Bounds.Height) -50;
            this.Size = new Size(pointX, pointY);

            var textbox = new Label();
            textbox.Dock = DockStyle.Fill;
            textbox.Text = settings.Message;
            textbox.Font = new Font(new FontFamily("Arial"), 172);
            this.Controls.Add(textbox);

            Opacity = 0;
            _timer = new Timer(Callback);
            _timerInterval = (int) settings.Periodicity.TotalMilliseconds;
            _timer.Change(_timerInterval, Timeout.Infinite);
        }

        private Timer _timer;
        private int stage = 0;
        private ShortMessageSettings settings;

        private void Callback(object? state)
        {
            if (stage == 0)
            {
                stage++;
                ShowIt();
            }
            else
            {
                stage++;
                HideIt();
            }
        }

        private void ShowIt()
        {
            Invoke(() =>
            {
                Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;
                _timer.Change(settings.Duration, Timeout.Infinite);
                Opacity = 1;
            });
        }

        private void HideIt()
        {
            Invoke(() =>
            {
                _timer.Change(Timeout.Infinite, Timeout.Infinite);
                Opacity = 0;
                Close();
                Thread.CurrentThread.Priority = ThreadPriority.Normal;
            });
        }

        public new void Dispose()
        {
            _timer?.Dispose();
            base.Dispose();
        }
        public void TimerEnabled(bool enabled)
        {
            // if !enabled, set it to run when the world ends
            var interval = GetInterval(enabled);
            _timer.Change(interval, interval);
        }

        private int GetInterval(bool enabled) => enabled ? _timerInterval : Timeout.Infinite;
    }
}
