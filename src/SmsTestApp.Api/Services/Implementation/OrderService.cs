using SmsTestApp.Contracts.Order;

namespace SmsTestApp.Api.Services.Implementation
{
    /// <summary>
    /// Реализация сервиса приема заказов.
    /// </summary>
    public sealed class OrderService : IOrderService
    {
        /// <inheritdoc/>
        public Task SendOrderAsync(string orderId, IEnumerable<OrderItemDto> items)
        {
            // В тестовом варианте ничего не делаем.
            return Task.CompletedTask;
        }
    }
}
