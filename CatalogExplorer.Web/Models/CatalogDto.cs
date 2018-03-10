using System.ComponentModel.DataAnnotations;

namespace CatalogExplorer.Web.Models
{
    public class CatalogDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Path { get; set; }
        public int? ParentId { get; set; }
    }
    public class CatalogResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}