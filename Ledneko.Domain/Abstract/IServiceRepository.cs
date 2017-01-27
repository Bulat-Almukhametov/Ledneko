using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Ledneko.Domain.Entities;

namespace Ledneko.Domain.Abstract
{
    public interface IServiceRepository
    {
        IQueryable<Service> GetServices { get; }
        bool SaveService(Service service);
        bool DeleteService(int serviceId);
        bool SaveLogo(int serviceId, byte[] imageData, string mimeType);
        ImageFile GetLogo(int serviceId);
        IQueryable<Picture> GetPictures { get; }
        bool SavePicture(Picture picture);
        bool DeletePicture(int picId);
    }
}
