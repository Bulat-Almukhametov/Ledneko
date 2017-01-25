using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ledneko.WebUI.Areas.Admin.Models
{
    public class EditedSolutionModel
    {
        public int SolutionId { get; set; }
        public int ViewingDetailsId { get; set; }
        public int CategoryId { get; set; }

        public string Name { get; set; }
        public int Price { get; set; }
        public int OldPrice { get; set; }
        public string DemoUrl { get; set; }

        public string Header { get; set; }
        public string Description { get; set; }
    }
}