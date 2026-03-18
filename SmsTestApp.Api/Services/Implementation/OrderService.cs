using SmsTestApp.Contracts.Order;

namespace SmsTestApp.Api.Services.Implementation
{
    /// <summary>
    /// Реализация сервиса приема заказов.
    /// </summary>
    public sealed class OrderService : IOrderService
    {
        /// <inheritdoc/>
        public Task SendOrderAsync(Guid orderId, IEnumerable<OrderItem> items)
        {
            // В тестовом варианте ничего не делаем.
            return Task.CompletedTask;
        }
    }
}
