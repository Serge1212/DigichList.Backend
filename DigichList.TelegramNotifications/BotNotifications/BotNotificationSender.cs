using static DigichList.TelegramNotifications.Helpers.TelegramBotTextMessages;
using static DigichList.TelegramNotifications.Helpers.TelegramBotMessageSender;
using DigichList.Core.Entities;
using System.Threading.Tasks;

namespace DigichList.TelegramNotifications.BotNotifications
{
    public class BotNotificationSender : IBotNotificationSender
    {
        public async Task NotifyUserIsOrIsNotRegistered(int telegramId, bool registrationStatus)
        {
            await (registrationStatus ?
                SendMessageAsync(telegramId, UserGotRegistered) : 
                SendMessageAsync(telegramId, UserWasNotRegistered));
        }
        public async Task NotifyUserWasGivenWithDefect(int telegramId, Defect defect)
        {
            var message = string.Format(UserGotDefect, defect.ToString());
            await SendMessageAsync(telegramId, message);
        }

        public async Task NotifyUserHisDefectGotApproved(int telegramId, string defectDescription)
        {
            var message = string.Format(UsersDefectGotApproved, defectDescription);
            await SendMessageAsync(telegramId, message);
        }

        public async Task NotifyUserGotRole(int telegramId, string roleName)
        {
            var message = GetRoleInfo(roleName);
            await SendMessageAsync(telegramId, message);
        }

        private string GetRoleInfo(string roleName)
        {
            if (roleName == "Maid")
            {
                return string.Format(UserGotRole, roleName, UserGotMaidRole);
            }
            else if(roleName == "Technician")
            {
                return string.Format(UserGotRole, roleName, UserGotTechnicianRole);
            }
            return string.Empty;
        }
    }
}
