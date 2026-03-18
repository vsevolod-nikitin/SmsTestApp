using SmsTestApp.Contracts.Menu;
using SmsTestApp.Contracts.Order;

namespace SmsTestApp
{
    /// <summary>
    /// Приложение для работы с продуктами.
    /// </summary>
    public interface IProductsApp
    {
        /// <summary>
        /// Получить меню.
        /// </summary>
        /// <param name="withPrice">Признак получения цен.</param>
        /// <returns>Набор элементов.</returns>
        /// <exception cref="InvalidOperationException">Ошибка при получении меню.</exception>"
        Task<IEnumerable<MenuItemDto>> GetMenuAsync(bool withPrice);

        /// <summary>
        /// Отправить заказ на выполнение.
        /// </summary>
        /// <param name="orderId">Идентификатор заказа.</param>
        /// <param name="items">Набор элементов.</param>
        /// <returns>Задача на ожидание.</returns>
        /// <exception cref="InvalidOperationException">Ошибка при отправке заказа.</exception>"
        Task SendOrderAsync(string orderId, IEnumerable<OrderItemDto> items);
    }
}
