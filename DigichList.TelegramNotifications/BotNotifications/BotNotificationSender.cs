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
            var message = GetMessageForSpecifiedRole(roleName, UserGotRole);
            await SendMessageAsync(telegramId, message);
        }

        private string GetMessageForSpecifiedRole(string roleName, string message)
        {
            if (roleName == "Maid")
            {
                return string.Concat(message, MaidRoleDescription);
            }
            else if(roleName == "Technician")
            {
                return string.Concat(message, TechnicianRoleDescription);
            }
            return string.Empty;
        }

        public async Task NotifyUserLostRole(int telegramId, string roleName)
        {
            var message = GetMessageForSpecifiedRole(roleName, "На жаль, ви втратили наступні повноваження:\n");
            await SendMessageAsync(telegramId, message);
        }
    }
}
