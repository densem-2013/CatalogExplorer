using System.Data.Entity;
using CatalogExplorer.DAL.Models;

namespace CatalogExplorer.DAL
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            modelBuilder.Entity<Catalog>()
                .HasMany(s => s.Catalogs)
                .WithOptional(g => g.ParentCatalog)
                .HasForeignKey(s => s.ParentId);

            modelBuilder.Entity<Catalog>()
                .HasMany(c => c.Files)
                .WithRequired(f => f.Catalog)
                .HasForeignKey(f => f.CatalogId)
                .WillCascadeOnDelete();


        }
    }
}