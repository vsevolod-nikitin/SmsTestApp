using SmsTestApp.Contracts.Menu;

namespace SmsTestApp.Api.Services
{
    /// <summary>
    /// Сервис для работы с меню.
    /// </summary>
    public interface IMenuService
    {
        /// <summary>
        /// Получить меню.
        /// </summary>
        /// <param name="withPrice">Признак получения цен на блюда.</param>
        /// <returns>Набор блюд.</returns>
        Task<IEnumerable<MenuItemDto>> GetMenuAsync(bool withPrice);
    }
}
