using DigichList.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DigichList.Infrastructure.Extensions
{
    public static class AssignedDefectDbContextExtensions
    {
        public static IQueryable<AssignedDefect> GetAssignedDefectsByUserId(this DbSet<AssignedDefect> assignedDefects, int telegramId)
        {
            return assignedDefects
                .Include(d => d.Defect)
                .Include(a => a.AssignedWorker)
                .Where(x => x.AssignedWorker.TelegramId == telegramId);
        }
    }
}
