using AutoMapper;
using CatalogExplorer.Web.AutoMapper;

namespace CatalogExplorer.Web
{
    public static class Application
    {
        public static readonly IMapper Mapper = MappingProfile.InitializeAutoMapper().CreateMapper();
    }
}
