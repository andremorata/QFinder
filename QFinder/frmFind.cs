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

namespace QFinder
{
    public partial class frmFind : Form
    {

        TypeAssistant assistant;

        public frmFind()
        {
            //Application startup
            Program.DB = new Data.DB();
            Program.DB.Folder = Application.StartupPath + "\\Data";
            if (!Program.DB.Check()) Program.DB.CreateDB();
            Program.DB.CheckDbStructure();

            //Run indexing subsystem
            Program.Idx = new Index.Index();
            Program.Idx.BuildIndex();

            InitializeComponent();

            assistant = new TypeAssistant();
            assistant.Idled += Assistant_Idled;

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
                    MessageBox.Show("The item you entered has failed to start. Please check if the file still exists or the commando is an available one." +
                        "\r\n\r\n Internal Error message: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
            Model model = new Model();
            
            var files = model.Files
                .Include("Type")
                .Where(i => i.Name.ToLower().Contains(text.ToLower()))
                .Take(10);

            return files.ToList();
        }

        private void tmrIndex_Tick(object sender, EventArgs e)
        {
            Program.Idx.BuildIndex();
        }
    }
}
