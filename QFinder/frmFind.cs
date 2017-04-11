using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using QFinder.Data;
using QFinder.Helpers;
using System.IO;

namespace QFinder
{
	public partial class frmFind : Form
	{

		TypeAssistant assistant;
		List<string> history;

		public frmFind()
		{
			InitializeComponent();

			ShowInTaskbar = true;

			assistant = new TypeAssistant(1000);
			assistant.Idled += Assistant_Idled;

			history = new List<string>();

			Task.Run(() =>
			{
                //Application startup
                App.DB = new DB()
                {
                    Folder = Application.StartupPath + "\\Data"
                };
                if (!App.DB.Check()) App.DB.CreateDB();

				//Run indexing subsystem
				App.Idx = new Indexing.Index();
				App.Idx.StartMonitoring();

				App.Idx.CheckSchedule();
			});

			BringToFront();
			ShowIndexInfo();
			txtFind.Focus();

		}

		private async void ShowIndexInfo()
		{
			var count = "";
			await Task.Run(() =>
			{
				using (var m = new Model())
				{
					count = $"{m.Files.Count().ToString()} files mapped.";
				}
			});
			lbInfo.Text = count;
		}

		private new void Hide()
		{
			base.WindowState = FormWindowState.Minimized;
			txtFind.Text = "";
			txtFind.Focus();
			lbFullPath.Text = "";
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
					if (SpecialCommands.RunStaticCommand(txtFind.Text))
					{
						txtFind.Clear();
						txtFind.Focus();
						lstFiles.Items.Clear();
						return;
					}

					var selected = lstFiles.SelectedItems;
					if (selected != null && selected.Count > 0 && selected[0] != null)
					{
						var item = selected[0];
						if (!e.Control)
							Process.Start(item.SubItems[3].Text); //fullpath
						else
							Process.Start("explorer.exe", $"/select,{item.SubItems[3].Text}"); //open folder with item selected
						Hide();
					}
					else if (!string.IsNullOrEmpty(txtFind.Text.Trim()))
					{
						Process.Start(txtFind.Text.Trim());
						Hide();
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("The command you entered has failed to start. Please check if the file still exists " + 
                        $"or if this is an available command.\r\n\r\n Internal Error message: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			if (e.KeyCode == Keys.Up && txtFind.Focused && e.Shift)
			{
				var val = txtFind.Text.Trim();
				var item = history.FirstOrDefault(i => i == val);
				if (!string.IsNullOrEmpty(item))
				{
					var pos = history.IndexOf(item);
					if (pos > 0)
						txtFind.Text = history[pos - 1];
					else
						txtFind.Text = history.Last();
				}
				else
					txtFind.Text = history[history.Count - 1];
				txtFind.Update(); txtFind.SelectAll(); txtFind.Focus();
				return;
			}

			if (e.KeyCode == Keys.Down && txtFind.Focused && e.Shift)
			{
				try
				{
					var val = txtFind.Text.Trim();
					var item = history.FirstOrDefault(i => i == val);
					if (!string.IsNullOrEmpty(item))
					{
						var pos = history.IndexOf(item);
						if (pos < history.Count - 1)
							txtFind.Text = history[history.IndexOf(item) + 1];
						else
							txtFind.Text = history[0];
					}
					else
						txtFind.Text = history[0];
					txtFind.Update(); txtFind.SelectAll(); txtFind.Focus();
					return;
				} catch { }
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

			if (e.KeyCode == Keys.D && e.Modifiers == Keys.Alt)
			{
				txtFind.Focus();
			}
		}

		private void Assistant_Idled(object sender, EventArgs e)
		{
			this.Invoke(
				new MethodInvoker(() =>
				{
					var val = txtFind.Text.Trim();
					var item = history.FirstOrDefault(i => i == val);
					if (item == null) history.Add(val);
					if (history.Count > 25)
						history.RemoveAt(0);

					lstFiles.Items.Clear();
					lbFullPath.Text = "";
					var suggests = GetIndex(val);
					foreach (var i in suggests)
						lstFiles.Items.Add(new ListViewItem(
							new string[] { i.FileName, i.Type.Name, i.Extension, i.FullPath }));
				})
			);
		}

		private void txtFind_TextChanged(object sender, EventArgs e)
		{
			if (txtFind.Text.Length >= 1) assistant.TextChanged();
		}

		private ICollection<FileIndex> GetIndex(string text)
		{
			using (var model = new Model())
			{
				//text = text.RemoveDiacritics();
				var type = "";
				if (text.Contains(">"))
				{
					type = text.Split('>')[0].ToUpper();
					text = text.Split('>')[1].Trim();
				}

				var files = model.Files.Include("Type").AsQueryable();
				if (type != "") files = files.Where(i => i.Extension.Equals(type));

				if (text.Contains("/"))
				{
					var folder = text.Split('/')[0].ToLower();
					text = text.Split('/')[1];
					if (folder != "") files = files.Where(i => i.Path.ToLower().Contains(folder));
				}

				var terms = text.Split(' ');
				if (terms.Count() > 0)
				{
					foreach (var term in terms)
					{
						if (term.StartsWith("*"))
						{
							var val = term.Replace("*", "");
                            files = files.Where(i => i.Name.ToLower().EndsWith(val.ToLower()));
								//.Substring(i.Name.Length - val.Length, val.Length) == val.ToLower());
						}
						else if (term.EndsWith("*"))
						{
							var val = term.Replace("*", "");
							files = files.Where(i => i.Name.ToLower()
								.StartsWith(val.ToLower()));
						}
						else if (term.Contains("*"))
						{
							var start = term.ToLower().Split('*')[0];
							var end = term.ToLower().Split('*')[1];
                            files = files.Where(i => 
                                i.Name.ToLower().StartsWith(start) && 
                                i.Name.ToLower().EndsWith(end)
								//.Substring(i.Name.ToLower().Length - end.Length, end.Length) == end.ToLower()
							);
						}
						else
							files = files.Where(i => 
                                i.Name.ToLower().Contains(term.ToLower()) ||
                                term.ToLower().Contains(i.Name.ToLower() + "." + i.Extension.ToLower())
                            );
					}
				}
				else
				{
					files = files.Where(i => 
                        i.Name.ToLower().Contains(text.ToLower()) ||
                        text.ToLower().Contains(i.Name.ToLower() + "." + i.Extension.ToLower())
                    );
				}

				return files
					.OrderByDescending(i => i.Type.Name)
					.ThenBy(i => i.Name)
					.Take(50) //50 items tops
					.ToList();

			}
		}

		private void tmrIndex_Tick(object sender, EventArgs e)
		{
			//Program.Idx.BuildIndex();
		}

		private void lbInfo_DoubleClick(object sender, EventArgs e)
		{
			lbInfo.Text = "Gathering Index Info...";
			ShowIndexInfo();
		}

		private void lstFiles_SelectedIndexChanged(object sender, EventArgs e)
		{
			var selected = lstFiles.SelectedItems;
			if (selected != null && selected.Count > 0 && selected[0] != null)
			{
				var item = selected[0];
				lbFullPath.Text = item.SubItems[3].Text; //fullpath
			}
			else
			{
				lbFullPath.Text = "";
			}
		}

		private void tmrInfo_Tick(object sender, EventArgs e)
		{
			ShowIndexInfo();
		}

		private void lbReindex_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure that you want to re-map all files? This might take few minutes to complete.",
				"Rebuild Index", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
			{
				App.Idx.RebuildIndex();
			}
		}

		private void frmFind_FormClosing(object sender, FormClosingEventArgs e)
		{
			Application.Exit();
		}

		private void frmFind_Resize(object sender, EventArgs e)
		{
			txtFind.Focus();
		}

		private void lstFiles_DoubleClick(object sender, EventArgs e)
		{
			if (lstFiles.SelectedIndices.Count > 0)
			{
				frmFind_KeyDown(this, new KeyEventArgs(Keys.Enter));
			}
		}
	}
}
