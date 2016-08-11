namespace QFinder
{
    partial class frmFind
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFind));
            this.txtFind = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ntfIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.ctxNtfIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrIndex = new System.Windows.Forms.Timer(this.components);
            this.ctxNtfIcon.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFind
            // 
            this.txtFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFind.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtFind.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtFind.BackColor = System.Drawing.Color.White;
            this.txtFind.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFind.Location = new System.Drawing.Point(17, 50);
            this.txtFind.Margin = new System.Windows.Forms.Padding(8);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(857, 41);
            this.txtFind.TabIndex = 0;
            this.txtFind.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFind.TextChanged += new System.EventHandler(this.txtFind_TextChanged);
            this.txtFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFind_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(224, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(442, 35);
            this.label1.TabIndex = 1;
            this.label1.Text = "type something you want to find";
            // 
            // ntfIcon
            // 
            this.ntfIcon.ContextMenuStrip = this.ctxNtfIcon;
            this.ntfIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("ntfIcon.Icon")));
            this.ntfIcon.Text = "QFinder";
            this.ntfIcon.Visible = true;
            this.ntfIcon.DoubleClick += new System.EventHandler(this.ntfIcon_DoubleClick);
            // 
            // ctxNtfIcon
            // 
            this.ctxNtfIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.ctxNtfIcon.Name = "ctxNtfIcon";
            this.ctxNtfIcon.Size = new System.Drawing.Size(93, 26);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // tmrIndex
            // 
            this.tmrIndex.Enabled = true;
            this.tmrIndex.Interval = 1800000;
            this.tmrIndex.Tick += new System.EventHandler(this.tmrIndex_Tick);
            // 
            // frmFind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 35F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(891, 108);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFind);
            this.Font = new System.Drawing.Font("Ubuntu Light", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(8);
            this.Name = "frmFind";
            this.Opacity = 0.9D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QFinder";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmFind_KeyDown);
            this.Resize += new System.EventHandler(this.frmFind_Resize);
            this.ctxNtfIcon.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NotifyIcon ntfIcon;
        private System.Windows.Forms.ContextMenuStrip ctxNtfIcon;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Timer tmrIndex;
    }
}

