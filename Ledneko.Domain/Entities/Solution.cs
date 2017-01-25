using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ledneko.Domain.Entities
{
    public class Solution
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int SolutionViewingDetailsId { get; set; }
        public SolutionViewingDetails SolutionViewingDetails { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Цена шаблона")]
        public decimal Price { get; set; }
        [Display(Name = "Цена без скидки")]
        public int? OldPrice { get; set; }
        [Display(Name = "Ссылка на Демо сайта")]
        public string DemoUrl { get; set; }
        public byte[] DataImageBytes { get; set; }
        public string MimeType { get; set; }

        public Category Category { get; set; }
    }
}
