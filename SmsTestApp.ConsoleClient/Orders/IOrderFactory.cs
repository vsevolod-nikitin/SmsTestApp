namespace SmsTestApp.ConsoleClient.Orders
{
    /// <summary>
    /// Фабрика для создания новых заказов.
    /// </summary>
    internal interface IOrderFactory
    {
        /// <summary>
        /// Создать новый заказ.
        /// </summary>
        /// <param name="input">Строка ввода.</param>
        /// <returns>Экземпляр заказа.</returns>
        /// <exception cref="ArgumentException">Входные данные недопустимы.</exception>"
        Task<IOrder> CreateOrderAsync(string input);
    }
}
