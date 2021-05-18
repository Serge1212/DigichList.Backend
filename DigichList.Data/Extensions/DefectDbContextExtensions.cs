using DigichList.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DigichList.Infrastructure.Extensions
{
    public static class DefectDbContextExtensions
    {
        public static async Task<Defect> GetDefectWithAssignedDefectByIdAsync(this DbSet<Defect> defects, int defectId)
        {
            return await defects
                .Include(a => a.AssignedDefect)
                .FirstOrDefaultAsync(x => x.Id == defectId);
        }
    }
}
