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

        /// <summary>
        /// Получить признак валидности артикула.
        /// </summary>
        /// <param name="article">Артикул.</param>
        /// <returns>Признак валидного артикула.</returns>
        Task<bool> IsArticleValidAsync(string article);
    }
}
