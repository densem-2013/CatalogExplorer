using System.Collections.Generic;
using System.Data.Entity;
using CatalogExplorer.DAL.Models;

namespace CatalogExplorer.DAL
{
    public class ExplorerInitializer : DropCreateDatabaseIfModelChanges<ExplorerContext>
    {
        protected override void Seed(ExplorerContext context)
        {
            Catalog catalog = new Catalog
            {
                Name = "Creating Digital Images",
                Path = "/Catalogs",
                Catalogs = new List<Catalog>
                {
                    new Catalog
                    {
                        Name = "Resources",
                        Path = "/Catalogs/Creating Digital Images",
                        Catalogs = new List<Catalog>
                        {
                            new Catalog
                            {
                                Name = "Primary Sources",
                                Path = "/Catalogs/Creating Digital Images/Resources"
                            },
                            new Catalog
                            {
                                Name = "Secondary Sources",
                                Path = "/Catalogs/Creating Digital Images/Resources"
                            }
                        }
                    },
                    new Catalog
                    {
                        Name = "Evidence",
                        Path = "/Catalogs/Creating Digital Images",
                    },
                    new Catalog
                    {
                        Name = "Graphic Products",
                        Path = "/Catalogs/Creating Digital Images",
                        Catalogs = new List<Catalog>
                        {
                            new Catalog
                            {
                                Name = "Process",
                                Path = "/Catalogs/Creating Digital Images/Graphic Products"
                            },
                            new Catalog
                            {
                                Name = "Final Product",
                                Path = "/Catalogs/Creating Digital Images/Graphic Products"
                            }
                        }
                    },
                }
            };

            context.Catalogs.Add(catalog);
            context.SaveChanges();
        }
    }
}