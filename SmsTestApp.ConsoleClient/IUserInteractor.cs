namespace SmsTestApp.ConsoleClient
{
    /// <summary>
    /// Функционал взаимодействия с пользователем.
    /// </summary>
    internal interface IUserInteractor
    {
        /// <summary>
        /// Получить строку ввода от пользователя.
        /// </summary>
        /// <param name="requestText">Текст запроса для пользователя.</param>
        /// <returns>Строка ввода.</returns>
        string GetUserInput(string requestText);
    }
}
