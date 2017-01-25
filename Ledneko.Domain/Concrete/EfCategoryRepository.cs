using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ledneko.Domain.Abstract;
using Ledneko.Domain.Entities;

namespace Ledneko.Domain.Concrete
{
    public class EfCategoryRepository : ICategoryRepository
    {
        private EfSoluionsContext context;

        public EfCategoryRepository(EfSoluionsContext context)
        {
            this.context = context;
        }
        public IQueryable<Category> GetCategories
        {
            get { return context.Categories; }
        }
        
        public bool SaveCategory(Category category)
        {
            try
            {
                if (category.Id == 0)
                {
                    context.Categories.Add(category);
                }
                else
                {
                    Category dbEntry = context.Categories.Find(category.Id);
                    if (dbEntry != null)
                    {
                        dbEntry.Name = category.Name;
                        dbEntry.CatalogId = category.CatalogId;
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

        public bool DeleteCategory(int categoryId)
        {
            Category dbEntry = context.Categories.Find(categoryId);
            if (dbEntry == null)
                return false;
            else
            {
                context.Categories.Remove(dbEntry);
                context.SaveChanges();
                return true;
            }
        }
    }
}
