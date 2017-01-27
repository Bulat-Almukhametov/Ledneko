using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ledneko.Domain.Entities
{
    public class Picture
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public byte[] ImageData { get; set; }
        public string MimeType { get; set; }

        public Service Service { get; set; }
    }
}
