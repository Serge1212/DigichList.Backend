using DigichList.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigichList.Infrastructure.Extensions
{
    public static class DefectDbContextExtensions
    {
        public static async Task<Defect> GetDefectWithAssignedDefectByIdAsync(this DbSet<Defect> defects, int defectId)
        {
            return await defects
                .Include(a => a.AssignedDefect)
                .ThenInclude(w => w.AssignedWorker)
                .FirstOrDefaultAsync(x => x.Id == defectId);
        }

        public static IEnumerable<Defect> GetDefectsWithUsersAndAssignedDefects(this DbSet<Defect> defects)
        {
            return defects
                .Include(p => p.Publisher)
                .Include(a => a.AssignedDefect)
                .ThenInclude(u => u.AssignedWorker);
        }
    }
}
