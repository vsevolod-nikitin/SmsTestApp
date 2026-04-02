using System.Text.Json.Serialization;

namespace SmsTestApp.Contracts.Order
{
    /// <summary>
    /// Элемент заказа.
    /// </summary>
    public sealed record OrderItemDto
    {
        /// <summary>
        /// Идентификатор блюда из меню.
        /// </summary>
        [JsonPropertyName("Id")]
        public required string MenuItemId { get; init; }

        /// <summary>
        /// Количество блюда. Для взвешиваемых блюд - вес в килограммах, для невзвешиваемых - количество в штуках.
        /// </summary>
        [JsonPropertyName("Quantity")]
        public required string Quantity { get; init; }
    }
}
