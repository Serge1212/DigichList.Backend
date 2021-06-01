using DigichList.TelegramNotifications.BotConfig;
using System.Threading.Tasks;

namespace DigichList.TelegramNotifications.Helpers
{
    internal static class TelegramBotMessageSender
    {
        public static async Task SendMessageAsync(int telegramId, string text)
        {
            await BotClient.Bot.SendTextMessageAsync(telegramId, text);
        }
    }
}
