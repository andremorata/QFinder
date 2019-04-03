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
            this.label1 = new System.Windows.Forms.Label();
            this.tmrIndex = new System.Windows.Forms.Timer(this.components);
            this.txtFind = new System.Windows.Forms.TextBox();
            this.lstFiles = new System.Windows.Forms.ListView();
            this.ColName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colExt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFullPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFolder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lbInfo = new System.Windows.Forms.Label();
            this.lbFullPath = new System.Windows.Forms.Label();
            this.tmrInfo = new System.Windows.Forms.Timer(this.components);
            this.lbReindex = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 5);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 15, 0, 5);
            this.label1.Size = new System.Drawing.Size(554, 63);
            this.label1.TabIndex = 1;
            this.label1.Text = "type something you want to find";
            // 
            // tmrIndex
            // 
            this.tmrIndex.Enabled = true;
            this.tmrIndex.Interval = 3600000;
            this.tmrIndex.Tick += new System.EventHandler(this.tmrIndex_Tick);
            // 
            // txtFind
            // 
            this.txtFind.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtFind.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllSystemSources;
            this.txtFind.BackColor = System.Drawing.Color.Black;
            this.txtFind.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFind.ForeColor = System.Drawing.Color.White;
            this.txtFind.Location = new System.Drawing.Point(8, 71);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(867, 49);
            this.txtFind.TabIndex = 2;
            this.txtFind.TextChanged += new System.EventHandler(this.txtFind_TextChanged);
            // 
            // lstFiles
            // 
            this.lstFiles.BackColor = System.Drawing.Color.Black;
            this.lstFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColName,
            this.colType,
            this.colExt,
            this.colFullPath,
            this.colFolder});
            this.lstFiles.Font = new System.Drawing.Font("Ubuntu Light", 11F);
            this.lstFiles.ForeColor = System.Drawing.Color.White;
            this.lstFiles.FullRowSelect = true;
            this.lstFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstFiles.HideSelection = false;
            this.lstFiles.Location = new System.Drawing.Point(8, 126);
            this.lstFiles.MultiSelect = false;
            this.lstFiles.Name = "lstFiles";
            this.lstFiles.Size = new System.Drawing.Size(867, 226);
            this.lstFiles.TabIndex = 3;
            this.lstFiles.UseCompatibleStateImageBehavior = false;
            this.lstFiles.View = System.Windows.Forms.View.Details;
            this.lstFiles.SelectedIndexChanged += new System.EventHandler(this.lstFiles_SelectedIndexChanged);
            this.lstFiles.DoubleClick += new System.EventHandler(this.lstFiles_DoubleClick);
            // 
            // ColName
            // 
            this.ColName.Text = "Name";
            this.ColName.Width = 652;
            // 
            // colType
            // 
            this.colType.Text = "Type";
            this.colType.Width = 64;
            // 
            // colExt
            // 
            this.colExt.Text = "Extension";
            this.colExt.Width = 117;
            // 
            // colFullPath
            // 
            this.colFullPath.Text = "FullPath";
            this.colFullPath.Width = 0;
            // 
            // colFolder
            // 
            this.colFolder.Text = "Folder";
            this.colFolder.Width = 0;
            // 
            // lbInfo
            // 
            this.lbInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbInfo.AutoSize = true;
            this.lbInfo.Font = new System.Drawing.Font("Ubuntu Mono", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInfo.Location = new System.Drawing.Point(41, 4);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(263, 23);
            this.lbInfo.TabIndex = 4;
            this.lbInfo.Text = "Gathering Index Info...";
            this.lbInfo.DoubleClick += new System.EventHandler(this.lbInfo_DoubleClick);
            // 
            // lbFullPath
            // 
            this.lbFullPath.AutoEllipsis = true;
            this.lbFullPath.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbFullPath.Font = new System.Drawing.Font("Ubuntu Light", 8F);
            this.lbFullPath.Location = new System.Drawing.Point(8, 396);
            this.lbFullPath.Name = "lbFullPath";
            this.lbFullPath.Size = new System.Drawing.Size(867, 25);
            this.lbFullPath.TabIndex = 5;
            // 
            // tmrInfo
            // 
            this.tmrInfo.Enabled = true;
            this.tmrInfo.Interval = 10000;
            this.tmrInfo.Tick += new System.EventHandler(this.tmrInfo_Tick);
            // 
            // lbReindex
            // 
            this.lbReindex.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbReindex.Font = new System.Drawing.Font("Webdings", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbReindex.ForeColor = System.Drawing.Color.DarkOrange;
            this.lbReindex.Location = new System.Drawing.Point(3, 0);
            this.lbReindex.Name = "lbReindex";
            this.lbReindex.Size = new System.Drawing.Size(32, 32);
            this.lbReindex.TabIndex = 6;
            this.lbReindex.Text = "q";
            this.lbReindex.Click += new System.EventHandler(this.lbReindex_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.txtFind);
            this.flowLayoutPanel1.Controls.Add(this.lstFiles);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel1.Controls.Add(this.lbFullPath);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(883, 428);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.lbReindex);
            this.flowLayoutPanel2.Controls.Add(this.lbInfo);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(8, 358);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(867, 35);
            this.flowLayoutPanel2.TabIndex = 4;
            // 
            // frmFind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(20F, 42F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(883, 428);
            this.Controls.Add(this.flowLayoutPanel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Ubuntu Light", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(8);
            this.Name = "frmFind";
            this.Opacity = 0.8D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QFinder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmFind_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmFind_KeyDown);
            this.Resize += new System.EventHandler(this.frmFind_Resize);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer tmrIndex;
        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.ListView lstFiles;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ColumnHeader ColName;
        private System.Windows.Forms.ColumnHeader colExt;
        private System.Windows.Forms.ColumnHeader colFullPath;
        private System.Windows.Forms.Label lbInfo;
        private System.Windows.Forms.Label lbFullPath;
        private System.Windows.Forms.Timer tmrInfo;
        private System.Windows.Forms.Label lbReindex;
        private System.Windows.Forms.ColumnHeader colFolder;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
    }
}

