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
            var dirs = new List<string>();

            dirs.Add(@"D:\Files");
            dirs.Add(@"D:\Install");
            dirs.Add(@"D:\Pictures");
            dirs.Add(@"D:\Music");
            dirs.Add(@"D:\Videos");
            dirs.Add(@"D:\Projetos");
            dirs.Add(@"D:\RDP");

            var ret = new List<string>();

            foreach (var dir in dirs)
            {
                ret.AddRange(GetFiles(dir, text));
            }

            return ret.ToArray();
        }

        private string[] GetFiles(string path, string term = "")
        {
            var ret = new List<string>();
            var firstDirectories = Directory.GetDirectories(path).Where(src => src.Contains(term));
            ret.AddRange(firstDirectories);
            foreach (var childDir in Directory.GetDirectories(path))
            {
                ret.AddRange(GetFiles(path, term));
            }
            ret.AddRange(Directory.GetFiles(path).Where(src => src.Contains(term)));
            return ret.ToArray();
        }

    }
}
