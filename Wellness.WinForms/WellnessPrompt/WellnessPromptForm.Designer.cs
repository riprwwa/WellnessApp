using System.Windows.Forms;

namespace Wellness.WinForms.WellnessPrompt
{
    partial class WellnessPromptForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpBoxDoing = new System.Windows.Forms.GroupBox();
            this.txtDoing = new System.Windows.Forms.TextBox();
            this.grpBoxMisc = new System.Windows.Forms.GroupBox();
            this.txtBoxMisc = new System.Windows.Forms.TextBox();
            this.btnAccept = new System.Windows.Forms.Button();
            this.pnlFeelings = new System.Windows.Forms.Panel();
            this.grpBoxFeelings = new System.Windows.Forms.GroupBox();
            this.grpBoxDoing.SuspendLayout();
            this.grpBoxMisc.SuspendLayout();
            this.pnlFeelings.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBoxDoing
            // 
            this.grpBoxDoing.Controls.Add(this.txtDoing);
            this.grpBoxDoing.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpBoxDoing.Location = new System.Drawing.Point(4, 4);
            this.grpBoxDoing.Name = "grpBoxDoing";
            this.grpBoxDoing.Size = new System.Drawing.Size(691, 100);
            this.grpBoxDoing.TabIndex = 0;
            this.grpBoxDoing.TabStop = false;
            this.grpBoxDoing.Text = "What are you doing right now?";
            // 
            // txtDoing
            // 
            this.txtDoing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDoing.Location = new System.Drawing.Point(3, 19);
            this.txtDoing.Multiline = true;
            this.txtDoing.Name = "txtDoing";
            this.txtDoing.PlaceholderText = "Writing code, reading documentation etc";
            this.txtDoing.Size = new System.Drawing.Size(685, 78);
            this.txtDoing.TabIndex = 0;
            this.txtDoing.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.preSave);
            // 
            // grpBoxMisc
            // 
            this.grpBoxMisc.Controls.Add(this.txtBoxMisc);
            this.grpBoxMisc.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpBoxMisc.Location = new System.Drawing.Point(4, 496);
            this.grpBoxMisc.Name = "grpBoxMisc";
            this.grpBoxMisc.Size = new System.Drawing.Size(691, 93);
            this.grpBoxMisc.TabIndex = 1;
            this.grpBoxMisc.TabStop = false;
            this.grpBoxMisc.Text = "Anything else to share?";
            // 
            // txtBoxMisc
            // 
            this.txtBoxMisc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBoxMisc.Location = new System.Drawing.Point(3, 19);
            this.txtBoxMisc.Multiline = true;
            this.txtBoxMisc.Name = "txtBoxMisc";
            this.txtBoxMisc.PlaceholderText = "Anything else";
            this.txtBoxMisc.Size = new System.Drawing.Size(685, 71);
            this.txtBoxMisc.TabIndex = 2000;
            this.txtBoxMisc.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.preSave);
            // 
            // btnAccept
            // 
            this.btnAccept.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnAccept.Location = new System.Drawing.Point(4, 589);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(691, 23);
            this.btnAccept.TabIndex = 2001;
            this.btnAccept.Text = "&Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // pnlFeelings
            // 
            this.pnlFeelings.AutoScroll = true;
            this.pnlFeelings.BackColor = System.Drawing.SystemColors.Control;
            this.pnlFeelings.Controls.Add(this.grpBoxFeelings);
            this.pnlFeelings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFeelings.Location = new System.Drawing.Point(4, 104);
            this.pnlFeelings.Name = "pnlFeelings";
            this.pnlFeelings.Size = new System.Drawing.Size(691, 392);
            this.pnlFeelings.TabIndex = 3;
            // 
            // grpBoxFeelings
            // 
            this.grpBoxFeelings.AutoSize = true;
            this.grpBoxFeelings.BackColor = System.Drawing.SystemColors.Control;
            this.grpBoxFeelings.Location = new System.Drawing.Point(0, 0);
            this.grpBoxFeelings.Name = "grpBoxFeelings";
            this.grpBoxFeelings.Size = new System.Drawing.Size(685, 374);
            this.grpBoxFeelings.TabIndex = 1;
            this.grpBoxFeelings.TabStop = false;
            this.grpBoxFeelings.Text = "What are you feeling?";
            // 
            // WellnessPromptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 616);
            this.Controls.Add(this.pnlFeelings);
            this.Controls.Add(this.grpBoxMisc);
            this.Controls.Add(this.grpBoxDoing);
            this.Controls.Add(this.btnAccept);
            this.Name = "WellnessPromptForm";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Text = "WellnessPromptForm";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WellnessPromptForm_FormClosing);
            this.Load += new System.EventHandler(this.WellnessPromptForm_Load);
            this.grpBoxDoing.ResumeLayout(false);
            this.grpBoxDoing.PerformLayout();
            this.grpBoxMisc.ResumeLayout(false);
            this.grpBoxMisc.PerformLayout();
            this.pnlFeelings.ResumeLayout(false);
            this.pnlFeelings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox grpBoxDoing;
        private TextBox txtDoing;
        private GroupBox grpBoxMisc;
        private Panel pnlFeelings;
        private GroupBox grpBoxFeelings;
        private TextBox txtBoxMisc;
        private Button btnAccept;
    }
}