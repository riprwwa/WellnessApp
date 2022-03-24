using System.Runtime.InteropServices;
using System.Text;

namespace Wellness.WinForms
{
    public class ActiveWindowTitleLogger
    {
        private const string _dateFormat = "yyyy_MM_dd";
        private const string _timeFormat = "HH:mm:ss";
        private const int TimerInterval = 10 * 1000;
        private int _timerInterval;
        private readonly System.Threading.Timer _timer;
        private string _previousTitle = string.Empty;
        private DateTime _previousTime = DateTime.Now;
        private string _rootDir;
        private bool _isEnabled = false;

        public bool IsEnabled => _isEnabled;

        public ActiveWindowTitleLogger(string folder, int? timerInterval = null)
        {
            if (!string.IsNullOrWhiteSpace(folder) && Directory.Exists(folder))
            {
                _rootDir = folder;
            }

            if (string.IsNullOrWhiteSpace(_rootDir))
            {
                _rootDir = System.Reflection.Assembly.GetEntryAssembly()!.Location;
            }

            _timerInterval = timerInterval ?? TimerInterval;
            _timer = new System.Threading.Timer(Timer_Tick);
        }

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private static string? GetActiveWindowTitle()
        {
            const int nChars = 256;
            var buff = new StringBuilder(nChars);
            var handle = GetForegroundWindow();

            return GetWindowText(handle, buff, nChars) > 0 ? buff.ToString() : null;
        }

        private void Timer_Tick(object? state)
        {
            var windowTitle = GetActiveWindowTitle();
            if (_previousTitle == windowTitle) return;

            var now = DateTime.Now;
            var date = now.ToString(_dateFormat);
            var time = now.ToString(_timeFormat);
            
            _previousTime = now;
            _previousTitle = windowTitle!;
            
            var line = $"{time} {windowTitle}";
            WriteToFile($"{date}_tracking.txt", line);
        }

        private void WriteToFile(string name, string what)
        {
            var path = Path.Combine(_rootDir, name);
            var dir = Path.GetDirectoryName(path)!;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            using var writer = new StreamWriter(path, true);
            writer.WriteLine(what);
        }

        private string TimespanToString(TimeSpan span) => $"{span.Hours:00}h{span.Minutes:00}m{span.Seconds:00}s";

        public bool TimerEnabled(bool enabled)
        {
            // if !enabled, set it to run when the world ends
            var to = enabled ? _timerInterval : int.MaxValue;
            _timer.Change(to, to);
            return _isEnabled = enabled;
        }
    }
}
