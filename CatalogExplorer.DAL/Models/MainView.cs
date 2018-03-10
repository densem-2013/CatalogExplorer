using System.Collections.Generic;

namespace CatalogExplorer.DAL.Models
{
    public class MainView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public string Path { get; set; }
        public IEnumerable<CatalogView> Catalogs { get; set; }
        public IEnumerable<FileView> Files { get; set; }
    }
}