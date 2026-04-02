using System.Text.Json.Serialization;

namespace SmsTestApp.Contracts.Menu
{
    /// <summary>
    /// Запрос на получение меню.
    /// </summary>
    public sealed record GetMenuRequest
    {
        /// <summary>
        /// Признак получения цен на блюда.
        /// </summary>
        [JsonPropertyName("WithPrice")]
        public bool WithPrice { get; init; }
    }
}
