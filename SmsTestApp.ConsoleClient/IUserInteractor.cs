namespace SmsTestApp.ConsoleClient
{
    /// <summary>
    /// Функционал взаимодействия с пользователем.
    /// </summary>
    internal interface IUserInteractor
    {
        /// <summary>
        /// Вывести информационное сообщение.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        void LogInfo(string message);
    }
}
