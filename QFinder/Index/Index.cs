using QFinder.Data;
using QFinder.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QFinder.Index
{
    class Index
    {
        public void BuildIndex()
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
                    if (!mapped.Any(i => i == item.FullPath))
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

                var type = GetIfIsFileOrFolder(path);

                if (type == null) return;

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

                if (!model.Files.Any(i => i.Path == folder && i.Name == name && i.Extension == ext))
                {
                    model.Files.Add(
                        new FileIndex()
                        {
                            Type = itemType,
                            Name = name,
                            Extension = ext,
                            Path = folder
                        });
                }
                model.SaveChanges();
            }
            catch (Exception ex)
            {
                LogFailure(ex.Message + "\r\n" + path);
            }

        }

        private void LogFailure(string error)
        {
            Log.Write(EventLogEntryType.Error, error);
        }

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
                    var file = model.Files.FirstOrDefault(i =>
                        i.FullPath.Equals(e.FullPath, StringComparison.InvariantCultureIgnoreCase));
                    if (file != null)
                    {
                        model.Files.Remove(file);
                        model.SaveChanges();
                    }
                    Log.Write($"QFinder - File removed from index - {e.FullPath}");
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
                    var file = model.Files.FirstOrDefault(i =>
                        i.FullPath.Equals(e.OldFullPath, StringComparison.InvariantCultureIgnoreCase));
                    if (file != null)
                    {
                        model.Files.Remove(file);
                        model.SaveChanges();

                        AddIndexedItem(e.FullPath);
                    }
                    Log.Write($"QFinder - File removed from index - {e.FullPath}");
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
