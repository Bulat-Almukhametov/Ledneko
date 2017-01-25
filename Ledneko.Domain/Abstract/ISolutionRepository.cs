using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ledneko.Domain.Entities;

namespace Ledneko.Domain.Abstract
{
    public interface ISolutionRepository
    {
        IQueryable<Solution> GetSolutions { get; }
        bool SaveSolution(Solution solution, SolutionViewingDetails details);
        bool SaveLogo(int solutionId, string mimeType, byte[] imageBytes);
        bool SaveScrin(int detailsId, string mimeType, byte[] imageBytes);
        bool DeleteSolution(int solutionId);
        bool DeleteScrin(int detailsId, int scrinId);
        SolutionViewingDetails GetDetails(Solution sln);
        IQueryable<Scrinshot> GetScrinshots(int detailsId);
    }
}
