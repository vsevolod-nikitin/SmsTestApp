namespace SmsTestApp.WpfClient.Data.Implementation
{
    /// <summary>
    /// Реализация переменной среды.
    /// </summary>
    internal sealed class VariableItem(string name, string? description) : IVariableItem
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
