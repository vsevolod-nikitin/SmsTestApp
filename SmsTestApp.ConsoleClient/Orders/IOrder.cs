using SmsTestApp.Contracts.Order;

namespace SmsTestApp.ConsoleClient.Orders
{
    /// <summary>
    /// Структура заказа.
    /// </summary>
    internal interface IOrder
    {
        /// <summary>
        /// Идентификатор заказа.
        /// </summary>
        Guid OrderId { get; }

        /// <summary>
        /// Элементы заказа.
        /// </summary>
        IEnumerable<OrderItemDto> Items { get; }
    }
}
