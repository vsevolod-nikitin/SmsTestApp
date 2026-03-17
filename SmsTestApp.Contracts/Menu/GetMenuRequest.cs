namespace SmsTestApp.Contracts.Menu
{
    /// <summary>
    /// Запрос на получение меню.
    /// </summary>
    public sealed record GetMenuRequest : RequestBase<GetMenuRequest.MenuParameters>
    {
        /// <inheritdoc/>
        public override string Command => "GetMenu";

        /// <summary>
        /// Параметры получения меню.
        /// </summary>
        public sealed record MenuParameters
        {
            /// <summary>
            /// Признак получения цен на блюда.
            /// </summary>
            public bool WithPrice { get; init; }
        }
    }
}
