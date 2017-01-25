using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ledneko.Domain.Abstract;
using Ledneko.Domain.Entities;
using Ledneko.WebUI.Areas.Admin.Models;
using Microsoft.Ajax.Utilities;

namespace Ledneko.WebUI.Areas.Admin.Controllers
{
    public class EditController : Controller
    {
        private ISolutionRepository solutions;
        private ICategoryRepository categories;
        private ICatalogRepository catalogs;

        public EditController(ISolutionRepository sln, ICategoryRepository categ, ICatalogRepository catal)
        {
            solutions = sln;
            categories = categ;
            catalogs = catal;
        }

        [HttpPost]
        public string DeleteSolution(int solutionId)
        {
            var result = solutions.DeleteSolution(solutionId);

            return result ? "success" : "error occured";
        }

        public ActionResult Solution(int solutionId = -1)
        {
            Solution editItem;
            IQueryable<int> ids = (new int[0]).AsQueryable();
            if (solutionId < 0)
            {
                editItem = new Solution();
                editItem.SolutionViewingDetails = new SolutionViewingDetails();
            }
            else
            {
                editItem = solutions.GetSolutions.FirstOrDefault(sln => sln.Id == solutionId);
                var details = solutions.GetDetails(editItem);
                editItem.SolutionViewingDetails = details;
                ids = solutions.GetScrinshots(details.Id).Select(scrin => scrin.Id);
            }

            var catalogId = solutionId < 0
                ? catalogs.GetCatalogs.First().Id
                : categories.GetCategories.First(categ => categ.Id == editItem.CategoryId).CatalogId;

            var result = new EditSolutionModel
            {
                Solution = editItem,
                Catalogs = catalogs.GetCatalogs,
                Categories = categories.GetCategories.Where(categ => categ.CatalogId == catalogId),
                CurrentCategory = editItem.CategoryId,
                CurentCatalog = catalogId,
                Ids = ids
            };


            return View(result);
        }

        [HttpPost]
        public RedirectToRouteResult SaveLogo(int solutionId, HttpPostedFileBase image)
        {
            if (image != null)
            {
                var DataImageBytes = new byte[image.ContentLength];
                image.InputStream.Read(DataImageBytes, 0, image.ContentLength);

                if (solutions.SaveLogo(solutionId, image.ContentType, DataImageBytes))
                    TempData["message"] = string.Format("\"Логотип\" был сохранен в Базе Данных");
                else
                    TempData["message"] = string.Format("Не удалось сохранить \"логотип\" в Базе Данных");
            }

            return RedirectToAction("Solution", new { solutionId = solutionId });
        }

        [HttpPost]
        public string AddScrinshot(int detailsId, HttpPostedFileBase image)
        {
            if (image != null)
            {
                var DataImageBytes = new byte[image.ContentLength];
                image.InputStream.Read(DataImageBytes, 0, image.ContentLength);

                if (solutions.SaveScrin(detailsId, image.ContentType, DataImageBytes))
                    return solutions.GetScrinshots(detailsId).OrderByDescending(scrin => scrin.Id).FirstOrDefault().Id.ToString();
            }
            return "error occured";
        }

        [HttpPost]
        public string DeleteScrinshot(int scrinId, int detailsId)
        {
            if (solutions.DeleteScrin(detailsId, scrinId))
                return "success";
            else
                return "error occured";
        }

        [HttpPost]
        public ActionResult Solution(EditedSolutionModel inputParams)
        {
            var sln = new Solution
            {
                Id = inputParams.SolutionId,
                CategoryId = inputParams.CategoryId,
                DemoUrl = inputParams.DemoUrl,
                Name = inputParams.Name,
                Price = inputParams.Price,
                OldPrice = inputParams.OldPrice,
                SolutionViewingDetailsId = inputParams.ViewingDetailsId
            };

            var details = new SolutionViewingDetails
            {
                Id = inputParams.ViewingDetailsId,
                Header = inputParams.Header,
                Description = inputParams.Description
            };

            if (!ModelState.IsValid || !solutions.SaveSolution(sln, details))
            {
                sln.SolutionViewingDetails = details;
                var model = new EditSolutionModel
                {

                };
                return View(model);
            }

            return RedirectToAction("Solutions", "List");
        }


        public ActionResult Category(int catid = 0)
        {
            Category category;
            if(catid == 0)
            category = new Category();
            else
            {
                category = categories.GetCategories.FirstOrDefault(cat => cat.Id == catid);
            }

            if (category == null)
                return RedirectToAction("Categories", "List");
            EditCategoryModel editCategoryModel = new EditCategoryModel
            {
                Category = category,
                Catalogs = catalogs.GetCatalogs
            };

            return View(editCategoryModel);
        }

        [HttpPost]
        public string DeleteCategory(int catid = 0)
        {
            var result = categories.DeleteCategory(catid);

            return result ? "success" : "error occured";
        }

        [HttpPost]
        public ActionResult SaveCategory(Category cat)
        {
            categories.SaveCategory(cat);


            return RedirectToAction("Categories", "List");
        }

        public ActionResult Catalog(int catid = 0)
        {
            Catalog catalog;
            if (catid == 0)
                catalog = new Catalog();
            else
            {
                catalog = catalogs.GetCatalogs.FirstOrDefault(cat => cat.Id == catid);
            }

            if (catalog == null)
                return RedirectToAction("Catalogs", "List");

            return View(catalog);
        }

        [HttpPost]
        public string DeleteCatalog(int catid = 0)
        {
            var result = catalogs.DeleteCatalog(catid);

            return result ? "success" : "error occured";
        }

        [HttpPost]
        public ActionResult SaveCatalog(Catalog cat)
        {
            catalogs.SaveCatalog(cat);


            return RedirectToAction("Catalogs", "List");
        }
    }
}
