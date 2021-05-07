using DigichList.Core.Entities;
using System.Threading.Tasks;

namespace DigichList.Core.Repositories
{
    public interface IUserRepository
    {
        public Task<User> GetUserByTelegramId(int telegramId);
    }
}
