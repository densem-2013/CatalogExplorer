using AutoMapper;
using CatalogExplorer.DAL.Models;

namespace CatalogExplorer.DAL.AutoMapper
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<File, FileView>();
            CreateMap<Catalog, CatalogView>();
        }
    }

}
