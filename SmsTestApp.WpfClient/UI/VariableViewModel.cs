using Caliburn.Micro;
using SmsTestApp.WpfClient.Data;

namespace SmsTestApp.WpfClient.UI
{
    /// <summary>
    /// Модель представления переменной среды.
    /// </summary>
    /// <param name="variable">Переменная среды.</param>
    internal sealed class VariableViewModel(IVariableItem variable) : PropertyChangedBase
    {
        private string? _value = variable.Value;

        /// <summary>
        /// Наименование.
        /// </summary>
        public string? Name => variable.Name;

        /// <summary>
        /// Значение.
        /// </summary>
        public string? Value
        {
            get => _value;
            set
            {
                if (Set(ref _value, value))
                {
                    variable.Value = value;
                }
            }
        }

        /// <summary>
        /// Описание.
        /// </summary>
        public string? Description => variable.Description;
    }
}
