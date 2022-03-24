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
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chkLogActiveWindowTitle = new System.Windows.Forms.CheckBox();
            this.grpBoxLogging = new System.Windows.Forms.GroupBox();
            this.btnViewLogs = new System.Windows.Forms.Button();
            this.grpBoxAddresses = new System.Windows.Forms.GroupBox();
            this.lstAddresses = new System.Windows.Forms.ListBox();
            this.btnRefreshAddresses = new System.Windows.Forms.Button();
            this.grpCheckin = new System.Windows.Forms.GroupBox();
            this.btnLaunchWellnessPrompt = new System.Windows.Forms.Button();
            this.showWellnessPromptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxtMenuStrip.SuspendLayout();
            this.grpBoxLogging.SuspendLayout();
            this.grpBoxAddresses.SuspendLayout();
            this.grpCheckin.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.ctxtMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Wellness App";
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // ctxtMenuStrip
            // 
            this.ctxtMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.toggleWindowTitleLoggingToolStripMenuItem,
            this.showWellnessPromptToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.ctxtMenuStrip.Name = "ctxtMenuStrip";
            this.ctxtMenuStrip.Size = new System.Drawing.Size(245, 114);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // toggleWindowTitleLoggingToolStripMenuItem
            // 
            this.toggleWindowTitleLoggingToolStripMenuItem.Name = "toggleWindowTitleLoggingToolStripMenuItem";
            this.toggleWindowTitleLoggingToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.toggleWindowTitleLoggingToolStripMenuItem.Text = "Start logging active window title";
            this.toggleWindowTitleLoggingToolStripMenuItem.Click += new System.EventHandler(this.toggleWindowTitleLoggingToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // chkLogActiveWindowTitle
            // 
            this.chkLogActiveWindowTitle.AutoSize = true;
            this.chkLogActiveWindowTitle.Location = new System.Drawing.Point(6, 22);
            this.chkLogActiveWindowTitle.Name = "chkLogActiveWindowTitle";
            this.chkLogActiveWindowTitle.Size = new System.Drawing.Size(148, 19);
            this.chkLogActiveWindowTitle.TabIndex = 1;
            this.chkLogActiveWindowTitle.Text = "Log active window title";
            this.chkLogActiveWindowTitle.UseVisualStyleBackColor = true;
            this.chkLogActiveWindowTitle.CheckedChanged += new System.EventHandler(this.chkLogActiveWindowTitle_CheckedChanged);
            // 
            // grpBoxLogging
            // 
            this.grpBoxLogging.Controls.Add(this.btnViewLogs);
            this.grpBoxLogging.Controls.Add(this.chkLogActiveWindowTitle);
            this.grpBoxLogging.Location = new System.Drawing.Point(12, 12);
            this.grpBoxLogging.Name = "grpBoxLogging";
            this.grpBoxLogging.Size = new System.Drawing.Size(573, 55);
            this.grpBoxLogging.TabIndex = 2;
            this.grpBoxLogging.TabStop = false;
            this.grpBoxLogging.Text = "Log titles";
            // 
            // btnViewLogs
            // 
            this.btnViewLogs.Enabled = false;
            this.btnViewLogs.Location = new System.Drawing.Point(492, 18);
            this.btnViewLogs.Name = "btnViewLogs";
            this.btnViewLogs.Size = new System.Drawing.Size(75, 23);
            this.btnViewLogs.TabIndex = 2;
            this.btnViewLogs.Text = "View logs";
            this.btnViewLogs.UseVisualStyleBackColor = true;
            // 
            // grpBoxAddresses
            // 
            this.grpBoxAddresses.Controls.Add(this.lstAddresses);
            this.grpBoxAddresses.Controls.Add(this.btnRefreshAddresses);
            this.grpBoxAddresses.Location = new System.Drawing.Point(12, 73);
            this.grpBoxAddresses.Name = "grpBoxAddresses";
            this.grpBoxAddresses.Size = new System.Drawing.Size(573, 97);
            this.grpBoxAddresses.TabIndex = 3;
            this.grpBoxAddresses.TabStop = false;
            this.grpBoxAddresses.Text = "Network addresses";
            // 
            // lstAddresses
            // 
            this.lstAddresses.FormattingEnabled = true;
            this.lstAddresses.ItemHeight = 15;
            this.lstAddresses.Location = new System.Drawing.Point(8, 22);
            this.lstAddresses.Name = "lstAddresses";
            this.lstAddresses.Size = new System.Drawing.Size(478, 64);
            this.lstAddresses.TabIndex = 3;
            // 
            // btnRefreshAddresses
            // 
            this.btnRefreshAddresses.Location = new System.Drawing.Point(492, 43);
            this.btnRefreshAddresses.Name = "btnRefreshAddresses";
            this.btnRefreshAddresses.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshAddresses.TabIndex = 2;
            this.btnRefreshAddresses.Text = "Refresh";
            this.btnRefreshAddresses.UseVisualStyleBackColor = true;
            this.btnRefreshAddresses.Click += new System.EventHandler(this.btnRefreshAddresses_Click);
            // 
            // grpCheckin
            // 
            this.grpCheckin.Controls.Add(this.btnLaunchWellnessPrompt);
            this.grpCheckin.Location = new System.Drawing.Point(10, 177);
            this.grpCheckin.Name = "grpCheckin";
            this.grpCheckin.Size = new System.Drawing.Size(573, 97);
            this.grpCheckin.TabIndex = 5;
            this.grpCheckin.TabStop = false;
            this.grpCheckin.Text = "Wellness prompt";
            // 
            // btnLaunchWellnessPrompt
            // 
            this.btnLaunchWellnessPrompt.Location = new System.Drawing.Point(10, 22);
            this.btnLaunchWellnessPrompt.Name = "btnLaunchWellnessPrompt";
            this.btnLaunchWellnessPrompt.Size = new System.Drawing.Size(186, 23);
            this.btnLaunchWellnessPrompt.TabIndex = 2;
            this.btnLaunchWellnessPrompt.Text = "Launch Wellness Prompt";
            this.btnLaunchWellnessPrompt.UseVisualStyleBackColor = true;
            this.btnLaunchWellnessPrompt.Click += new System.EventHandler(this.btnLaunchWellnessPrompt_Click);
            // 
            // showWellnessPromptToolStripMenuItem
            // 
            this.showWellnessPromptToolStripMenuItem.Name = "showWellnessPromptToolStripMenuItem";
            this.showWellnessPromptToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.showWellnessPromptToolStripMenuItem.Text = "Show wellness prompt";
            this.showWellnessPromptToolStripMenuItem.Click += new System.EventHandler(this.showWellnessPromptToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 450);
            this.Controls.Add(this.grpCheckin);
            this.Controls.Add(this.grpBoxAddresses);
            this.Controls.Add(this.grpBoxLogging);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Wellness";
            this.Shown += new System.EventHandler(this.mainForm_Shown);
            this.Resize += new System.EventHandler(this.mainForm_Resize);
            this.ctxtMenuStrip.ResumeLayout(false);
            this.grpBoxLogging.ResumeLayout(false);
            this.grpBoxLogging.PerformLayout();
            this.grpBoxAddresses.ResumeLayout(false);
            this.grpCheckin.ResumeLayout(false);
            this.ResumeLayout(false);

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
    }
}