using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ledneko.Domain.Entities
{
    public class ImageFile
    {
        public string MimeType { get; set; }
        public byte[] ImageData { get; set; }
    }
}
