namespace SmsTestApp.ConsoleClient
{
    /// <summary>
    /// Менеджер взаимодействия с продуктами.
    /// </summary>
    internal interface IProductsManager
    {
        /// <summary>
        /// Запустить функционал.
        /// </summary>
        /// <returns>Задача на ожидание.</returns>
        /// <exception cref="InvalidOperationException">Ошибка в рамках работы функционала.</exception>
        Task RunAsync();
    }
}