using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CatalogExplorer.DAL.Models;

namespace CatalogExplorer.Web.Models
{
    public class CatexContext : DbContext
    {
        public CatexContext()
            : base("name=CatexContext")
        {
        }

        static CatexContext()
        {
            Database.SetInitializer<CatexContext>(new CatexDbInitializer());
        }
        public virtual DbSet<Catalog> Catalogs { get; set; }
    }
}