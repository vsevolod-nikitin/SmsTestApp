using System.Text.Json.Serialization;

namespace SmsTestApp.Contracts.Order
{
    /// <summary>
    /// Команда на отправку заказа.
    /// </summary>
    public sealed record SendOrderCommand
    {
        /// <summary>
        /// Номер заказа.
        /// </summary>
        [JsonPropertyName("OrderId")]
        public required Guid OrderId { get; init; }

        /// <summary>
        /// Элементы заказа.
        /// </summary>
        [JsonPropertyName("MenuItems")]
        public List<OrderItem> Items { get; init; } = [];
    }
}
