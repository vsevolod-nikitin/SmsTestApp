using Microsoft.Extensions.Configuration;

namespace SmsTestApp.WpfClient.Data.Implementation
{
    /// <summary>
    /// Реализация провайдера переменных среды.
    /// </summary>
    internal sealed class VariablesProvider : IVariablesProvider
    {
        private readonly List<VariableItem> _variables = [];

        /// <summary>
        /// Создать новый экземпляр <see cref="VariablesProvider"/>.
        /// </summary>
        /// <param name="configuration">Конфигурация функционала.</param>
        public VariablesProvider(IConfiguration configuration)
        {
            // Инициализация переменных среды на основе конфигурации
            foreach (var section in configuration.GetSection("EnvironmentVariables").GetChildren())
            {
                var name = section.Key;
                var description = section.Value;

                var variable = new VariableItem(name, description);
                _variables.Add(variable);
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IVariableItem> GetVariables() => _variables;
    }
}
