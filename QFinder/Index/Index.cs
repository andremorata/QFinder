using QFinder.Data;
using QFinder.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QFinder.Index
{
    class Index
    {
        public void BuildIndex()
        {
            var indexTask = Task.Run(() =>
            {
                Model model = new Model();
                var dirs = model.IndexingPaths.Select(i => i.Path);
                var mapped = new List<string>();

                foreach (var dir in dirs)
                    mapped.AddRange(Map(dir));

                var toBeRemoved = new List<FileIndex>();
                foreach (var item in model.Files)
                    if (!mapped.Any(i => i == item.FullPath))
                        toBeRemoved.Add(item);

                model.Files.RemoveRange(toBeRemoved);
                model.SaveChanges();
            });
        }

        private string[] Map(string path, string term = "")
        {
            var ret = new List<string>();
            var firstDirectories = Directory.GetDirectories(path).Where(src => src.Contains(term));
            ret.AddRange(firstDirectories);
            foreach (var item in firstDirectories)
            {
                try { AddIndexedItem("Folder", item); }
                catch (Exception ex)
                {
                    LogFailure(ex.Message);
                }
            }

            foreach (var childDir in Directory.GetDirectories(path))
            {
                try
                {
                    AddIndexedItem("Folder", childDir);
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
                    AddIndexedItem("File", item);
                }
                catch (Exception ex)
                {
                    LogFailure(ex.Message);
                }
            }
            return ret.ToArray();
        }


        private void AddIndexedItem(string type, string path)
        {
            try
            {
                Model model = new Model();

                var itemType = model.FileIndexTypes.FirstOrDefault(i => i.Name == type);

                var itemExt = "";
                if (itemType.Name != "Folder" && path.LastIndexOf(".") >= 0)
                    itemExt = path.Substring(path.LastIndexOf("."));

                if (!model.Files.Any(i => i.FullPath == path))
                {
                    model.Files.Add(
                        new FileIndex()
                        {
                            Type = itemType,
                            Name = path.Substring(path.LastIndexOf("\\") + 1),
                            Extension = itemExt,
                            FullPath = path
                        });
                }
                else
                {
                    var file = model.Files.FirstOrDefault(i => i.FullPath == path);
                    if (file != null)
                    {
                        file.Type = itemType;
                        file.Name = path.Substring(path.LastIndexOf("\\") + 1);
                        file.Extension = itemExt;
                        file.FullPath = path;
                    }
                }
                model.SaveChanges();
            }
            catch (Exception ex)
            {
                LogFailure(ex.Message);
            }

        }

        private void LogFailure(string error)
        {
            Log.Write(EventLogEntryType.Error, error);
        }


    }
}
