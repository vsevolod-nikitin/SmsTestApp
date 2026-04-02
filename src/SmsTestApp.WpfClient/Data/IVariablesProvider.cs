namespace SmsTestApp.WpfClient.Data
{
    /// <summary>
    /// Провайдер переменных среды.
    /// </summary>
    internal interface IVariablesProvider
    {
        /// <summary>
        /// Получить набор переменных.
        /// </summary>
        /// <returns>Набор переменных.</returns>
        IEnumerable<IVariableItem> GetVariables();
    }
}
