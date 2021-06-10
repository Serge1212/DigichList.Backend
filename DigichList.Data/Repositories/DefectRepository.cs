using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using DigichList.Infrastructure.Data;
using DigichList.Infrastructure.Extensions;
using DigichList.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigichList.Infrastructure.Repositories
{
    public class DefectRepository : Repository<Defect, int>, IDefectRepository
    {
        public DefectRepository(DigichListContext context) : base(context) { }


        public async Task DeleteRangeAsync(int[] idArr)
        {
            var defectsToDelete = GetRangeByIds(idArr);
            _context.RemoveRange(defectsToDelete);
            await SaveChangesAsync();
         }

        public IEnumerable<Defect> GetAllAsNoTracking()
        {
            return _context.Defects.AsNoTracking();
        }

        public async Task<Defect> GetDefectWithAssignedDefectByIdAsync(int defectId)
        {
            return await _context.Defects.GetDefectWithAssignedDefectByIdAsync(defectId);
        }

        public IEnumerable<Defect> GetRangeByIds(int[] idArr)
        {
            return _context.Defects.Where(d => idArr.Contains(d.Id));
        }
        public async Task<AssignedDefect> GetAssignedDefectAsync(int userId, int defectId)
        {
            var user = await _context.Users.GetUserByIdWithRole(userId);
            var defect = await GetByIdAsync(defectId);

            if(user?.Role?.Name != "Technician")
            {
                return null;
            }

            if(user != null || defect != null)
            {
                var assignedDefect = new AssignedDefect
                {
                    AssignedWorker = user,
                    Defect = defect
                };

                return assignedDefect;
            }
            else
            {
                return null;
            }
        }

        public async Task SaveAssignedDefect(AssignedDefect assignedDefect)
        {
            await _context.AssignedDefects.AddAsync(assignedDefect);
            await SaveChangesAsync();
        }

        public IEnumerable<Defect> GetDefectsWithUsersAndAssignedDefects()
        {
            return _context.Defects.GetDefectsWithUsersAndAssignedDefects();
        }
    }
}
