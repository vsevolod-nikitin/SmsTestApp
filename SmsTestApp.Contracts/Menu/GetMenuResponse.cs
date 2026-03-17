namespace SmsTestApp.Contracts.Menu
{
    /// <summary>
    /// Информация о меню.
    /// </summary>
    public sealed record GetMenuResponse : ResponseBase<GetMenuResponse.MenuStructure>
    {
        /// <inheritdoc/>
        public override string Command => "GetMenu";

        /// <summary>
        /// Структура ответа на запрос меню.
        /// </summary>
        public sealed record MenuStructure
        {
            /// <summary>
            /// Элементы меню.
            /// </summary>
            public List<MenuItem> MenuItems { get; } = [];
        }
    }
}
