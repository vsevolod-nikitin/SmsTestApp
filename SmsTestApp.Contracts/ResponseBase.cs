namespace SmsTestApp.Contracts
{
    /// <summary>
    /// Базовая реализация ответа.
    /// </summary>
    /// <typeparam name="T">Тип возвращаемых данных.</typeparam>
    public abstract record ResponseBase<T>
        where T : new()
    {
        /// <summary>
        /// Тип выполненной команды.
        /// </summary>
        public abstract string Command { get; }
        
        /// <summary>
        /// Признак успешного выполнения.
        /// </summary>
        public required bool Success { get; init; }

        /// <summary>
        /// Текст ошибки при неуспешном выполнении команды.
        /// </summary>
        public string ErrorMessage { get; init; } = string.Empty;

        /// <summary>
        /// Результат.
        /// </summary>
        public T Data { get; } = new();
    }
}
