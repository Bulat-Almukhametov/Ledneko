using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ledneko.Domain.Entities;

namespace Ledneko.Domain.Abstract
{
    public interface ICatalogRepository
    {
        IQueryable<Catalog> GetCatalogs { get; }
        bool SaveCatalog(Catalog catalog);
        bool DeleteCatalog(int catalogId);
    }
}
