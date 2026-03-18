namespace SmsTestApp.ConsoleClient.Implementation
{
    /// <summary>
    /// Реализация функционала взаимодействия с пользователем.
    /// </summary>
    internal sealed class UserInteractor : IUserInteractor
    {
        /// <inheritdoc/>
        public void LogInfo(string message)
        {
            Console.WriteLine(message);
        }

        /// <inheritdoc/>
        public string GetUserInput(string requestText)
        {
            Console.WriteLine(requestText);
            return Console.ReadLine() ?? string.Empty;
        }
    }
}
