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
    }
}
