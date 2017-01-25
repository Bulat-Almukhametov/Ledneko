﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Web.Http;
using System.Web.Mvc;
using Ledneko.Domain.Abstract;
using Ledneko.Domain.Entities;

namespace Ledneko.WebUI.Controllers
{
    public class ValuesController : ApiController
    {
        private ISolutionRepository solutions;
        private ICategoryRepository categories;
        private ICatalogRepository catalogs;
        private readonly int itemsPerPage = 10;

        public ValuesController(ISolutionRepository sln, ICategoryRepository categ, ICatalogRepository catal)
        {
            solutions = sln;
            categories = categ;
            catalogs = catal;
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
    }
}