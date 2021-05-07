using DigichList.Core.Entities;
using System.Threading.Tasks;

namespace DigichList.Core.Repositories
{
    public interface IRoleRepository
    {
        public Task<Role> GetRoleByNameAsync(string roleName);
    }
}
