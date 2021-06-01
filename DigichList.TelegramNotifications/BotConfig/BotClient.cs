using Telegram.Bot;

namespace DigichList.TelegramNotifications.BotConfig
{
    internal static class BotClient
    {
        internal static readonly TelegramBotClient Bot = new TelegramBotClient(BotConfiguration.BotToken);
    }
}
