using Caliburn.Micro;
using SmsTestApp.WpfClient.Data;

namespace SmsTestApp.WpfClient.UI
{
    /// <summary>
    /// Корневая модель представления.
    /// </summary>
    /// <param name="variablesProvider">Провайдер переменных среды.</param>
    internal sealed class ShellViewModel(IVariablesProvider variablesProvider) : Screen
    {
        /// <summary>
        /// Набор переменных среды.
        /// </summary>
        public BindableCollection<VariableViewModel> Variables { get; } = [];

        /// <inheritdoc/>
        protected override void OnViewLoaded(object view)
        {
            foreach (var variable in variablesProvider.GetVariables())
            {
                Variables.Add(new VariableViewModel(variable));
            }
        }
    }
}
