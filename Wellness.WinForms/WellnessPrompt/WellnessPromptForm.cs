using Newtonsoft.Json;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Wellness.WinForms.WellnessPrompt
{
    public partial class WellnessPromptForm : Form
    {
        private readonly bool _makeReallyVisible;
        private readonly string _rootDir;
        private List<Category>? _categories;
        private const int TimerInterval = 30;
        private int _timerInterval;
        private readonly System.Threading.Timer _timer;
        private Dictionary<string, Tuple<Point, Rectangle>> _previousBoundsAndLocations = new();

        public DateTime NextShow { get; private set; }

        public WellnessPromptForm(string folder, int? timerIntervalMinutes = null, bool makeReallyVisible = false)
        {
            _makeReallyVisible = makeReallyVisible;
            InitializeComponent();

            if (!string.IsNullOrWhiteSpace(folder) && Directory.Exists(folder))
            {
                _rootDir = folder;
            }

            if (string.IsNullOrWhiteSpace(_rootDir))
            {
                _rootDir = Assembly.GetEntryAssembly()!.Location;
            }

            _timer = new System.Threading.Timer(Timer_Tick);
            _timerInterval = (timerIntervalMinutes.HasValue ? timerIntervalMinutes.Value : TimerInterval) * 60 * 1000;
            _timer.Change(_timerInterval, Timeout.Infinite);
        }
        
        public void ShowIt()
        {
            var location = Point.Empty;
            if (_makeReallyVisible)
            {
                // this is assumed based on mouse position
                var mouseScreen = Screen.FromPoint(MousePosition);

                // don't center if previous show was on the same screen
                var screenKey = GetScreenKey(mouseScreen);
                var wasThisScreenVisited = _previousBoundsAndLocations.TryGetValue(screenKey, out var previousPositionOnThisScreen);
                
                if (!wasThisScreenVisited)
                {
                    var pointX = (mouseScreen.Bounds.Width - Width) >> 1;
                    var pointY = (mouseScreen.Bounds.Height - Height) >> 1;
                    location = new Point(pointX + mouseScreen.Bounds.Left, pointY + mouseScreen.Bounds.Top);
                }
                else
                {
                    location = previousPositionOnThisScreen!.Item1;
                }
            }

            Invoke(() =>
            {
                if (_makeReallyVisible && location != Point.Empty)
                {
                    Location = location;
                }

                Show();

                if (!_makeReallyVisible) return;

                TopMost = true;
                FlashWindowEx(this);
            });
        }

        private string GetScreenKey(Screen screen) => $"{screen.DeviceName}{screen.Bounds}";


        private void Timer_Tick(object? state)
        {
            ShowIt();
        }

        private void WellnessPromptForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            ClearControls();

            UpdatePreviousWindowLocation();

            NextShow = DateTime.Now.AddMilliseconds(_timerInterval);
            _timer.Change(_timerInterval, Timeout.Infinite);
            Hide();
        }

        private void UpdatePreviousWindowLocation()
        {
            var mouseScreen = Screen.FromPoint(MousePosition);
            var screenKey = GetScreenKey(mouseScreen);
            _previousBoundsAndLocations[screenKey] = new Tuple<Point, Rectangle>(Location, mouseScreen.Bounds);
        }

        private void ClearControls()
        {
            txtBoxMisc.Text = txtDoing.Text = "";
            foreach (var grp in grpBoxFeelings.Controls)
            {
                var grpBox = grp as GroupBox;
                if (grpBox == null) continue;
                foreach (var chk in grpBox.Controls)
                {
                    if (chk is not CheckBox chkBox) continue;
                    chkBox.Checked = false;
                }
            }
        }

        private void WellnessPromptForm_Load(object sender, EventArgs e)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Wellness.WinForms.feelings.json";

            using var stream = assembly.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(stream!);
            var result = reader.ReadToEnd();
            _categories = JsonConvert.DeserializeObject<List<Category>>(result);
            LoadCategories();
        }

        private void LoadCategories()
        {
            if (_categories == null || !_categories.Any()) return;

            grpBoxFeelings.SuspendLayout();
            grpBoxFeelings.Controls.Clear();

            var size = grpBoxFeelings.Size;
            var maxFeelingSize = 180;
            var maxFeelingsPerRow = size.Width / maxFeelingSize;
            var tabIndex = 0;
            var rowSize = 20;
            var extraX = 20;
            var betweenFeelingRows = 10;

            foreach (var category in _categories!)
            {
                var groupBox = new GroupBox();
                groupBox.BackColor = category.UiColor;
                groupBox.TabStop = false;
                groupBox.Text = category.Name;
                groupBox.Dock = DockStyle.Bottom;

                var i = 0;
                var rowNumber = category.Items.Count / maxFeelingsPerRow + 1;
                for (; i < category.Items.Count; i++)
                {
                    var checkbox = new CheckBox();
                    var startX = maxFeelingSize * (i % maxFeelingsPerRow) + extraX;
                    var startY = (i / maxFeelingsPerRow) * rowSize + rowSize;
                    checkbox.Location = new Point(startX, startY);
                    checkbox.Size = new Size(maxFeelingSize, rowSize);
                    checkbox.TabIndex = tabIndex++;
                    checkbox.Text = category.Items[i];
                    checkbox.UseVisualStyleBackColor = true;
                    checkbox.PreviewKeyDown += preSave!;
                    groupBox.Controls.Add(checkbox);
                }

                var feelingsHeight = rowSize + betweenFeelingRows + rowNumber * rowSize;

                groupBox.Size = new Size(size.Width - 2, feelingsHeight);

                grpBoxFeelings.Controls.Add(groupBox);
            }

            grpBoxFeelings.ResumeLayout();
        }

        private const string DateFormat = "yyyy_MM_dd";
        private const string TimeFormat = "HH:mm:ss";

        private void btnAccept_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            var feelings = GetFeelings();

            var json = JsonConvert.SerializeObject(feelings);
            var now = DateTime.Now;
            var date = now.ToString(DateFormat);
            var time = now.ToString(TimeFormat);

            var line = $"{time} '{json}'";
            WriteToFile($"{date}_feelings.txt", line);
            Close();
        }

        private Checkin GetFeelings()
        {
            var feelings = new Checkin(txtDoing.Text, txtBoxMisc.Text, new List<FeelingType>());
            foreach (var grp in grpBoxFeelings.Controls)
            {
                if (grp is not GroupBox grpBox) continue;
                var checkedFeelings = new List<string>();
                foreach (var chk in grpBox.Controls)
                {
                    if (chk is not CheckBox chkBox) continue;
                    if (!chkBox.Checked) continue;
                    checkedFeelings.Add(chkBox.Text);
                }

                if (!checkedFeelings.Any()) continue;
                feelings.Feelings.Add(new FeelingType(grpBox.Text, checkedFeelings));
            }

            return feelings;
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

        private void preSave(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue != (int) Keys.Enter) return;
            if (e.Alt || e.Control) Save();
        }

        #region support flashing

        // https://stackoverflow.com/a/11310217
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        //Flash both the window caption and taskbar button.
        //This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags. 
        public const UInt32 FLASHW_ALL = 3;

        // Flash continuously until the window comes to the foreground. 
        public const UInt32 FLASHW_TIMERNOFG = 12;

        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            public UInt32 cbSize;
            public IntPtr hwnd;
            public UInt32 dwFlags;
            public UInt32 uCount;
            public UInt32 dwTimeout;
        }

        // Do the flashing - this does not involve a raincoat.
        public static bool FlashWindowEx(Form form)
        {
            IntPtr hWnd = form.Handle;
            FLASHWINFO fInfo = new FLASHWINFO();

            fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
            fInfo.hwnd = hWnd;
            fInfo.dwFlags = FLASHW_ALL | FLASHW_TIMERNOFG;
            fInfo.uCount = UInt32.MaxValue;
            fInfo.dwTimeout = 0;

            return FlashWindowEx(ref fInfo);
        }

        #endregion support flashing
    }
}
