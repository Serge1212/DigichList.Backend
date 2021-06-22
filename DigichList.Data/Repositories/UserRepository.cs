using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using DigichList.Infrastructure.Data;
using DigichList.Infrastructure.Extensions;
using DigichList.Infrastructure.Repositories.Base;
using DigichList.TelegramNotifications.BotNotifications;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigichList.Infrastructure.Repositories
{
    public class UserRepository : Repository<User, int>, IUserRepository
    {
        private readonly IBotNotificationSender _botNotificationSender;
        public UserRepository(DigichListContext context, IBotNotificationSender botNotificationSender = null) : base(context) 
        {
            _botNotificationSender = botNotificationSender;
        }


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

        public async Task DeleteRangeAsync(int[] idArr)
        {
            var usersToDelete = GetRangeByIds(idArr);
            _context.RemoveRange(usersToDelete);
            await NotifyUsersTheyWereRemovedFromDatabase(usersToDelete);
            await SaveChangesAsync();
        }
        public IEnumerable<User> GetRangeByIds(int[] idArr)
        {
            return _context.Users.Where(d => idArr.Contains(d.Id));
        }

        private async Task NotifyUsersTheyWereRemovedFromDatabase(IEnumerable<User> usersToDelete)
        {
            foreach(var u in usersToDelete)
            {
                await _botNotificationSender.NotifyUserIsOrIsNotRegistered(u.TelegramId, false);
            }
        }
    }
}
