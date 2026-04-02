namespace SmsTestApp.WpfClient.Data
{
    /// <summary>
    /// Переменная среды.
    /// </summary>
    internal interface IVariableItem
    {
        /// <summary>
        /// Наименование.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Значение.
        /// </summary>
        /// <exception cref="InvalidOperationException">Ошибка при установке значения.</exception>
        string? Value { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        string? Description { get; }
    }
}
