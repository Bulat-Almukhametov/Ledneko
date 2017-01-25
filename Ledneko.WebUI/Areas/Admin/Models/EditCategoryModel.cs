using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ledneko.Domain.Entities;

namespace Ledneko.WebUI.Areas.Admin.Models
{
    public class EditCategoryModel
    {
        public IQueryable<Catalog> Catalogs { get; set; }
        public Category Category { get; set; }
    }
}