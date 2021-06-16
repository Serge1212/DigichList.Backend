using DigichList.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public static async Task<User> GetUserByIdWithRole(this DbSet<User> users, int id)
        {
            return await users
                .Include(r => r.Role)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public static IQueryable<User> GetTechnicians(this DbSet<User> users)
        {
            return users
                .Include(r => r.Role)
                .Where(x => x.Role.Name == "Technician");
        }

        public static IEnumerable<User> GetUsersWithRoles(this DbSet<User> users)
        {
            return users
                .Include(r => r.Role);
        }

        public static IEnumerable<User> GetUsersWithRolesAndAssignedDefects(this DbSet<User> users)
        {
            return users
                .Include(r => r.Role)
                .Include(a => a.AssignedDefects);
        }

        public static async Task<User> GetUserWithRolesAndAssignedDefectsByIdAsync(this DbSet<User> users, int id)
        {
            return await users
                .Include(r => r.Role)
                .Include(a => a.AssignedDefects)
                .FirstOrDefaultAsync(x => x.Id == id);
        }


    }
}
