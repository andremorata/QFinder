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
using QFinder.Helpers;
using System.Collections;
using System.Threading;

namespace QFinder
{
    public partial class frmFind : Form
    {

        TypeAssistant assistant;

        public frmFind()
        {
            InitializeComponent();

            assistant = new TypeAssistant(1000);
            assistant.Idled += Assistant_Idled;

            Task.Run(() =>
            {
                //Application startup
                Program.DB = new Data.DB();
                Program.DB.Folder = Application.StartupPath + "\\Data";
                if (!Program.DB.Check()) Program.DB.CreateDB();

                //Run indexing subsystem
                Program.Idx = new Index.Index();
                Program.Idx.BuildIndex();
            });
            ShowIndexInfo();
        }

        private async void ShowIndexInfo()
        {
            var count = "";
            await Task.Run(() =>
            {
                using (var m = new Model())
                {
                    count = $"{m.Files.LongCount().ToString()} files mapped.";
                }
            });
            lbInfo.Text = count;
        }

        #region initialize and form handles

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

        private new void Hide()
        {
            base.Hide();
            txtFind.Text = "";
            lstFiles.Items.Clear();
        }

        private void frmFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Hide();
            }

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    var selected = lstFiles.SelectedItems;
                    if (selected != null && selected.Count > 0 && selected[0] != null)
                    {
                        var item = selected[0];
                        Process.Start(item.SubItems[3].Text); //fullpath
                        Hide();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("The item you entered has failed to start. Please check if the file still exists or the command is an available one." +
                        "\r\n\r\n Internal Error message: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (e.KeyCode == Keys.Down && txtFind.Focused && lstFiles.Items.Count > 0)
            {
                lstFiles.Focus();
                lstFiles.Items[0].Selected = true;
            }

            if (e.KeyCode == Keys.Up && lstFiles.Focused &&
                lstFiles.SelectedItems.Count > 0 && lstFiles.SelectedItems[0].Index == 0)
            {
                txtFind.Focus();
            }
        }

        private void Assistant_Idled(object sender, EventArgs e)
        {
            this.Invoke(
                new MethodInvoker(() =>
                {
                    lstFiles.Items.Clear();
                    var suggests = SuggestStrings(txtFind.Text.Trim());
                    foreach (var item in suggests)
                        lstFiles.Items.Add(new ListViewItem(
                            new string[] { item.Name, item.Type.Name, item.Extension, item.FullPath }));
                })
            );
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            if (txtFind.Text.Length >= 3)
            {
                assistant.TextChanged();
            }
        }

        private ICollection<FileIndex> SuggestStrings(string text)
        {
            using (var model = new Model())
            {
                var type = "";
                if (text.Contains(">"))
                {
                    type = text.Split('>')[0];
                    text = text.Split('>')[1].Trim();
                }

                var files = model.Files.Include("Type").AsQueryable();
                if (type != "") files = files.Where(i => i.Extension.EndsWith(type));
                files = files.Where(i => i.Name.ToLower().Contains(text.ToLower()));

                return files.ToList();
            }
        }

        private void tmrIndex_Tick(object sender, EventArgs e)
        {
            Program.Idx.BuildIndex();
        }

        private void lbInfo_DoubleClick(object sender, EventArgs e)
        {
            lbInfo.Text = "Gathering Index Info...";
            ShowIndexInfo();
        }

        private void frmFind_FormClosing(object sender, FormClosingEventArgs e)
        {
            ntfIcon.Visible = false;
        }
    }
}
