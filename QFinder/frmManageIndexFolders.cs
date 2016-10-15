using QFinder.Data;
using QFinder.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QFinder
{
    public partial class frmManageIndexFolders : Form
    {

        int EditingItem = 0;

        public frmManageIndexFolders()
        {
            InitializeComponent();
            LoadGrid();
        }

        private void LoadGrid()
        {
            using (Model model = new Model())
            {
                BindingList<IndexingPath> dts = 
                    new BindingList<IndexingPath>(model.IndexingPaths.ToList());
                dts.AllowNew = true;
                grd.DataSource = dts;
                grd.Columns["Id"].Visible = false;
            }
        }

        string EditingPath = "";
        private void grd_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try { EditingPath = grd.Rows[e.RowIndex].Cells["Path"].Value.ToString(); }
            catch { EditingPath = ""; }
        }

        private void grd_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var oldValue = EditingPath;
                var newValue = grd.Rows[e.RowIndex].Cells["Path"].Value.ToString();

                using (Model model = new Model())
                {
                    var item = model.IndexingPaths.FirstOrDefault(i => i.Path == oldValue);
                    if (item != null)
                    {
                        item.Path = newValue;
                        model.SaveChanges();
                        LoadGrid();
                    }
                }
            }
            catch { }
        }
        
        private void grd_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            using (Model model = new Model())
            {
                var newPath = e.Row.Cells["Path"].Value.ToString();
                if (Directory.Exists(newPath))
                {
                    if (!model.IndexingPaths.Any(i => i.Path.Equals(newPath, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        model.IndexingPaths.Add(
                            new IndexingPath() { Path = newPath });
                        model.SaveChanges();
                        LoadGrid();
                    }
                }
                else
                {
                    MessageBox.Show("The path you entered is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadGrid();
                }
            }
        }

        private void grd_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            using (Model model = new Model())
            {
                var path = e.Row.Cells["Path"].Value.ToString();
                var toRemove = model.IndexingPaths.FirstOrDefault(i => i.Path == path);
                if (toRemove != null)
                {
                    model.IndexingPaths.Remove(toRemove);
                    model.SaveChanges();
                    LoadGrid();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
        
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (dlgFolderBrowse.ShowDialog() == DialogResult.OK)
                txtPath.Text = dlgFolderBrowse.SelectedPath;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPath.Text.Trim()))
                {
                    using (Model model = new Model())
                    {
                        if (EditingItem == 0) //new
                        {
                            model.IndexingPaths.Add(new IndexingPath() { Path = txtPath.Text.Trim() });
                        }
                        else //updating
                        {
                            var item = model.IndexingPaths.FirstOrDefault(i => i.Id == EditingItem);
                            if (item != null)
                            {
                                item.Path = txtPath.Text.Trim();
                            }
                        }
                        model.SaveChanges();
                        ClearEditing();
                        App.Idx.RestartLiveMonitoring();
                        MessageBox.Show("Path saved successfuly", "QFinder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(System.Diagnostics.EventLogEntryType.Error, $"QFinder - Index Paths - Error: {ex.Message}\r\n---------\r\n{ex.StackTrace}");
                MessageBox.Show( $"Error saving the path. Error: {ex.Message}", "QFinder", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                using (Model model = new Model())
                {
                    if (EditingItem != 0) //new
                    {
                        var item = model.IndexingPaths.FirstOrDefault(i => i.Id == EditingItem);
                        if (item != null)
                        {
                            if (MessageBox.Show("Are you sure?", "QFinder - Removing path of the index", 
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                model.IndexingPaths.Remove(item);
                                model.SaveChanges();
                                ClearEditing();
                                MessageBox.Show("Path removed successfuly", "QFinder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(System.Diagnostics.EventLogEntryType.Error, $"QFinder - Index Paths - Error: {ex.Message}\r\n---------\r\n{ex.StackTrace}");
                MessageBox.Show($"Error removing the path. Error: {ex.Message}", "QFinder", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearEditing();
        }

        private void ClearEditing()
        {
            txtPath.Clear();
            EditingItem = 0;
            txtPath.Focus();
            LoadGrid();
        }

        private void grd_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var item = grd.Rows[e.RowIndex];
            EditingItem = (int)item.Cells[0].Value;
            txtPath.Text = (string)item.Cells[1].Value;
            txtPath.Focus();
        }
    }
}
