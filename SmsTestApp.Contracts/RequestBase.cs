namespace SmsTestApp.Contracts
{
    /// <summary>
    /// Базовая реализация запроса.
    /// </summary>
    /// <typeparam name="T">Тип дополнительных параметров.</typeparam>
    public abstract record RequestBase<T>
        where T : new()
    {
        /// <summary>
        /// Тип команды.
        /// </summary>
        public abstract string Command { get; }

        /// <summary>
        /// Дополнительные параметры.
        /// </summary>
        public T CommandParameters { get; } = new();
    }
}
