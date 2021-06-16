 namespace DigichList.TelegramNotifications.Helpers
{
    internal static class TelegramBotTextMessages
    {
        internal const string UserGotRegistered = "Вітаємо! Вашу заявку на реєстрацію було схвалено";
        internal const string UserWasNotRegistered = "На жаль, Вашу заявку на реєстрацію було відхилено";
        internal const string UserGotDefect = "Вам призначено дефект:{0}";
        internal const string UsersDefectGotApproved = "Ваш дефект з описом \"{0}\" підтвердили!";
        internal const string UserGotRole = "Вітаємо! Вам була призначена роль \"{0}\". Тепер ви зможете:\n{1}";
        internal const string MaidRoleDescription = "Публікувати дефекти";
        internal const string TechnicianRoleDescription = "Публікувати дефекти\nВиправляти дефекти";
    }
}
