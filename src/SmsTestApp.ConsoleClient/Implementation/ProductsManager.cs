using Microsoft.Extensions.Logging;
using SmsTestApp.ConsoleClient.Orders;
using SmsTestApp.ConsoleClient.Repository;

namespace SmsTestApp.ConsoleClient.Implementation
{
    /// <summary>
    /// Реализация менеджера для взаимодействия с продуктами.
    /// </summary>
    /// <param name="productsApp">Приложение для работы с продуктами.</param>
    /// <param name="userInteractor">Функционал взаимодействия с пользователем.</param>
    /// <param name="menuStorage">Функционал хранения информации о блюдах.</param>
    /// <param name="orderFactory">Фабрика новых заказов.</param>
    /// <param name="logger">Функционал логирования.</param>
    internal sealed class ProductsManager(
        IProductsApp productsApp,
        IUserInteractor userInteractor,
        IMenuStorage menuStorage,
        IOrderFactory orderFactory,
        ILogger<ProductsManager> logger) : IProductsManager
    {
        /// <inheritdoc/>
        public async Task RunAsync()
        {
            try
            {
                // Получаем меню и сохраняем его в хранилище.
                var menuItems = await productsApp.GetMenuAsync(true);
                await menuStorage.UpdateProductsAsync(menuItems);

                foreach (var item in menuItems)
                {
                    logger.LogInformation("{Name} - {Article} - {Price}", item.Name, item.Article, item.Price);
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
                        logger.LogError(ex, ex.Message);
                        continue;
                    }
                }

                // Отправляем заказ и обрабатываем результат.
                await productsApp.SendOrderAsync(order.OrderId.ToString(), order.Items);
                logger.LogInformation("УСПЕХ");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при работе с продуктами: {Message}", ex.Message);
                throw new InvalidOperationException("Произошла ошибка при работе с продуктами. Подробности в логах.", ex);
            }
        }
    }
}
