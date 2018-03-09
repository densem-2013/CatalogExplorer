using System.Data.Entity;
using CatalogExplorer.DAL.Models;

namespace CatalogExplorer.DAL.Config
{
    public class ExplorerContext : DbContext
    {
        public ExplorerContext()
            : base("name=ExplorerContext")
        {
        }

        static ExplorerContext()
        {
            Database.SetInitializer(new ExplorerInitializer());
        }
        public virtual DbSet<Catalog> Catalogs { get; set; }
        public virtual DbSet<File> Files { get; set; }
    }
}