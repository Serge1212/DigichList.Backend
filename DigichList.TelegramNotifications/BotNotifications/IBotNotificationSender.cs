using DigichList.Core.Entities;
using System.Threading.Tasks;

namespace DigichList.TelegramNotifications.BotNotifications
{
    public interface IBotNotificationSender
    {
        public Task NotifyUserIsOrIsNotRegistered(int telegramId, bool registrationStatus);
        public Task NotifyUserWasGivenWithDefect(int telegramId, Defect defect);
        public Task NotifyUserHisDefectGotApproved(int telegramId, string defectDescription);
        public Task NotifyUserGotRole(int telegramId, string roleName);
        public Task NotifyUserLostRole(int telegramId, string roleName);

    }
}
