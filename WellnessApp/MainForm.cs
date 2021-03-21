using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WellnessApp
{
    public partial class MainForm : Form
    {
        bool isTimerOn;
        string previousTitle;
        string root;
        string Root
        {
            get
            {
                return root;
            }
            set
            {
                this.txtFolder.Text = root = value;
            }
        }

        public MainForm(string folder)
        {
            InitializeComponent();

            string rootDir;
            var hasDir = false;

            if (!string.IsNullOrWhiteSpace(folder) && Directory.Exists(folder))
            {
                rootDir = folder;
                hasDir = true;
            }
            else
            {
                rootDir = ConfigurationManager.AppSettings["LogFolder"]; ;
            }

            if (string.IsNullOrWhiteSpace(rootDir)) rootDir = System.Reflection.Assembly.GetEntryAssembly().Location;

            Root = Path.GetDirectoryName(rootDir);
            previousTitle = null;

            if (hasDir)
            {
                BtnControl_Click(null, null);
                HideIt();
            }
        }


        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        public string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            var path = Path.Combine(Root, $"{now.ToString("yyyyMMdd")}.txt");
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            using (var writer = new StreamWriter(path, true))
            {
                var title = GetActiveWindowTitle();
                var line = $"{now.ToString("HHmmss")}: {title}";
                if (previousTitle != title)
                {
                    writer.WriteLine(line);
                }
                previousTitle = title;
            }
        }

        private void BtnFolder_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog.SelectedPath = this.Root;
            var result = this.folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Root = this.folderBrowserDialog.SelectedPath;
            }
        }

        private void BtnControl_Click(object sender, EventArgs e)
        {
            isTimerOn = !isTimerOn;
            if (isTimerOn)
            {
                timer.Start();
                tsmiStart.Text = btnControl.Text = "Stop";
            }
            else
            {
                timer.Stop();
                tsmiStart.Text = btnControl.Text = "Start";
            }
            txtFolder.Enabled = btnFolder.Enabled = !isTimerOn;
        }

        private void TxtFolder_TextChanged(object sender, EventArgs e)
        {
            Root = ((TextBox)sender).Text;
        }

        private void ShowIt()
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            icon.Visible = false;
        }

        private void HideIt()
        {
            icon.Visible = true;
            this.WindowState = FormWindowState.Minimized;
            Hide();
        }

        private void TheForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                HideIt();
            }
        }

        private void TsmiStart_Click(object sender, EventArgs e)
        {
            BtnControl_Click(sender, e);
        }

        private void TsmiQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Icon_DoubleClick(object sender, EventArgs e)
        {
            ShowIt();
        }

        private void TsmiShow_Click(object sender, EventArgs e)
        {
            ShowIt();
        }
    }
}
