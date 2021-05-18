using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using DigichList.Infrastructure.Data;
using DigichList.Infrastructure.Extensions;
using DigichList.Infrastructure.Repositories.Base;
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
    }
}
