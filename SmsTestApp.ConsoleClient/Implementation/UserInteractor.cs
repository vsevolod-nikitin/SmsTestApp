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
    }
}
