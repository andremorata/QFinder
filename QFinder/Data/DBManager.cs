using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QFinder.Data
{
    public class DB
    {
        private string _folder;
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
            return $"Data Source={Folder}Index.sdf;Password=Holy!QFinder!!!";
        }

        public bool Check()
        {
            return File.Exists($"{Folder}Index.sdf");
        }

        public void CreateDB()
        {
            string connStr = GetConnectionString();
            SqlCeConnection conn = new SqlCeConnection(connStr);

            if (!Directory.Exists(Folder)) Directory.CreateDirectory(Folder);

            if (!File.Exists($"{Folder}Index.sdf"))
            {
                try
                {
                    SqlCeEngine engine = new SqlCeEngine(connStr);
                    engine.CreateDatabase();
                    CheckDbStructure();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        
        public bool CheckDbStructure()
        {
            try
            {
                Model m = new Model();                
                if (!m.IndexingPaths.Any())
                {
                    m.IndexingPaths.Add(new IndexingPath() { Path = @"D:\Files" });
                    m.IndexingPaths.Add(new IndexingPath() { Path = @"D:\Install" });
                    m.IndexingPaths.Add(new IndexingPath() { Path = @"D:\Pictures" });
                    m.IndexingPaths.Add(new IndexingPath() { Path = @"D:\Music" });
                    m.IndexingPaths.Add(new IndexingPath() { Path = @"D:\Videos" });
                    m.IndexingPaths.Add(new IndexingPath() { Path = @"D:\Projetos" });
                    m.IndexingPaths.Add(new IndexingPath() { Path = @"D:\RDP" });

                    m.SaveChanges();
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