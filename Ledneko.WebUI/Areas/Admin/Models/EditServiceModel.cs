using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ledneko.Domain.Entities;

namespace Ledneko.WebUI.Areas.Admin.Models
{
    public class EditServiceModel
    {
        public Service Service { get; set; }
        public IQueryable<Picture> Images { get; set; } 
    }
}