using DigichList.Core.Entities;
using DigichList.Core.Repositories.Base;
using System.Threading.Tasks;

namespace DigichList.Core.Repositories
{
    public interface IUserRepository : IRepository<User, int>
    {
        public Task<User> GetUserByTelegramIdAsync(int telegramId);

        public Task<User> GetUserByTelegramIdWithRoleAsync(int telegramId);
    }
}
