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
            return $"Data Source={Folder}Index.sdf;Password=Holy!QFinder!!!;LCID=1033;";
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
                    m.IndexingPaths.Add(new IndexingPath() { Path = @"D:\RDP" });
                    m.IndexingPaths.Add(new IndexingPath() { Path = @"D:\Files\OneDrive" });
                    m.IndexingPaths.Add(new IndexingPath() { Path = @"D:\Files\PM" });
                    m.IndexingPaths.Add(new IndexingPath() { Path = @"D:\Files\Google Drive\Project Manager - Projetos" });
                    m.IndexingPaths.Add(new IndexingPath() { Path = @"D:\Files\Google Drive\Project Manager - Propostas Comerciais" });
                    m.IndexingPaths.Add(new IndexingPath() { Path = @"D:\Files\Google Drive\Project Manager - Institucional" });
                    m.IndexingPaths.Add(new IndexingPath() { Path = @"D:\Files\Google Drive\Project Manager - Diretoria" });
                    m.IndexingPaths.Add(new IndexingPath() { Path = @"D:\Files\Google Drive\Project Manager - Desenvolvimento" });
                    m.IndexingPaths.Add(new IndexingPath() { Path = @"D:\Files\Google Drive\Learning" });
                    m.IndexingPaths.Add(new IndexingPath() { Path = @"D:\Files\Google Drive\Andre" });
                    m.IndexingPaths.Add(new IndexingPath() { Path = @"D:\Pictures" });

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