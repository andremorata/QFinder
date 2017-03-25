using System;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;


namespace QFinder.Data
{
    public class DB
    {
        private string _folder;
        private const string dbName = "QFinder.Index.db3";

        public string Folder
        {
            get
            {
                return _folder;
            }
            set
            {
                if (!value.EndsWith("\\")) _folder = $"{value}\\";
            }
        }

        public string GetConnectionString()
        {
            return $"Data Source={Folder}{dbName};"; // ;Password=Holy!QFinder!!!;LCID=1033;";
        }

        public DbConnection GetConnection()
        {
            return new SQLiteConnection(GetConnectionString());
        }

        public bool Check()
        {
            return File.Exists($"{Folder}{dbName}");
        }

        public void CreateDB()
        {
            string connStr = GetConnectionString();
            SQLiteConnection conn = new SQLiteConnection(connStr);

            if (!Directory.Exists(Folder)) Directory.CreateDirectory(Folder);

            if (!File.Exists($"{Folder}{dbName}"))
            {
                try
                {
                    CheckDbStructure();
                }
                catch (Exception ex) { throw ex; }
            }
        }
        
        public bool CheckDbStructure()
        {
            try
            {
                Model m = new Model();
                if (!m.IndexSchedule.Any())
                {
                    m.IndexSchedule.Add(
                        new IndexSchedule()
                        {
                            Type = "Days",
                            Value = 1
                        });
                }

                if (!m.FileIndexTypes.Any())
                {
                    m.FileIndexTypes.Add(new FileIndexType() { Name = "File" });
                    m.FileIndexTypes.Add(new FileIndexType() { Name = "Folder" });

                    m.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
            return true;
        }
    }

}