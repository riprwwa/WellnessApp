namespace Wellness.WinForms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.ctxtMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleWindowTitleLoggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showWellnessPromptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chkLogActiveWindowTitle = new System.Windows.Forms.CheckBox();
            this.grpBoxLogging = new System.Windows.Forms.GroupBox();
            this.btnViewLogs = new System.Windows.Forms.Button();
            this.grpBoxAddresses = new System.Windows.Forms.GroupBox();
            this.lstAddresses = new System.Windows.Forms.ListBox();
            this.btnRefreshAddresses = new System.Windows.Forms.Button();
            this.grpCheckin = new System.Windows.Forms.GroupBox();
            this.chkShowWellnessPrompt = new System.Windows.Forms.CheckBox();
            this.lblTimeToNextCheckin = new System.Windows.Forms.Label();
            this.btnLaunchWellnessPrompt = new System.Windows.Forms.Button();
            this.grpShortMessage = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTestShortMessage = new System.Windows.Forms.Button();
            this.chkShortMessageEnabled = new System.Windows.Forms.CheckBox();
            this.ctxtMenuStrip.SuspendLayout();
            this.grpBoxLogging.SuspendLayout();
            this.grpBoxAddresses.SuspendLayout();
            this.grpCheckin.SuspendLayout();
            this.grpShortMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.ctxtMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Wellness App";
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            this.notifyIcon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseMove);
            // 
            // ctxtMenuStrip
            // 
            this.ctxtMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.ctxtMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.toggleWindowTitleLoggingToolStripMenuItem,
            this.showWellnessPromptToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.ctxtMenuStrip.Name = "ctxtMenuStrip";
            this.ctxtMenuStrip.Size = new System.Drawing.Size(339, 132);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(338, 32);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // toggleWindowTitleLoggingToolStripMenuItem
            // 
            this.toggleWindowTitleLoggingToolStripMenuItem.Name = "toggleWindowTitleLoggingToolStripMenuItem";
            this.toggleWindowTitleLoggingToolStripMenuItem.Size = new System.Drawing.Size(338, 32);
            this.toggleWindowTitleLoggingToolStripMenuItem.Text = "Start logging active window title";
            this.toggleWindowTitleLoggingToolStripMenuItem.Click += new System.EventHandler(this.toggleWindowTitleLoggingToolStripMenuItem_Click);
            // 
            // showWellnessPromptToolStripMenuItem
            // 
            this.showWellnessPromptToolStripMenuItem.Name = "showWellnessPromptToolStripMenuItem";
            this.showWellnessPromptToolStripMenuItem.Size = new System.Drawing.Size(338, 32);
            this.showWellnessPromptToolStripMenuItem.Text = "Show wellness prompt";
            this.showWellnessPromptToolStripMenuItem.Click += new System.EventHandler(this.showWellnessPromptToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(338, 32);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // chkLogActiveWindowTitle
            // 
            this.chkLogActiveWindowTitle.AutoSize = true;
            this.chkLogActiveWindowTitle.Location = new System.Drawing.Point(9, 37);
            this.chkLogActiveWindowTitle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkLogActiveWindowTitle.Name = "chkLogActiveWindowTitle";
            this.chkLogActiveWindowTitle.Size = new System.Drawing.Size(219, 29);
            this.chkLogActiveWindowTitle.TabIndex = 1;
            this.chkLogActiveWindowTitle.Text = "Log active window title";
            this.chkLogActiveWindowTitle.UseVisualStyleBackColor = true;
            this.chkLogActiveWindowTitle.CheckedChanged += new System.EventHandler(this.chkLogActiveWindowTitle_CheckedChanged);
            // 
            // grpBoxLogging
            // 
            this.grpBoxLogging.Controls.Add(this.btnViewLogs);
            this.grpBoxLogging.Controls.Add(this.chkLogActiveWindowTitle);
            this.grpBoxLogging.Location = new System.Drawing.Point(17, 20);
            this.grpBoxLogging.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpBoxLogging.Name = "grpBoxLogging";
            this.grpBoxLogging.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpBoxLogging.Size = new System.Drawing.Size(819, 92);
            this.grpBoxLogging.TabIndex = 2;
            this.grpBoxLogging.TabStop = false;
            this.grpBoxLogging.Text = "Log titles";
            // 
            // btnViewLogs
            // 
            this.btnViewLogs.Enabled = false;
            this.btnViewLogs.Location = new System.Drawing.Point(703, 30);
            this.btnViewLogs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnViewLogs.Name = "btnViewLogs";
            this.btnViewLogs.Size = new System.Drawing.Size(107, 38);
            this.btnViewLogs.TabIndex = 2;
            this.btnViewLogs.Text = "View logs";
            this.btnViewLogs.UseVisualStyleBackColor = true;
            // 
            // grpBoxAddresses
            // 
            this.grpBoxAddresses.Controls.Add(this.lstAddresses);
            this.grpBoxAddresses.Controls.Add(this.btnRefreshAddresses);
            this.grpBoxAddresses.Location = new System.Drawing.Point(17, 122);
            this.grpBoxAddresses.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpBoxAddresses.Name = "grpBoxAddresses";
            this.grpBoxAddresses.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpBoxAddresses.Size = new System.Drawing.Size(819, 162);
            this.grpBoxAddresses.TabIndex = 3;
            this.grpBoxAddresses.TabStop = false;
            this.grpBoxAddresses.Text = "Network addresses";
            // 
            // lstAddresses
            // 
            this.lstAddresses.FormattingEnabled = true;
            this.lstAddresses.ItemHeight = 25;
            this.lstAddresses.Location = new System.Drawing.Point(11, 37);
            this.lstAddresses.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstAddresses.Name = "lstAddresses";
            this.lstAddresses.Size = new System.Drawing.Size(681, 104);
            this.lstAddresses.TabIndex = 3;
            // 
            // btnRefreshAddresses
            // 
            this.btnRefreshAddresses.Location = new System.Drawing.Point(703, 72);
            this.btnRefreshAddresses.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRefreshAddresses.Name = "btnRefreshAddresses";
            this.btnRefreshAddresses.Size = new System.Drawing.Size(107, 38);
            this.btnRefreshAddresses.TabIndex = 2;
            this.btnRefreshAddresses.Text = "Refresh";
            this.btnRefreshAddresses.UseVisualStyleBackColor = true;
            this.btnRefreshAddresses.Click += new System.EventHandler(this.btnRefreshAddresses_Click);
            // 
            // grpCheckin
            // 
            this.grpCheckin.Controls.Add(this.chkShowWellnessPrompt);
            this.grpCheckin.Controls.Add(this.lblTimeToNextCheckin);
            this.grpCheckin.Controls.Add(this.btnLaunchWellnessPrompt);
            this.grpCheckin.Location = new System.Drawing.Point(14, 295);
            this.grpCheckin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpCheckin.Name = "grpCheckin";
            this.grpCheckin.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpCheckin.Size = new System.Drawing.Size(819, 130);
            this.grpCheckin.TabIndex = 5;
            this.grpCheckin.TabStop = false;
            this.grpCheckin.Text = "Wellness prompt";
            // 
            // chkShowWellnessPrompt
            // 
            this.chkShowWellnessPrompt.AutoSize = true;
            this.chkShowWellnessPrompt.Location = new System.Drawing.Point(12, 40);
            this.chkShowWellnessPrompt.Name = "chkShowWellnessPrompt";
            this.chkShowWellnessPrompt.Size = new System.Drawing.Size(239, 29);
            this.chkShowWellnessPrompt.TabIndex = 4;
            this.chkShowWellnessPrompt.Text = "Wellness Prompt Enabled";
            this.chkShowWellnessPrompt.UseVisualStyleBackColor = true;
            // 
            // lblTimeToNextCheckin
            // 
            this.lblTimeToNextCheckin.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTimeToNextCheckin.Location = new System.Drawing.Point(12, 86);
            this.lblTimeToNextCheckin.Name = "lblTimeToNextCheckin";
            this.lblTimeToNextCheckin.Size = new System.Drawing.Size(500, 25);
            this.lblTimeToNextCheckin.TabIndex = 3;
            // 
            // btnLaunchWellnessPrompt
            // 
            this.btnLaunchWellnessPrompt.Location = new System.Drawing.Point(339, 34);
            this.btnLaunchWellnessPrompt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLaunchWellnessPrompt.Name = "btnLaunchWellnessPrompt";
            this.btnLaunchWellnessPrompt.Size = new System.Drawing.Size(266, 38);
            this.btnLaunchWellnessPrompt.TabIndex = 2;
            this.btnLaunchWellnessPrompt.Text = "Launch Wellness Prompt";
            this.btnLaunchWellnessPrompt.UseVisualStyleBackColor = true;
            this.btnLaunchWellnessPrompt.Click += new System.EventHandler(this.btnLaunchWellnessPrompt_Click);
            // 
            // grpShortMessage
            // 
            this.grpShortMessage.Controls.Add(this.textBox1);
            this.grpShortMessage.Controls.Add(this.label1);
            this.grpShortMessage.Controls.Add(this.btnTestShortMessage);
            this.grpShortMessage.Location = new System.Drawing.Point(14, 435);
            this.grpShortMessage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpShortMessage.Name = "grpShortMessage";
            this.grpShortMessage.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpShortMessage.Size = new System.Drawing.Size(819, 130);
            this.grpShortMessage.TabIndex = 6;
            this.grpShortMessage.TabStop = false;
            this.grpShortMessage.Text = "Short message";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(360, 85);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(450, 31);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "00:30:00|10|This is a test message";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(10, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 25);
            this.label1.TabIndex = 3;
            // 
            // btnTestShortMessage
            // 
            this.btnTestShortMessage.Location = new System.Drawing.Point(350, 40);
            this.btnTestShortMessage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnTestShortMessage.Name = "btnTestShortMessage";
            this.btnTestShortMessage.Size = new System.Drawing.Size(214, 42);
            this.btnTestShortMessage.TabIndex = 4;
            this.btnTestShortMessage.Text = "Test Short Message";
            this.btnTestShortMessage.UseVisualStyleBackColor = true;
            // 
            // chkShortMessageEnabled
            // 
            this.chkShortMessageEnabled.AutoSize = true;
            this.chkShortMessageEnabled.Location = new System.Drawing.Point(14, 482);
            this.chkShortMessageEnabled.Name = "chkShortMessageEnabled";
            this.chkShortMessageEnabled.Size = new System.Drawing.Size(224, 29);
            this.chkShortMessageEnabled.TabIndex = 7;
            this.chkShortMessageEnabled.Text = "Short Message Enabled";
            this.chkShortMessageEnabled.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 750);
            this.Controls.Add(this.chkShortMessageEnabled);
            this.Controls.Add(this.grpShortMessage);
            this.Controls.Add(this.grpCheckin);
            this.Controls.Add(this.grpBoxAddresses);
            this.Controls.Add(this.grpBoxLogging);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "Wellness";
            this.Shown += new System.EventHandler(this.mainForm_Shown);
            this.Resize += new System.EventHandler(this.mainForm_Resize);
            this.ctxtMenuStrip.ResumeLayout(false);
            this.grpBoxLogging.ResumeLayout(false);
            this.grpBoxLogging.PerformLayout();
            this.grpBoxAddresses.ResumeLayout(false);
            this.grpCheckin.ResumeLayout(false);
            this.grpCheckin.PerformLayout();
            this.grpShortMessage.ResumeLayout(false);
            this.grpShortMessage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NotifyIcon notifyIcon;
        private ContextMenuStrip ctxtMenuStrip;
        private ToolStripMenuItem quitToolStripMenuItem;
        private ToolStripMenuItem showToolStripMenuItem;
        private ToolStripMenuItem toggleWindowTitleLoggingToolStripMenuItem;
        private CheckBox chkLogActiveWindowTitle;
        private GroupBox grpBoxLogging;
        private Button btnViewLogs;
        private GroupBox grpBoxAddresses;
        private ListBox lstAddresses;
        private Button btnRefreshAddresses;
        private GroupBox grpCheckin;
        private Button btnLaunchWellnessPrompt;
        private ToolStripMenuItem showWellnessPromptToolStripMenuItem;
        private Label lblTimeToNextCheckin;
        private GroupBox grpShortMessage;
        private TextBox textBox1;
        private Label label1;
        private Button btnTestShortMessage;
        private CheckBox chkShowWellnessPrompt;
        private CheckBox chkShortMessageEnabled;
    }
}