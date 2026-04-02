using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmsTestApp.ConsoleClient.Repository.Model
{
    /// <summary>
    /// Штрихкод, по которому будет происходить идентификация блюда.
    /// </summary>
    internal sealed class Barcode
    {
        /// <summary>
        /// Значение штрихкода.
        /// </summary>
        [Key]
        public required string Value { get; set; }

        /// <summary>
        /// Идентификатор родительского блюда.
        /// </summary>
        public required string MenuItemId { get; set; }

        /// <summary>
        /// Ссылка на родительский элемент меню.
        /// </summary>
        [ForeignKey(nameof(MenuItemId))]
        public MenuItem? MenuItem { get; set; }
    }
}
