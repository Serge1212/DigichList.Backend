using DigichList.Core.Entities;
using DigichList.Core.Repositories.Base;
using System.Linq;

namespace DigichList.Core.Repositories
{
    public interface IAssignedDefectRepository : IRepository<AssignedDefect, int>
    {
        public IQueryable<AssignedDefect> GetAssignedDefectsByUserId(int telegramId);
    }
}
