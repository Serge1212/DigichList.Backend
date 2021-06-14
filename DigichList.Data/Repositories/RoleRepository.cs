using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using DigichList.Infrastructure.Data;
using DigichList.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace DigichList.Infrastructure.Repositories
{
    public class RoleRepository : Repository<Role, int>, IRoleRepository
    {
        public RoleRepository(DigichListContext context) : base(context) {}


        public async Task<Role> GetRoleByNameAsync(string roleName)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        }
        public async Task<bool> AssignRole(User user, int roleId)
        {
            if(user == null)
                return false;


            var role = await GetByIdAsync(roleId);

            if(role == null)
                return false;
            
            if(user.Role.Name == "Technician")
            {
                _context
                    .AssignedDefects
                    .RemoveRange(_context.AssignedDefects.Where(x => x.AssignedWorker.Id == user.Id));
            }

            user.Role = role;
            _context.Users.Update(user);
            await SaveChangesAsync();
            return true;
        }
        public bool RemoveRoleFromUser(User user)
        {
            if (user == null)
                return false;

            RemoveRoleAndAssignedDefects(user);
            return true;
        }

        private void RemoveRoleAndAssignedDefects(User user)
        {
            _context.AssignedDefects.RemoveRange(user.AssignedDefects.Where(x => x.ClosedAt == null));
            user.AssignedDefects = null;
            user.Role = null;
            user.IsRegistered = false;
            _context.Users.Update(user);
            _context.SaveChanges();
        }

    }
}
