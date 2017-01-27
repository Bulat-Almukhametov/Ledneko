using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Web.Http;
using Ledneko.Domain.Abstract;
using Ledneko.Domain.Entities;

namespace Ledneko.WebUI.Controllers
{
    public class ValuesController : ApiController
    {
        private ISolutionRepository solutions;
        private ICategoryRepository categories;
        private ICatalogRepository catalogs;
        private IServiceRepository services;
        private readonly int itemsPerPage = 10;

        public ValuesController(ISolutionRepository sln, ICategoryRepository categ, ICatalogRepository catal, IServiceRepository srv)
        {
            solutions = sln;
            categories = categ;
            catalogs = catal;
            services = srv;
        }
        public IEnumerable<Catalog> GetCatalogs()
        {
            return catalogs.GetCatalogs;
        }

        public IEnumerable<Category> GetCategories(int catalogId)
        {
            return categories.GetCategories.Where(catg => catg.CatalogId == catalogId);
        }

        public HttpResponseMessage GetScrinshot(int detailsId, int scrinId)
        {
            Scrinshot prod = solutions.GetScrinshots(detailsId).FirstOrDefault(scrin=>scrin.Id==scrinId);
            if (prod != null)
            {
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StreamContent(new MemoryStream(prod.ImageData));
                result.Content.Headers.ContentType =
                    new MediaTypeHeaderValue(prod.MimeType);
                return result;
            }
            else
            {
                return null;
            }
        }

        public HttpResponseMessage GetLogo(int solutionId)
        {
            Solution prod = solutions.GetSolutions.FirstOrDefault(sln => sln.Id == solutionId);
            if (prod != null)
            {
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StreamContent(new MemoryStream(prod.DataImageBytes));
                result.Content.Headers.ContentType =
                    new MediaTypeHeaderValue(prod.MimeType);
                return result;
            }
            else
            {
                return null;
            }
        }

        public HttpResponseMessage GetSrvLogo(int srvId)
        {
            ImageFile logo = services.GetLogo(srvId);
            if (logo != null)
            {
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StreamContent(new MemoryStream(logo.ImageData));
                result.Content.Headers.ContentType =
                    new MediaTypeHeaderValue(logo.MimeType);
                return result;
            }
            else
            {
                return null;
            }
        }

        [ActionName("GetPicture")]
        public HttpResponseMessage GetPicture(int picId)
        {
            Picture picture = services.GetPictures.FirstOrDefault(pic => pic.Id == picId);
            if (picture != null)
            {
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StreamContent(new MemoryStream(picture.ImageData));
                result.Content.Headers.ContentType =
                    new MediaTypeHeaderValue(picture.MimeType);
                return result;
            }
            else
            {
                return null;
            }
        }

    }
}