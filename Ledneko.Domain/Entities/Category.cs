using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Ledneko.Domain.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int CatalogId { get; set; }
        public Catalog Catalog { get; set; }
        public ICollection<Solution> Solutions { get; set; }
    }
}
