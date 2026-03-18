using System.ComponentModel.DataAnnotations;

namespace SmsTestApp.ConsoleClient.Repository.Model
{
    /// <summary>
    /// Информация о блюде.
    /// </summary>
    internal sealed class MenuItem
    {
        /// <summary>
        /// Уникальный идентификатор позиции меню.
        /// </summary>
        [Key]
        public required string Id { get; set; }

        /// <summary>
        /// Внутренний артикул/код товара.
        /// </summary>
        public required string Article { get; set; }

        /// <summary>
        /// Наименование блюда.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Цена за единицу.
        /// </summary>
        public required decimal Price { get; set; }

        /// <summary>
        /// Признак взвешиваемого товара (true — измеряется по весу).
        /// </summary>
        public required bool IsWeighted { get; set; }

        /// <summary>
        /// Полный путь категории/раздела в меню.
        /// </summary>
        public required string FullPath { get; set; }

        /// <summary>
        /// Список штрихкодов, связанных с позицией.
        /// </summary>
        public List<Barcode> Barcodes { get; init; } = [];
    }
}
