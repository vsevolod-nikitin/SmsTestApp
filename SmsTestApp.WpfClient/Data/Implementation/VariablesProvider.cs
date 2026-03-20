using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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
        /// <param name="logger">Функционал логирования.</param>
        public VariablesProvider(IConfiguration configuration, ILogger<VariableItem> logger)
        {
            // Инициализация переменных среды на основе конфигурации
            foreach (var section in configuration.GetSection("EnvironmentVariables").GetChildren())
            {
                var name = section.Key;
                var description = section.Value;

                var variable = new VariableItem(name, description, logger);
                _variables.Add(variable);
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IVariableItem> GetVariables() => _variables;
    }
}
