using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogExplorer.DAL.Models
{
    public sealed class Catalog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public Catalog ParentCatalog { get; set; }
        public ICollection<Catalog> Catalogs { get; set; }
        public ICollection<File> Files { get; set; }

        public Catalog()
        {
            Files = new HashSet<File>();
            Catalogs = new HashSet<Catalog>();
        }
    }

    public class CatalogView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public IEnumerable<FileView> Files { get; set; }

    }
}