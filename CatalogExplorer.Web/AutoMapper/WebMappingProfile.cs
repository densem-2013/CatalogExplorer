using AutoMapper;
using CatalogExplorer.DAL.Models;
using CatalogExplorer.Web.Models;

namespace CatalogExplorer.Web.AutoMapper
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<File, FileView>();
            CreateMap<Catalog, CatalogView>();
            CreateMap<Catalog, MainView>();
            CreateMap<CatalogDto, Catalog>();
            CreateMap<Catalog, CatalogResult>();
        }
    }

}
