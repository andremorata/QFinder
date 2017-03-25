using QFinder.Data;
using QFinder.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QFinder.Indexing
{
	class Index
	{
		private List<FileSystemWatcher> monitors;
		System.Windows.Forms.Timer IndexScheduler = new System.Windows.Forms.Timer();

		public Index()
		{
			monitors = new List<FileSystemWatcher>();
		}

		public void RebuildIndex()
		{
			var indexTask = Task.Run(() =>
			{
				Log.Write($"QFinder - Indexing Started at {DateTime.Now.ToString()}");
				Model model = new Model();
				var dirs = model.IndexingPaths.Select(i => i.Path);
				var mapped = new List<string>();

				List<Task> factory = new List<Task>();

				foreach (var dir in dirs)
				{
					factory.Add(
						Task.Factory.StartNew(() =>
						{
							mapped.AddRange(Map(dir));
						})
					);
				}

				Task.WaitAll(factory.ToArray());

				var toBeRemoved = new List<FileIndex>();
				foreach (var item in model.Files)
					if (!mapped.Any(i => i.ToLower() == item.FullPath.ToLower()))
						toBeRemoved.Add(item);

				model.Files.RemoveRange(toBeRemoved);
				model.SaveChanges();
				Log.Write($"QFinder - Indexing Ended at {DateTime.Now.ToString()}");
			});
		}

		private string[] Map(string path, string term = "")
		{
			var ret = new List<string>();
			var firstDirectories = Directory.GetDirectories(path).Where(src => src.Contains(term));
			ret.AddRange(firstDirectories);
			foreach (var item in firstDirectories)
			{
				try { AddIndexedItem(item); }
				catch (Exception ex)
				{
					LogFailure(ex.Message);
				}
			}

			foreach (var childDir in Directory.GetDirectories(path))
			{
				try
				{
					AddIndexedItem(childDir);
					ret.AddRange(Map(childDir, term));
				}
				catch (Exception ex)
				{
					LogFailure(ex.Message);
				}
			}

			var dirFiles = Directory.GetFiles(path).Where(src => src.Contains(term));
			ret.AddRange(dirFiles);
			foreach (var item in dirFiles)
			{
				try
				{
					AddIndexedItem(item);
				}
				catch (Exception ex)
				{
					LogFailure(ex.Message);
				}
			}
			return ret.ToArray();
		}

		public void DestroyAndRebuildIndex()
		{
			using (var model = new Model())
			{
				Task.Run(() =>
				{
					model.Files.RemoveRange(
					model.Files);
					model.SaveChanges();
				}).Wait();
				RebuildIndex();
			}
		}

		internal void CheckSchedule()
		{
			var schedule = App.Idx.GetIndexSchedule();
			if (schedule.Enabled)
			{
				var multiplier = 0;
				switch (schedule.Type)
				{
					case "Minutes":
						multiplier = 60;
						break;
					case "Hours":
						multiplier = 60 * 60;
						break;
					case "Days":
						multiplier = 60 * 60 * 24;
						break;
					case "Weeks":
						multiplier = 60 * 60 * 24 * 7;
						break;
					default:
						break;
				}
				var interval = 1000 * multiplier;

				IndexScheduler = null;
				IndexScheduler = new System.Windows.Forms.Timer();
				IndexScheduler.Interval = schedule.Value * interval;
				IndexScheduler.Tick += IndexScheduler_Tick;
				IndexScheduler.Start();
			}
			else
				IndexScheduler.Stop();
		}

		private void IndexScheduler_Tick(object sender, EventArgs e)
		{
			RebuildIndex();
		}

		#region Utils

		private string GetIfIsFileOrFolder(string path)
		{
			if (Directory.Exists(path))
				return "Folder";
			else if (File.Exists(path))
				return "File";
			else
				return null;
		}

		private void AddIndexedItem(string path)
		{
			try
			{
				Model model = new Model();
				var addItem = GetItemForIndex(path);
				if (addItem != null &&
					!model.Files.Any(i => i.Path == addItem.Path && i.Name == addItem.Name && i.Extension == addItem.Extension))
				{
					model.Files.Add(
						new FileIndex()
						{
							TypeId = addItem.TypeId,
							Name = addItem.Name,
							Extension = addItem.Extension,
							Path = addItem.Path
						});
				}
				model.SaveChanges();
			}
			catch (Exception ex)
			{
				LogFailure(ex.Message + "\r\n" + path);
			}

		}

		private FileIndex GetItemForIndex(string path)
		{
			var type = GetIfIsFileOrFolder(path);
			if (type == null) return null;

			Model model = new Model();

			var itemType = model.FileIndexTypes.FirstOrDefault(i => i.Name == type);

			var folder = path;
			if (itemType.Name != "Folder")
				folder = path.Substring(0, path.LastIndexOf("\\"));

			var ext = "";
			var name = path.Substring(path.LastIndexOf("\\") + 1);
			if (itemType.Name != "Folder" && name.LastIndexOf(".") >= 0)
			{
				ext = name.Substring(name.LastIndexOf(".") + 1).ToUpper();
				name = name.Substring(0, name.LastIndexOf("."));
			}

			return new FileIndex()
			{
				TypeId = itemType.Id,
				Name = name,
				Extension = ext,
				Path = folder
			};
		}

		internal void RestartLiveMonitoring()
		{
			StopAllMonitors();
			StartMonitoring();
		}

		private FileIndex GetItemSegments(string path)
		{
			var itemData = new FileIndex();
			itemData.Path = path.Substring(0, path.LastIndexOf('\\'));
			itemData.Name = path.Substring(path.LastIndexOf('\\') + 1);
			if (itemData.Name.LastIndexOf('.') > -1)
			{
				itemData.Extension = itemData.Name.Substring(itemData.Name.LastIndexOf('.') + 1).ToUpper();
				itemData.Name = itemData.Name.Substring(0, itemData.Name.LastIndexOf('.'));
			}
			return itemData;
		}

		private void LogFailure(string error)
		{
			Log.Write(EventLogEntryType.Error, error);
		}

		public IndexSchedule GetIndexSchedule()
		{
			using (Model db = new Model())
			{
				var schedule = db.IndexSchedule.FirstOrDefault();
				return schedule ?? new IndexSchedule()
				{
					Enabled = true,
					Type = "Days",
					Value = 1
				};
			}
		}

		public void UpdateIndexSchedule(IndexSchedule m)
		{
			using (Model db = new Model())
			{
				var schedule = db.IndexSchedule.FirstOrDefault();
				if (schedule != null)
				{
					schedule.Enabled = m.Enabled;
					schedule.Type = m.Type;
					schedule.Value = m.Value;
				}
				else
				{
					db.IndexSchedule.Add(m);
				}
				db.SaveChanges();
			}
		}

		#endregion

		#region Index Live Monitoring

		public void StartMonitoring()
		{
			Model model = new Model();
			var dirs = model.IndexingPaths.Select(i => i.Path);
			foreach (var dir in dirs)
			{
				AddWatcher(dir);
			}
		}

		public void StopAllMonitors()
		{
			while (monitors.Count > 0)
			{
				var monitor = monitors[0];
				monitor.EnableRaisingEvents = false;
				monitor.Dispose();
				monitor = null;
				monitors.RemoveAt(0);
			}
		}

		private void AddWatcher(string path)
		{
			FileSystemWatcher watcher = new FileSystemWatcher();
			watcher.Path = path;
			watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName;
			watcher.Filter = "*.*";
			watcher.Created += Watcher_Created; ;
			watcher.Deleted += Watcher_Deleted; ;
			watcher.Renamed += Watcher_Renamed; ;
			watcher.IncludeSubdirectories = true;
			watcher.EnableRaisingEvents = true;

			monitors.Add(watcher);
		}

		private void Watcher_Created(object sender, FileSystemEventArgs e)
		{
			try
			{
				Thread.Sleep(500);
				AddIndexedItem(e.FullPath);
				Log.Write($"QFinder - File added to index - {e.FullPath}");
			}
			catch (Exception ex)
			{
				LogFailure($"QFinder - Error: {ex.Message}");
			}
		}

		private void Watcher_Deleted(object sender, FileSystemEventArgs e)
		{
			try
			{
				using (Model model = new Model())
				{
					var FileNameData = GetItemSegments(e.FullPath);

					if (FileNameData != null)
					{
						var file = model.Files.FirstOrDefault(i =>
						i.Path.ToLower().Equals(FileNameData.Path.ToLower()) &&
						i.Name.ToLower().Equals(FileNameData.Name.ToLower()) &&
						i.Extension.Equals(FileNameData.Extension));

						if (file != null)
						{
							model.Files.Remove(file);
							model.SaveChanges();
						}
						Log.Write($"QFinder - File removed from index - {e.FullPath}");
					}
				}
			}
			catch (Exception ex)
			{
				LogFailure($"QFinder - Error: {ex.Message}");
			}
		}

		private void Watcher_Renamed(object sender, RenamedEventArgs e)
		{
			try
			{
				using (Model model = new Model())
				{
					var oldFileNameData = GetItemSegments(e.OldFullPath);

					if (oldFileNameData != null)
					{
						var file = model.Files.FirstOrDefault(i =>
						i.Path.ToLower().Equals(oldFileNameData.Path.ToLower()) &&
						i.Name.ToLower().Equals(oldFileNameData.Name.ToLower()) &&
						i.Extension.Equals(oldFileNameData.Extension));

						if (file != null)
						{
							model.Files.Remove(file);
							model.SaveChanges();

							AddIndexedItem(e.FullPath);
						}
						Log.Write($"QFinder - File removed from index - {e.FullPath}");
					}

				}
			}
			catch (Exception ex)
			{
				LogFailure($"QFinder - Error: {ex.Message}");
			}
		}

		#endregion

	}
}
