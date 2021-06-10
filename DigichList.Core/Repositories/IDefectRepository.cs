using DigichList.Core.Entities;
using DigichList.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigichList.Core.Repositories
{
    public interface IDefectRepository : IRepository<Defect, int>
    {
        public IEnumerable<Defect> GetAllAsNoTracking();
        public Task<Defect> GetDefectWithAssignedDefectByIdAsync(int defectId);
        public Task DeleteRangeAsync(int[] idArr);
        public IEnumerable<Defect> GetRangeByIds(int[] idArr);
        public Task<AssignedDefect> GetAssignedDefectAsync(int userId, int defectId);
        public Task SaveAssignedDefect(AssignedDefect assignedDefect);
        public IEnumerable<Defect> GetDefectsWithUsersAndAssignedDefects();
    }
}
