using System.Text.Json.Serialization;

namespace SmsTestApp.Contracts.Menu
{
    /// <summary>
    /// Информация о меню.
    /// </summary>
    public sealed record GetMenuResponse
    {
        /// <summary>
        /// Элементы меню.
        /// </summary>
        [JsonPropertyName("MenuItems")]
        public List<MenuItemDto> Items { get; init; } = [];
    }
}
