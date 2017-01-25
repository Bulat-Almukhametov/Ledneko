using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Ledneko.Domain.Abstract;
using Ledneko.Domain.Entities;

namespace Ledneko.Domain.Concrete
{
    public class EfCatalogRepository : ICatalogRepository
    {
        private EfSoluionsContext context;

        public EfCatalogRepository(EfSoluionsContext context)
        {
            this.context = context;
        }

        public IQueryable<Catalog> GetCatalogs
        {
            get { return context.Catalogs; }
        }

        public bool SaveCatalog(Catalog catalog)
        {
            try
            {
                if (catalog.Id == 0)
                {
                    context.Catalogs.Add(catalog);
                }
                else
                {
                    Catalog dbEntry = context.Catalogs.Find(catalog.Id);
                    if (dbEntry != null)
                    {
                        dbEntry.Name = catalog.Name;
                        dbEntry.Description = catalog.Description;
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

        public bool DeleteCatalog(int catalogId)
        {
            Catalog dbEntry = context.Catalogs.Find(catalogId);
            if (dbEntry == null)
                return false;
            else
            {
                context.Catalogs.Remove(dbEntry);
                context.SaveChanges();
                return true;
            }
        }
    }
}
