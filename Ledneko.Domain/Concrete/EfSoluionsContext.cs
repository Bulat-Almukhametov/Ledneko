using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ledneko.Domain.Entities;

namespace Ledneko.Domain.Concrete
{
    public class EfSoluionsContext : DbContext
    {
        public EfSoluionsContext()
        {
           // Database.SetInitializer(new MigrateDatabaseToLatestVersion<EfSoluionsContext, Configuration>("Ledneko"));
        }

        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Solution> Solutions { get; set; } 
        public DbSet<SolutionViewingDetails> Details { get; set; }
        public DbSet<Scrinshot> Scrinshots { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }

}
