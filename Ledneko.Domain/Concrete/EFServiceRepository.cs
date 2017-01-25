using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ledneko.Domain.Abstract;
using Ledneko.Domain.Entities;

namespace Ledneko.Domain.Concrete
{
    public class EFServiceRepository : IServiceRepository
    {
        private EfSoluionsContext context;

        public EFServiceRepository(EfSoluionsContext context)
        {
            this.context = context;
        }

        public IQueryable<Service> GetServices
        {
            get { return context.Services; }
        }

        public bool SaveService(Service service)
        {
            try
            {
                if (service.Id == 0)
                {
                    context.Services.Add(service);
                }
                else
                {
                    Service dbEntry = context.Services.Find(service.Id);
                    if (dbEntry != null)
                    {
                        dbEntry.Name = service.Name;
                        dbEntry.Description = service.Description;
                        dbEntry.Content = service.Content;
                    }
                }
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteService(int serviceId)
        {
            Service dbEntry = context.Services.Find(serviceId);
            if (dbEntry == null)
                return false;
            else
            {
                context.Services.Remove(dbEntry);
                context.SaveChanges();
                return true;
            }
        }

        public bool SaveLogo(int serviceId, byte[] imageData, string mimeType)
        {

            try
            {
                if (serviceId == 0)
                {
                    return false;
                }
                else
                {
                    Service dbEntry = context.Services.Find(serviceId);
                    if (dbEntry != null)
                    {
                        dbEntry.MimeType = mimeType;
                        dbEntry.ImageData = imageData;
                    }
                }
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public ImageFile GetLogo(int serviceId)
        {
            try
            {
                if (serviceId == 0)
                {
                    return null;
                }
                else
                {
                    Service dbEntry = context.Services.Find(serviceId);
                    ImageFile result;
                    if (dbEntry != null)
                    {
                        result = new ImageFile
                        {
                            ImageData = dbEntry.ImageData,
                            MimeType = dbEntry.MimeType
                        };
                        return result;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
