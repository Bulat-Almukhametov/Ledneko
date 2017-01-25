using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ledneko.Domain.Entities;

namespace Ledneko.WebUI.Areas.Admin.Models
{
    public class EditSolutionModel
    {
        public Solution Solution { get; set; }
        public int CurentCatalog { get; set; }
        public IEnumerable<Catalog> Catalogs { get; set; }
        public int CurrentCategory { get; set; }
        public IEnumerable<Category> Categories { get; set; }  
        public IQueryable<int> Ids { get; set; }
    }
}