using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ledneko.Domain.Entities;

namespace Ledneko.Domain.Abstract
{
    public interface ICategoryRepository
    {
        IQueryable<Category> GetCategories { get; }
        bool SaveCategory(Category category);
        bool DeleteCategory(int categoryId);
    }
}
