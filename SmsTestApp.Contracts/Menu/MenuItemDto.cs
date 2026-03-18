using System.Text.Json.Serialization;

namespace SmsTestApp.Contracts.Menu
{
    /// <summary>
    /// Отдельное блюдо.
    /// </summary>
    public sealed record MenuItemDto
    {
        /// <summary>
        /// Уникальный идентификатор блюда.
        /// </summary>
        [JsonPropertyName("Id")]
        public required string Id { get; init; }

        /// <summary>
        /// Артикул блюда.
        /// </summary>
        [JsonPropertyName("Article")]
        public required string Article { get; init; }

        /// <summary>
        /// Наименование блюда.
        /// </summary>
        [JsonPropertyName("Name")]
        public required string Name { get; init; }

        /// <summary>
        /// Стоимость блюда.
        /// </summary>
        [JsonPropertyName("Price")]
        public decimal? Price { get; init; }

        /// <summary>
        /// Признак того, что блюдо является взвешиваемым.
        /// </summary>
        [JsonPropertyName("IsWeighted")]
        public bool IsWeighted { get; init; }

        /// <summary>
        /// Полный путь к блюду в структуре меню.
        /// </summary>
        [JsonPropertyName("FullPath")]
        public required string FullPath { get; init; }

        /// <summary>
        /// Штрихкоды, по которым можно идентифицировать блюдо.
        /// </summary>
        [JsonPropertyName("Barcodes")]
        public List<string> Barcodes { get; init; } = [];
    }
}
