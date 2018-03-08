using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CatalogExplorer.Web.Models
{
    public class CatexDbInitializer : DropCreateDatabaseIfModelChanges<CatexContext>
    {
        protected override void Seed(CatexContext context)
        {
        }
    }
}