using DigichList.Core.Entities;
using DigichList.Core.Repositories.Base;
using System.Threading.Tasks;

namespace DigichList.Core.Repositories
{
    public interface IRoleRepository : IRepository<Role, int>
    {
        public Task<Role> GetRoleByNameAsync(string roleName);
        public Task<dynamic> ReturnRoleByIdRequest(int id);
    }
}
