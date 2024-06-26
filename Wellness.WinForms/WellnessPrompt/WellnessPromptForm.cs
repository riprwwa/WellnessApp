﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using Timer = System.Threading.Timer;
// ReSharper disable InconsistentNaming

namespace Wellness.WinForms.WellnessPrompt
{
    public partial class WellnessPromptForm : Form
    {
        private readonly bool _makeReallyVisible;
        private readonly string _rootDir;
        private List<Category> _categories;
        private const int TimerInterval = 30;
        private int _timerInterval;
        private readonly Timer _timer;
        private Dictionary<string, Tuple<Point, Rectangle>> _previousBoundsAndLocations = new Dictionary<string, Tuple<Point, Rectangle>>();

        public DateTime NextShow { get; private set; }

        public WellnessPromptForm(string folder, int? timerIntervalMinutes = null, bool makeReallyVisible = false)
        {
            showDelegate = ShowDelegateMethod;

            _makeReallyVisible = makeReallyVisible;
            InitializeComponent();

            if (!string.IsNullOrWhiteSpace(folder) && Directory.Exists(folder))
            {
                _rootDir = folder;
            }

            if (string.IsNullOrWhiteSpace(_rootDir))
            {
                _rootDir = Assembly.GetEntryAssembly().Location;
            }

            _timer = new Timer(Timer_Tick);
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
                    location = previousPositionOnThisScreen.Item1;
                }
            }

            Invoke(showDelegate, location);
        }

        delegate void ShowDelegate(Point location);
        private void ShowDelegateMethod(Point location)
        {
            if (_makeReallyVisible && location != Point.Empty)
            {
                Location = location;
            }

            ClearControls();
            txtDoing.Focus();
            Show();

            if (!_makeReallyVisible) return;

            TopMost = true;
            FlashWindowEx(this);
        }

        private ShowDelegate showDelegate;


        private string GetScreenKey(Screen screen) => $"{screen.DeviceName}{screen.Bounds}";


        private void Timer_Tick(object state)
        {
            ShowIt();
        }

        private void WellnessPromptForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

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
            txtBoxMisc.Text = string.Empty;
            txtDoing.Text = string.Empty;
            foreach (var grp in grpBoxFeelings.Controls)
            {
                var grpBox = grp as GroupBox;
                if (grpBox == null) continue;
                foreach (var chk in grpBox.Controls)
                {
                    var chkBox = chk as CheckBox;
                    if (chkBox == null) continue;
                    chkBox.Checked = false;
                }
            }
        }

        private void WellnessPromptForm_Load(object sender, EventArgs e)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Wellness.WinForms.feelings.json";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {

                var result = reader.ReadToEnd();
                _categories = JsonConvert.DeserializeObject<List<Category>>(result);
                LoadCategories();
            }
        }

        private void LoadCategories()
        {
            if (_categories == null || !_categories.Any()) return;

            grpBoxFeelings.SuspendLayout();
            grpBoxFeelings.Controls.Clear();

            var size = grpBoxFeelings.Size;
            var maxFeelingWidth = 180;
            var maxFeelingsPerRow = size.Width / maxFeelingWidth;
            var tabIndex = 0;
            var rowHeight = 20;
            var checkBoxExtraWidth = 20;
            var betweenFeelingRows = 10;

            foreach (var category in _categories)
            {
                var groupBox = new GroupBox();
                groupBox.BackColor = category.UiColor;
                groupBox.TabStop = false;
                groupBox.Text = category.Name;
                groupBox.Dock = DockStyle.Bottom;
                
                for (var i = 0; i < category.Items.Count; i++)
                {
                    var startX = maxFeelingWidth * (i % maxFeelingsPerRow) + checkBoxExtraWidth;
                    var startY = (i / maxFeelingsPerRow + 1) * rowHeight;

                    var checkbox = new CheckBox
                    {
                        Location = new Point(startX, startY),
                        Size = new Size(maxFeelingWidth, rowHeight),
                        TabIndex = tabIndex++,
                        Text = category.Items[i],
                        UseVisualStyleBackColor = true
                    };
                    checkbox.PreviewKeyDown += preSave;

                    groupBox.Controls.Add(checkbox);
                }
                
                var rowsPerCategory = (int)Math.Ceiling(category.Items.Count / (decimal) maxFeelingsPerRow);
                var feelingsHeight = rowHeight + betweenFeelingRows + rowsPerCategory * rowHeight;

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
                var grpBox = grp as GroupBox;
                if (grpBox == null) continue;
                var checkedFeelings = new List<string>();
                foreach (var chk in grpBox.Controls)
                {
                    var chkBox = chk as CheckBox;
                    if (chkBox == null) continue;
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
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            using (var writer = new StreamWriter(path, true))
            {
                writer.WriteLine(what);
            }
        }

        private void preSave(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue != (int)Keys.Enter) return;
            if (!e.Alt && !e.Control && !(sender is CheckBox)) return;
            if (sender is TextBox box)
            {
                box.Text.TrimEnd('\n');
                box.Text.TrimEnd('\r');
            }
            Save();
        }

        #region support flashing
        // https://stackoverflow.com/a/11310217

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        //Flash both the window caption and taskbar button.
        //This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags. 
        public const uint FLASHW_ALL = 3;

        // Flash continuously until the window comes to the foreground. 
        public const uint FLASHW_TIMERNOFG = 12;

        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            public uint cbSize;
            public IntPtr hwnd;
            public uint dwFlags;
            public uint uCount;
            public uint dwTimeout;
        }

        // Do the flashing - this does not involve a raincoat.
        public static bool FlashWindowEx(Form form)
        {
            IntPtr hWnd = form.Handle;
            FLASHWINFO fInfo = new FLASHWINFO();

            fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
            fInfo.hwnd = hWnd;
            fInfo.dwFlags = FLASHW_ALL | FLASHW_TIMERNOFG;
            fInfo.uCount = uint.MaxValue;
            fInfo.dwTimeout = 0;

            return FlashWindowEx(ref fInfo);
        }

        #endregion support flashing
    }
}
