using SmsTestApp.ConsoleClient.Orders;
using SmsTestApp.ConsoleClient.Repository;

namespace SmsTestApp.ConsoleClient
{
    /// <summary>
    /// Менеджер для взаимодействия с продуктами.
    /// </summary>
    /// <param name="productsApp">Приложение для работы с продуктами.</param>
    /// <param name="userInteractor">Функционал взаимодействия с пользователем.</param>
    /// <param name="menuStorage">Функционал хранения информации о блюдах.</param>
    /// <param name="orderFactory">Фабрика новых заказов.</param>
    internal sealed class ProductsManager(
        IProductsApp productsApp,
        IUserInteractor userInteractor,
        IMenuStorage menuStorage,
        IOrderFactory orderFactory)
    {
        /// <summary>
        /// Запустить приложение.
        /// </summary>
        /// <returns>Задача на ожидание.</returns>
        public async Task RunAsync()
        {
            // Получаем меню и сохраняем его в хранилище.
            var menuItems = await productsApp.GetMenuAsync(true);
            await menuStorage.UpdateProductsAsync(menuItems);

            foreach (var item in menuItems)
            {
                userInteractor.LogInfo($"{item.Name} - {item.Article} - {item.Price}");
            }

            // Формируем заказ на основе пользовательского ввода.
            IOrder order;
            while (true)
            {
                var input = userInteractor.GetUserInput("Введите блюда в формате Код1:Количество1;Код2:Количество2;Код3:Количество3;...");
                try
                {
                    order = await orderFactory.CreateOrderAsync(input);
                    break;
                }
                catch (Exception ex)
                {
                    userInteractor.LogInfo(ex.Message);
                    continue;
                }
            }

            // Отправляем заказ и обрабатываем результат.
            try
            {
                await productsApp.SendOrderAsync(order.OrderId.ToString(), order.Items);
                userInteractor.LogInfo("УСПЕХ");
            }
            catch (Exception ex)
            {
                userInteractor.LogInfo(ex.Message);
            }
        }
    }
}
