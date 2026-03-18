using Microsoft.Extensions.Logging;

namespace SmsTestApp.ConsoleClient.Implementation
{
    /// <summary>
    /// Реализация функционала взаимодействия с пользователем.
    /// </summary>
    /// <param name="logger">Логгер для записи информации.</param>
    internal sealed class UserInteractor(ILogger<UserInteractor> logger) : IUserInteractor
    {
        /// <inheritdoc/>
        public string GetUserInput(string requestText)
        {
            logger.LogInformation(requestText);
            return Console.ReadLine() ?? string.Empty;
        }
    }
}
