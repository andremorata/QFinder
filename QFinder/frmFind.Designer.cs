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
            this.lbInfo = new System.Windows.Forms.Label();
            this.lbFullPath = new System.Windows.Forms.Label();
            this.tmrInfo = new System.Windows.Forms.Timer(this.components);
            this.lbReindex = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(442, 35);
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
            this.txtFind.BackColor = System.Drawing.Color.Black;
            this.txtFind.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFind.ForeColor = System.Drawing.Color.White;
            this.txtFind.Location = new System.Drawing.Point(12, 47);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(867, 41);
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
            this.colFullPath});
            this.lstFiles.Font = new System.Drawing.Font("Ubuntu Light", 11F);
            this.lstFiles.ForeColor = System.Drawing.Color.White;
            this.lstFiles.FullRowSelect = true;
            this.lstFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstFiles.HideSelection = false;
            this.lstFiles.Location = new System.Drawing.Point(12, 94);
            this.lstFiles.MultiSelect = false;
            this.lstFiles.Name = "lstFiles";
            this.lstFiles.Size = new System.Drawing.Size(867, 226);
            this.lstFiles.TabIndex = 3;
            this.lstFiles.UseCompatibleStateImageBehavior = false;
            this.lstFiles.View = System.Windows.Forms.View.Details;
            this.lstFiles.SelectedIndexChanged += new System.EventHandler(this.lstFiles_SelectedIndexChanged);
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
            // lbInfo
            // 
            this.lbInfo.AutoSize = true;
            this.lbInfo.Font = new System.Drawing.Font("Ubuntu Mono", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInfo.Location = new System.Drawing.Point(42, 325);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(192, 17);
            this.lbInfo.TabIndex = 4;
            this.lbInfo.Text = "Gathering Index Info...";
            this.lbInfo.DoubleClick += new System.EventHandler(this.lbInfo_DoubleClick);
            // 
            // lbFullPath
            // 
            this.lbFullPath.AutoEllipsis = true;
            this.lbFullPath.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbFullPath.Font = new System.Drawing.Font("Ubuntu Light", 8F);
            this.lbFullPath.Location = new System.Drawing.Point(0, 351);
            this.lbFullPath.Name = "lbFullPath";
            this.lbFullPath.Size = new System.Drawing.Size(891, 20);
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
            this.lbReindex.AutoSize = true;
            this.lbReindex.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbReindex.Font = new System.Drawing.Font("Webdings", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbReindex.ForeColor = System.Drawing.Color.DarkOrange;
            this.lbReindex.Location = new System.Drawing.Point(5, 320);
            this.lbReindex.Name = "lbReindex";
            this.lbReindex.Size = new System.Drawing.Size(40, 30);
            this.lbReindex.TabIndex = 6;
            this.lbReindex.Text = "q";
            this.lbReindex.Click += new System.EventHandler(this.lbReindex_Click);
            // 
            // frmFind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 35F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(891, 371);
            this.Controls.Add(this.lbReindex);
            this.Controls.Add(this.lbFullPath);
            this.Controls.Add(this.lbInfo);
            this.Controls.Add(this.lstFiles);
            this.Controls.Add(this.txtFind);
            this.Controls.Add(this.label1);
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
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmFind_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}

