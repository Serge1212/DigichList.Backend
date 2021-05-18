using DigichList.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DigichList.Infrastructure.Extensions
{
    public static class UserDbContextExtensions
    {
        public static async Task<User> GetUserByTelegramId(this DbSet<User> users, int telegramId)
        {
            return await users.FirstOrDefaultAsync(x => x.TelegramId == telegramId);
        }
        public static async Task<User> GetUserByTelegramIdWithRole(this DbSet<User> users, int telegramId)
        {
            return await users
                .Include(r => r.Role)
                .FirstOrDefaultAsync(x => x.TelegramId == telegramId);
        }

        public static IQueryable<User> GetTechnicians(this DbSet<User> users)
        {
            return users
                .Include(r => r.Role)
                .Where(x => x.Role.Name == "Technician");
        }
    }
}
