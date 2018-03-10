using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using AutoMapper;
using CatalogExplorer.DAL.Models;
using CatalogExplorer.DAL.UnitOfWork;

namespace CatalogExplorer.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = Application.Mapper;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(int? id = null)
        {
            var catRepo = _unitOfWork.Repository<Catalog>();

            var view = new MainView
            {
                Name = "Fake Catalog"
            };

            if (id == null)
            {
                id = catRepo.FirstOrDefault(c => c.ParentId == null)?.Id;
            }

            var curCatalog = catRepo.FirstOrDefault(c => c.Id == id, null, new List<Expression<Func<Catalog, object>>>
            {
                c => c.Catalogs,
                c => c.Files
            });

            if (curCatalog == null)
            {
                return View(view);
            }

            view = _mapper.Map<Catalog, MainView>(curCatalog);

            return View(view);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}