using QFinder.Data;
using System;
using System.Windows.Forms;

namespace QFinder
{
    public partial class frmIndexSchedule : Form
    {
        IndexSchedule currentSchedule;

        public frmIndexSchedule()
        {
            InitializeComponent();
            currentSchedule = App.Idx.GetIndexSchedule();
            UpdateControls();
        }

        private void UpdateControls()
        {
            ddlType.SelectedItem = currentSchedule.Type;
            txtValue.Value = currentSchedule.Value;
            ckEnable.Checked = currentSchedule.Enabled;
        }

        private void ckEnable_CheckedChanged(object sender, EventArgs e)
        {
            var ck = ckEnable.Checked;
            txtValue.Enabled = ck;
            ddlType.Enabled = ck;
            btnSave.Enabled = ck;
            currentSchedule.Enabled = ck;
            App.Idx.UpdateIndexSchedule(currentSchedule);
            App.Idx.CheckSchedule();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            App.Idx.UpdateIndexSchedule(
                new IndexSchedule()
                {
                    Enabled = ckEnable.Checked,
                    Type = ddlType.SelectedItem.ToString(),
                    Value = Convert.ToInt32(txtValue.Value)
                });
            App.Idx.CheckSchedule();
            Close();
        }
    }
}
