using System.IO;
using System.Web.Hosting;
using System.Web.Http;
using AutoMapper;
using CatalogExplorer.DAL.Models;
using CatalogExplorer.DAL.UnitOfWork;
using CatalogExplorer.Web.Models;

namespace CatalogExplorer.Web.ApiControllers
{
    [RoutePrefix("api/catalog")]
    public class CatalogController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = Application.Mapper;

        public CatalogController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddCatalog(CatalogDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var catRepo = _unitOfWork.Repository<Catalog>();
            var catalog = _mapper.Map<CatalogDto, Catalog>(dto);
            catRepo.Create(catalog);
            catRepo.Save();
            
            var result = _mapper.Map<Catalog, CatalogResult>(catalog);

            Directory.CreateDirectory($"{HostingEnvironment.MapPath("~/")}/{catalog.Path}/{catalog.Name}");

            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public  IHttpActionResult RemoveCatalog(int id)
        {
            var catRepo = _unitOfWork.Repository<Catalog>();
            var catalog = catRepo.Get(id);
            if (catalog!=null)
            {
                catRepo.Delete(catalog);
                catRepo.Save();
                var path = $"{HostingEnvironment.MapPath("~/")}/{catalog.Path}/{catalog.Name}";
                if (Directory.Exists(path))
                {
                    ClearFolder(path);
                }
                return Ok();
            }
            return BadRequest($"Catalog with id = {id} not found!!!");
        }
        private void ClearFolder(string folderName)
        {
            DirectoryInfo dir = new DirectoryInfo(folderName);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                ClearFolder(di.FullName);
                di.Delete();
            }
        }

    }
}
