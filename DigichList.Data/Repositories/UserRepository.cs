using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using DigichList.Infrastructure.Data;
using DigichList.Infrastructure.Extensions;
using DigichList.Infrastructure.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigichList.Infrastructure.Repositories
{
    public class UserRepository : Repository<User, int>, IUserRepository
    {
        public UserRepository(DigichListContext context) : base(context) { }


        public async Task<User> GetUserByTelegramIdAsync(int telegramId)
        {
            return await _context.Users.GetUserByTelegramId(telegramId);
        }

        public async Task<User> GetUserByTelegramIdWithRoleAsync(int telegramId)
        {
            return await _context.Users.GetUserByTelegramIdWithRole(telegramId);
        }

        public IEnumerable<User> GetUsersWithRoles()
        {
            return _context.Users.GetUsersWithRoles();
        }
        public IEnumerable<User> GetTechnicians()
        {
            return _context.Users.GetTechnicians();
        }

        public async Task<User> GetUserWithRoleAsync(int id)
        {
            return await _context.Users.GetUserByIdWithRole(id);
        }

        public IEnumerable<User> GetUsersWithRolesAndAssignedDefects()
        {
            return _context.Users.GetUsersWithRolesAndAssignedDefects();
        }

        public async Task<User> GetUserWithRolesAndAssignedDefectsByIdAsync(int id)
        {
            return await _context.Users.GetUserWithRolesAndAssignedDefectsByIdAsync(id);
        }
    }
}
