using SQLite.CodeFirst;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.SQLite;
using System.Data.SQLite.EF6;

namespace QFinder.Data
{
 
    public class Model : DbContext
    {
        public Model() : base(App.DB.GetConnection(), true)
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<Model>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }

        public virtual DbSet<FileIndex> Files { get; set; }
        public virtual DbSet<FileIndexType> FileIndexTypes { get; set; }
        public virtual DbSet<IndexingPath> IndexingPaths { get; set; }
        public virtual DbSet<IndexSchedule> IndexSchedule { get; set; }
    }

    public class FileIndexType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class FileIndex
    {
        [Key]
        public int Id { get; set; }
        [Index]
        public string Name { get; set; }
        [Index]
        public string Extension { get; set; }
        [Index]
        public string Path { get; set; }
        public int TypeId { get; set; }
        [ForeignKey("TypeId")]
        public virtual FileIndexType Type { get; set; }
        public string FileName
        {
            get
            {
                if (Type.Name != "Folder")
                    return $"{Name}.{Extension}";
                else
                    return Name;
            }
        }
        public string FullPath
        {
            get
            {
                if (Type.Name != "Folder")
                    return $"{Path}\\{FileName}";
                else
                    return Path;
            }
        }
    }

    public class IndexingPath
    {
        [Key]
        public int Id { get; set; }
        public string Path { get; set; }
    }

    public class IndexSchedule
    {
        [Key]
        public int Id { get; set; }
        public bool Enabled { get; set; }
        public int Value { get; set; }
        public string Type { get; set; }
    }
}