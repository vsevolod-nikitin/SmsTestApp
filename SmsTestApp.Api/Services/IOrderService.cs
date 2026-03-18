using SmsTestApp.Contracts.Order;

namespace SmsTestApp.Api.Services
{
    /// <summary>
    /// Сервис приема заказов.
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Отправить заказ на выполнение.
        /// </summary>
        /// <param name="orderId">Идентификатор заказа.</param>
        /// <param name="items">Элементы заказа.</param>
        /// <returns>Задача на ожидание.</returns>
        Task SendOrderAsync(Guid orderId, IEnumerable<OrderItemDto> items);
    }
}
