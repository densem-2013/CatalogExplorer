using AutoMapper;
using CatalogExplorer.DAL.AutoMapper;

namespace CatalogExplorer.DAL
{
    public static class Application
    {
        public static readonly IMapper Mapper = MappingProfile.InitializeAutoMapper().CreateMapper();
    }
}
