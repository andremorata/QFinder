using QFinder.Data;
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

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
