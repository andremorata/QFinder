using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using QFinder.Data;

namespace QFinder
{
    public partial class frmFind : Form
    {

        #region initialize and form handles

        public frmFind()
        {
            InitializeComponent();
        }


        private void frmFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Hide();
            }
        }

        private void frmFind_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized) Hide();
        }

        private void ntfIcon_DoubleClick(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        #endregion

        private void txtFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Process.Start(txtFind.Text.Trim());
            }
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            TextBox t = sender as TextBox;
            if (t != null)
            {
                if (t.Text.Length >= 3)
                {
                    string[] arr = SuggestStrings(t.Text);
                    AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                    collection.AddRange(arr);
                    txtFind.AutoCompleteCustomSource = collection;
                }
            }
        }

        private string[] SuggestStrings(string text)
        {
            Model model = new Model();
            var dirs = model.Files
                .Where(i => i.FullPath.Contains(text))
                .Select(i => i.Name);

            return dirs.ToArray();
        }
        
        private void tmrIndex_Tick(object sender, EventArgs e)
        {
            Program.Idx.BuildIndex();
        }
    }
}
