using SmsTestApp.Contracts.Menu;

namespace SmsTestApp.Api.Services.Implementation
{
    /// <summary>
    /// Реализация сервиса для работы с меню.
    /// </summary>
    internal sealed class MenuService : IMenuService
    {
        /// <inheritdoc/>
        public Task<IEnumerable<MenuItem>> GetMenuAsync(bool withPrice)
        {
            // Для тестового варианта возвращаем статический список меню. В реальной реализации здесь будет логика получения данных из базы данных или другого источника.
            var menuItems = GetTestMenuItems(withPrice);
            return Task.FromResult(menuItems);
        }

        private static IEnumerable<MenuItem> GetTestMenuItems(bool withPrice)
        {
            yield return new MenuItem
            {
                Id = "5979224",
                Article = "A1004292",
                Name = "Каша гречневая",
                Price = withPrice ? 50m : null,
                IsWeighted = false,
                FullPath = @"ПРОИЗВОДСТВО\Гарниры",
                Barcodes = { "57890975627974236429" }
            };

            yield return new MenuItem
            {
                Id = "9084246",
                Article = "A1004293",
                Name = "Конфеты Коровка",
                Price = withPrice ? 300m : null,
                IsWeighted = true,
                FullPath = @"ДЕСЕРТЫ\Развес"
            };
        }
    }
}
