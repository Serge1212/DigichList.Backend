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
       
        public async Task<User> GetUserByTelegramId(int telegramId)
        {
            return await _context.Users.GetUserByTelegramId(telegramId);
        }
    }
}
