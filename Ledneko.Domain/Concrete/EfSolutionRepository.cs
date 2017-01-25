using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ledneko.Domain.Abstract;
using Ledneko.Domain.Entities;

namespace Ledneko.Domain.Concrete
{
    public class EfSolutionRepository : ISolutionRepository
    {
        private EfSoluionsContext context;

        public EfSolutionRepository(EfSoluionsContext context)
        {
            this.context = context;
        }

        public IQueryable<Solution> GetSolutions
        {
            get { return context.Solutions; }
        }

        public bool SaveSolution(Solution solution, SolutionViewingDetails svdetails)
        {
            try
            {
                if (solution.Id == 0)
                {
                    var details = context.Details.Add(svdetails);
                    solution.SolutionViewingDetailsId = details.Id;

                    context.Solutions.Add(solution);
                }
                else
                {
                    Solution dbEntry = context.Solutions.Find(solution.Id);
                    if (dbEntry != null)
                    {
                        dbEntry.Name = solution.Name;
                        dbEntry.CategoryId = solution.CategoryId;
                        dbEntry.DemoUrl = solution.DemoUrl;
                        dbEntry.Price = solution.Price;
                        dbEntry.OldPrice = solution.OldPrice;

                        var details = context.Details.Find(dbEntry.SolutionViewingDetailsId);
                        details.Description = svdetails.Description;
                        details.Header = svdetails.Header;
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

        public bool SaveLogo(int solutionId, string mimeType, byte[] imageBytes)
        {
            try
            {
                Solution dbEntry = context.Solutions.Find(solutionId);
                if (dbEntry != null)
                {

                    dbEntry.MimeType = mimeType;
                    dbEntry.DataImageBytes = imageBytes;

                    context.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SaveScrin(int detailsId, string mimeType, byte[] imageBytes)
        {
            if (context.Details.FirstOrDefault(det => det.Id == detailsId) == null)
                return false;
            try
            {
                Scrinshot dbEntry = new Scrinshot
                    {
                        SolutionViewingDetailsId = detailsId,
                        MimeType = mimeType,
                        ImageData = imageBytes
                    };
                context.Scrinshots.Add(dbEntry);
                context.SaveChanges();
                return true;


            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteScrin(int detailsId, int scrinId)
        {
            if (context.Details.FirstOrDefault(det => det.Id == detailsId) == null)
                return false;

            Scrinshot dbEntry = GetScrinshots(detailsId).FirstOrDefault(scrin => scrin.Id == scrinId);
            if (dbEntry == null)
                return false;
            else
            {
                context.Scrinshots.Remove(dbEntry);
                context.SaveChanges();
                return true;
            }
        }

        public bool DeleteSolution(int solutionId)
        {
            Solution dbEntry = context.Solutions.Find(solutionId);
            if (dbEntry == null)
                return false;
            else
            {
                context.Solutions.Remove(dbEntry);
                context.SaveChanges();
                return true;
            }
        }

        public SolutionViewingDetails GetDetails(Solution sln)
        {
            if (sln != null)
                return context.Details.Find(sln.SolutionViewingDetailsId);
            else
                return null;

        }

        public IQueryable<Scrinshot> GetScrinshots(int detailsId)
        {
            return context.Scrinshots.Where(scrin => scrin.SolutionViewingDetailsId == detailsId);
        }
    }
}
