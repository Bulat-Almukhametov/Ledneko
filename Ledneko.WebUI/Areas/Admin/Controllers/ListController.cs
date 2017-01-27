using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ledneko.Domain.Abstract;
using Ledneko.Domain.Entities;

namespace Ledneko.WebUI.Areas.Admin.Controllers
{
    [Authorize]
    public class ListController : Controller
    {
        private ISolutionRepository solutions;
        private ICategoryRepository categories;
        private ICatalogRepository catalogs;
        private IServiceRepository services;

        private readonly int itemsPerPage = 10;

        public ListController(ISolutionRepository sln, ICategoryRepository categ, ICatalogRepository catal, IServiceRepository srv)
        {
            solutions = sln;
            categories = categ;
            catalogs = catal;
            services = srv;
        }

        public ActionResult Menu(string currentTab)
        {
            return View((object)currentTab);
        }

        public ActionResult Solutions(int page = 1, string name = null)
        {
            IQueryable<object> collection = solutions.GetSolutions.Join(categories.GetCategories, sln => sln.CategoryId, cat => cat.Id, (sln, cat) => new
            {
                Id = sln.Id,
                Name = sln.Name,
                Category = cat,
                Price = sln.Price,
                OldPrice = sln.OldPrice,
                DataImageBytes = sln.DataImageBytes,
                DemoUrl = sln.DemoUrl,
                MimeType = sln.MimeType,
                SolutionViewingDetailsId = sln.SolutionViewingDetailsId
            }).Where(sln => name == null || sln.Name.Contains(name)).OrderBy(sln => sln.Id).Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
            return View(collection);
        }

        public ActionResult Categories(int page = 1, string name = null)
        {
            IQueryable<object> collection = categories.GetCategories.Join(catalogs.GetCatalogs, category => category.CatalogId, catalog => catalog.Id, (category, catalog) => new
            {
                Id = category.Id,
                Name = category.Name,
                CatalogName = catalog.Name
            }).Where(cat => name == null || cat.Name.Contains(name)).OrderBy(cat => cat.Id).Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
            return View(collection);
        }


        public ActionResult Catalogs(int page = 1, string name = null)
        {
            var collection = catalogs.GetCatalogs.Where(cat => name == null || cat.Name.Contains(name)).OrderBy(cat => cat.Id).Skip((page - 1) * itemsPerPage).Take(itemsPerPage);

            return View(collection);
        }

        public ActionResult Services(int page = 1, string name = null)
        {
            var collection = services.GetServices.Where(serv => name == null || serv.Name.Contains(name)).OrderBy(serv => serv.Id).Skip((page - 1) * itemsPerPage).Take(itemsPerPage);

            return View(collection); 
        }

    }
}
