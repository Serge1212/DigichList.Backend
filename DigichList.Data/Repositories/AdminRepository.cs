using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using DigichList.Infrastructure.Data;
using DigichList.Infrastructure.Extensions;
using DigichList.Infrastructure.Repositories.Base;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigichList.Infrastructure.Repositories
{
    public class AdminRepository: Repository<Admin, int>, IAdminRepository
    {
        public AdminRepository(DigichListContext context) : base(context) { }

        public async Task DeleteRangeAsync(int[] idArr)
        {
            var defectsToDelete = GetRangeByIds(idArr);
            _context.RemoveRange(defectsToDelete);
            await SaveChangesAsync();
        }
        public IEnumerable<Admin> GetRangeByIds(int[] idArr)
        {
            return _context.Admins.Where(d => idArr.Contains(d.Id));
        }

        public async Task<Admin> GetAdminByEmail(string email)
        {
            return await _context.Admins.GetAdminByEmail(email);
        }
    }
}
