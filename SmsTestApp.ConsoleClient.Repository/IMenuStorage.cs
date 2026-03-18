using SmsTestApp.Contracts.Menu;

namespace SmsTestApp.ConsoleClient.Repository
{
    /// <summary>
    /// Функционал хранения информации о блюдах.
    /// </summary>
    public interface IMenuStorage
    {
        /// <summary>
        /// Обновить информацию о блюдах.
        /// </summary>
        /// <param name="products">Набор продуктов.</param>
        /// <returns>Задача на ожидание.</returns>
        Task UpdateProductsAsync(IEnumerable<MenuItemDto> products);
    }
}
