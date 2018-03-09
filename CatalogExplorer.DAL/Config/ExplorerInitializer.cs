using System.Data.Entity;

namespace CatalogExplorer.DAL.Config
{
    public class ExplorerInitializer : DropCreateDatabaseIfModelChanges<ExplorerContext>
    {
        protected override void Seed(ExplorerContext context)
        {
        }
    }
}