using SmsTestApp.ConsoleClient.Repository;

namespace SmsTestApp.ConsoleClient
{
    /// <summary>
    /// Менеджер для взаимодействия с продуктами.
    /// </summary>
    /// <param name="productsApp">Приложение для работы с продуктами.</param>
    /// <param name="userInteractor">Функционал взаимодействия с пользователем.</param>
    /// <param name="menuStorage">Функционал хранения информации о блюдах.</param>
    internal sealed class ProductsManager(
        IProductsApp productsApp,
        IUserInteractor userInteractor,
        IMenuStorage menuStorage)
    {
        /// <summary>
        /// Запустить приложение.
        /// </summary>
        /// <returns>Задача на ожидание.</returns>
        public async Task RunAsync()
        {
            var menuItems = await productsApp.GetMenuAsync(true);
            await menuStorage.UpdateProductsAsync(menuItems);

            foreach (var item in menuItems)
            {
                userInteractor.LogInfo($"{item.Name} - {item.Article} - {item.Price}");
            }
        }
    }
}
