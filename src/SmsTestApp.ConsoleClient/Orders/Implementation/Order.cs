using SmsTestApp.Contracts.Order;

namespace SmsTestApp.ConsoleClient.Orders.Implementation
{
    /// <summary>
    /// Реализация структуры заказа.
    /// </summary>
    internal sealed class Order : IOrder
    {
        private readonly List<OrderItemDto> _items = [];

        /// <inheritdoc/>
        public Guid OrderId { get; } = Guid.NewGuid();

        /// <inheritdoc/>
        public IEnumerable<OrderItemDto> Items => _items;

        /// <summary>
        /// Добавить новый элемент в заказ.
        /// </summary>
        /// <param name="menuItemId">Идентификатор блюда.</param>
        /// <param name="quantity">Количество.</param>
        public void AddItem(string menuItemId, double quantity)
        {
            var newItem = new OrderItemDto
            {
                MenuItemId = menuItemId,
                Quantity = quantity.ToString(),
            };

            _items.Add(newItem);
        }
    }
}
