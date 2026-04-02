using System.Text.Json.Serialization;

namespace SmsTestApp.Contracts
{
    /// <summary>
    /// Запрос на получение информации.
    /// </summary>
    public sealed record Request
    {
        /// <summary>
        /// Тип команды.
        /// </summary>
        [JsonPropertyName("Command")]
        public required string Command { get; init; }

        /// <summary>
        /// Параметры.
        /// </summary>
        [JsonPropertyName("CommandParameters")]
        public object CommandParameters { get; init; } = new();
    }
}
