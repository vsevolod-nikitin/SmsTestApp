using SmsTestApp.ConsoleClient.Repository;
using System.Globalization;

namespace SmsTestApp.ConsoleClient.Orders.Implementation
{
    /// <summary>
    /// Реализация фабрики новых заказов.
    /// </summary>
    /// <param name="menuStorage">Функционал хранения информации о блюдах.</param>
    internal sealed class OrderFactory(IMenuStorage menuStorage) : IOrderFactory
    {
        /// <inheritdoc/>
        public async Task<IOrder> CreateOrderAsync(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Пустой ввод недопустим");
            }

            var order = new Order();

            var items = input.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach (var item in items)
            {
                var info = item.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (info.Length != 2)
                {
                    throw new ArgumentException($"Неверный формат позиции: {item}");
                }

                var article = info[0];
                if (!await menuStorage.IsArticleValidAsync(article))
                {
                    throw new ArgumentException($"Блюдо с артикулом {article} не найдено в меню");
                }

                if (!double.TryParse(info[1], NumberStyles.Float, CultureInfo.InvariantCulture, out var quantity) || quantity <= 0)
                {
                    throw new ArgumentException($"Неверное количество для блюда {article}: {info[1]}");
                }

                order.AddItem(article, quantity);
            }

            return order;
        }
    }
}
