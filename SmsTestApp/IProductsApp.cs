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
        Task<IEnumerable<MenuItem>> GetMenuAsync(bool withPrice);

        /// <summary>
        /// Отправить заказ на выполнение.
        /// </summary>
        /// <param name="orderId">Идентификатор заказа.</param>
        /// <param name="items">Набор элементов.</param>
        /// <returns>Признак успешной отправки.</returns>
        Task<bool> SendOrderAsync(Guid orderId, IEnumerable<OrderItem> items);
    }
}
