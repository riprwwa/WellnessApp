using System.Text.RegularExpressions;
using Timer = System.Threading.Timer;

namespace Wellness.WinForms;

public class ShortMessageSettings
{
    public static ShortMessageSettings FromString(string config)
    {
        try
        {
            var regex = new Regex(@"^(?<periodicity>[^\|]+)\|(?<duration>[^\|]+)\|(?<message>.+)$");
            var match = regex.Match(config);
            if (!match.Success)
            {
                return new ShortMessageSettings
                {
                    Periodicity = TimeSpan.FromMinutes(15),
                    Duration = 100,
                    Message = ""
                };
            }

            return new ShortMessageSettings
            {
                Periodicity = TimeSpan.Parse(match.Groups["periodicity"].Value),
                Duration = int.Parse(match.Groups["duration"].Value),
                Message = match.Groups["message"].Value
            };
        }
        catch
        {
            return null;
        }
    }

    public TimeSpan Periodicity { get; set; }
    public int Duration { get; set; }
    public string Message { get; set; }
}

public partial class ShortMessageForm : Form, IDisposable
{
    private int _timerInterval;
    private Timer _timer;
    private int _stage = 0;
    private ShortMessageSettings _settings;
    private readonly TextBox _message;

    public DateTime NextShow { get; private set; }

    public ShortMessageForm()
    {
        InitializeComponent();
        _timer = new Timer(Callback);
        _timer.Change(Timeout.Infinite, Timeout.Infinite);

        _message = new TextBox();
        _message.Dock = DockStyle.Fill;
        _message.Font = new Font(new FontFamily("Arial"), 172);
        _message.ForeColor = Color.FromArgb(1, Color.RoyalBlue);
        _message.Multiline = true;
        _message.ReadOnly = true;
        Controls.Add(_message);

        _settings = new ShortMessageSettings
        {
            Duration = 10,
            Message = "",
            Periodicity = TimeSpan.FromDays(10)
        };

        Opacity = 0;
        Show();
        Hide();
    }

    public void SetConfig(string config)
    {
        if (string.IsNullOrWhiteSpace(config))
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            return;
        }

        _settings = ShortMessageSettings.FromString(config);
        if (_settings == null)
        {
            return;
        }

        _message.Text = _settings.Message;

        _timerInterval = (int) _settings.Periodicity.TotalMilliseconds;
        _timer.Change(_timerInterval, _timerInterval);
        ResetNextShow();
    }

    public void Test()
    {
        Callback(null);
    }

    private void Callback(object? state)
    {
        if (_stage == 0)
        {
            _stage++;
            ShowIt();
        }
        else
        {
            _stage++;
            HideIt();
        }
    }

    private void ShowIt()
    {
        try
        {
            Invoke(() =>
            {
                Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;

                var mouseScreen = Screen.FromPoint(MousePosition);
                Location = new Point(mouseScreen.Bounds.Left + 10, mouseScreen.Bounds.Top + 10);
                Size = new Size(mouseScreen.Bounds.Width - 20, mouseScreen.Bounds.Height - 20);

                _message.SelectionStart = 0;
                _message.SelectionLength = 0;

                _timer.Change(_settings.Duration, Timeout.Infinite);
                Opacity = 1;
                Show();
                TopMost = true;
            });
        }
        catch
        {//
        }
    }

    private void HideIt()
    {
        try
        {
            Invoke(() =>
            {
                _timer.Change((int) _settings.Periodicity.TotalMilliseconds, Timeout.Infinite);
                ResetNextShow();
                Opacity = 0;
                Hide();
                _stage = 0;
                Thread.CurrentThread.Priority = ThreadPriority.Normal;
            });
        }
        catch
        {//
        }
    }

    public void ResetNextShow()
    {
        NextShow = DateTime.Now.AddMilliseconds(_timerInterval);
    }

    public new void Dispose()
    {
        _timer.Dispose();
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
