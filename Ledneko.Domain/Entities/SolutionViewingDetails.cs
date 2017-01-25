using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Ledneko.Domain.Entities
{
    public class SolutionViewingDetails
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Заголовок")]
        public string Header { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        public IList<Scrinshot> Scrinshots { get; set; } 

    }
}
