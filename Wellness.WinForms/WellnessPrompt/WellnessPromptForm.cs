using Newtonsoft.Json;
using System.Reflection;

namespace Wellness.WinForms.WellnessPrompt
{
    public partial class WellnessPromptForm : Form
    {
        private readonly string _rootDir;
        private List<Category>? _categories;
        private const int TimerInterval = 30;
        private int _timerInterval;
        private readonly System.Threading.Timer _timer;
        
        public DateTime NextShow { get; private set; }

        public WellnessPromptForm(string folder, int? timerIntervalMinutes = null)
        {
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
            Invoke(Show);
        }

        private void Timer_Tick(object? state)
        {
            ShowIt();
        }

        private void WellnessPromptForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            txtBoxMisc.Text = txtDoing.Text = "";
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

            NextShow = DateTime.Now.AddMilliseconds(_timerInterval);
            _timer.Change(_timerInterval, Timeout.Infinite);
            Hide();
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
            var count = _categories.Count;
            var maxFeelingSize = 180;
            var maxFeelingsPerRow = size.Width / maxFeelingSize;
            var tabIndex = 0;
            var grpStart = 4;
            var grpLeft = 4;
            var betweenGroups = 4;
            var rowSize = 20;
            var extraX = 20;
            var betweenFeelingRows = 10;

            foreach (var category in _categories!)
            {
                var groupBox = new GroupBox();
                groupBox.BackColor = category.UiColor;
                groupBox.TabStop = false;
                groupBox.Text = category.Name;
                //groupBox.Location = new Point(grpLeft, grpStart);
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
                    groupBox.Controls.Add(checkbox);
                }

                grpStart += betweenGroups;
                
                var feelingsHeight = rowSize + betweenFeelingRows + rowNumber * rowSize;

                grpStart += feelingsHeight;
                groupBox.Size = new Size(size.Width - 2, feelingsHeight);

                grpBoxFeelings.Controls.Add(groupBox);
            }

            grpBoxFeelings.ResumeLayout();
        }

        private const string _dateFormat = "yyyy_MM_dd";
        private const string _timeFormat = "HH:mm:ss";

        private void btnAccept_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save () {
            var feelings = GetFeelings();

            var json = JsonConvert.SerializeObject(feelings);
            var now = DateTime.Now;
            var date = now.ToString(_dateFormat);
            var time = now.ToString(_timeFormat);

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
            if (e.KeyValue != (int)Keys.Enter) return;
            if (e.Alt || e.Control) Save();
        }
    }
}
