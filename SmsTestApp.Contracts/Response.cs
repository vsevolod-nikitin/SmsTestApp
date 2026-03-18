using System.Text.Json.Serialization;

namespace SmsTestApp.Contracts
{
    /// <summary>
    /// Ответ от сервиса.
    /// </summary>
    public record Response
    {
        /// <summary>
        /// Тип выполненной команды.
        /// </summary>
        [JsonPropertyName("Command")]
        public required string Command { get; init; }

        /// <summary>
        /// Признак успешного выполнения.
        /// </summary>
        [JsonPropertyName("Success")]
        public required bool Success { get; init; }

        /// <summary>
        /// Текст ошибки при неуспешном выполнении команды.
        /// </summary>
        [JsonPropertyName("ErrorMessage")]
        public string ErrorMessage { get; init; } = string.Empty;

        /// <summary>
        /// Результат.
        /// </summary>
        [JsonPropertyName("Data")]
        public object? Data { get; init; }
    }
}
