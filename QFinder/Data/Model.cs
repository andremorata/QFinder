namespace QFinder.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Linq;

    public class Model : DbContext
    {
        public Model()
            : base(Program.DB.GetConnectionString())
        {
           
        }
                
        public virtual DbSet<FileIndex> Files { get; set; }
        public virtual DbSet<FileIndexType> FileIndexTypes { get; set; }
        public virtual DbSet<IndexingPath> IndexingPaths { get; set; }
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
        public string Name { get; set; }
        public string Extension { get; set; }
        public string FullPath { get; set; }
        public FileIndexType Type { get; set; }
    }

    public class IndexingPath
    {
        [Key]
        public int Id { get; set; }
        public string Path { get; set; }
    }
}