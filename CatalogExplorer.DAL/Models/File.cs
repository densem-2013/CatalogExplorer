using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogExplorer.DAL.Models
{
    public class File
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int CatalogId { get; set; }
        [ForeignKey("CatalogId")]
        public virtual Catalog Catalog { get; set; }
    }

    public class FileView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CatalogId { get; set; }
    }
}
