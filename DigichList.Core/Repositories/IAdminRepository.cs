using DigichList.Core.Entities;
using DigichList.Core.Repositories.Base;
using System.Threading.Tasks;

namespace DigichList.Core.Repositories
{
    public interface IAdminRepository: IRepository<Admin, int>
    {
        public Task<Admin> GetAdminByEmail(string email);
        public Task DeleteRangeAsync(int[] idArr);
    }
}
