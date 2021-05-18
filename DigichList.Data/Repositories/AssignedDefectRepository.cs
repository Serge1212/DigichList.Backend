using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using DigichList.Infrastructure.Data;
using DigichList.Infrastructure.Extensions;
using DigichList.Infrastructure.Repositories.Base;
using System.Linq;

namespace DigichList.Infrastructure.Repositories
{
    public class AssignedDefectRepository : Repository<AssignedDefect, int>, IAssignedDefectRepository
    {
        public AssignedDefectRepository(DigichListContext context) : base(context) { }
        public IQueryable<AssignedDefect> GetAssignedDefectsByUserId(int telegramId)
        {
            return _context.AssignedDefects.GetAssignedDefectsByUserId(telegramId); 
        }
    }
}
