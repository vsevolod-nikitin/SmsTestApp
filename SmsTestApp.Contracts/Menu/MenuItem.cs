namespace SmsTestApp.Contracts.Menu
{
    /// <summary>
    /// Отдельное блюдо.
    /// </summary>
    public sealed record MenuItem
    {
        /// <summary>
        /// Уникальный идентификатор блюда.
        /// </summary>
        public required string Id { get; init; }

        /// <summary>
        /// Артикул блюда.
        /// </summary>
        public required string Article { get; init; }

        /// <summary>
        /// Наименование блюда.
        /// </summary>
        public required string Name { get; init; }

        /// <summary>
        /// Стоимость блюда.
        /// </summary>
        public decimal? Price { get; init; }

        /// <summary>
        /// Признак того, что блюдо является взвешиваемым.
        /// </summary>
        public bool IsWeighted { get; init; }

        /// <summary>
        /// Полный путь к блюду в структуре меню.
        /// </summary>
        public required string FullPath { get; init; }

        /// <summary>
        /// Штрихкоды, по которым можно идентифицировать блюдо.
        /// </summary>
        public List<string> Barcodes { get; } = [];
    }
}
