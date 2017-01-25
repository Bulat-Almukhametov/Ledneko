using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ledneko.Domain.Entities
{
    public class Scrinshot
    {
        public int Id { get; set; }
        public int SolutionViewingDetailsId { get; set; }
        public SolutionViewingDetails SolutionViewingDetails { get; set; }
        
        public byte[] ImageData { get; set; }
        public string MimeType { get; set; }
    }
}
