using Microsoft.Extensions.Logging;

namespace SmsTestApp.WpfClient.Data.Implementation
{
    /// <summary>
    /// Реализация переменной среды.
    /// </summary>
    /// <param name="name">Наименование.</param>
    /// <param name="description">Описание.</param>
    /// <param name="logger">Функционал логирования.</param>
    internal sealed class VariableItem(
        string name,
        string? description,
        ILogger logger) : IVariableItem
    {
        private string? _value = Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.User);

        /// <inheritdoc/>
        public string Name { get; } = name;

        /// <inheritdoc/>
        public string? Value
        {
            get => _value;
            set
            {
                try
                {
                    Environment.SetEnvironmentVariable(Name, value, EnvironmentVariableTarget.User);
                    _value = value;

                    logger.LogInformation("Variable set: {Name} = {Value}", Name, value);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Ошибка при установке значения переменной среды '{Name}'.", ex);
                }
            }
        }

        /// <inheritdoc/>
        public string? Description { get; } = description;
    }
}
