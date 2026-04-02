using Microsoft.EntityFrameworkCore;
using SmsTestApp.ConsoleClient.Repository.Model;

namespace SmsTestApp.ConsoleClient.Repository
{
    /// <summary>
    /// Контекст хранения для доступа к базе данных через Entity Framework Core.
    /// </summary>
    internal sealed class StorageContext : DbContext
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="StorageContext"/> с указанными опциями.
        /// При создании контекста выполняется проверка и создание базы данных, если она ещё не существует.
        /// </summary>
        /// <param name="options">Опции конфигурации для <see cref="StorageContext"/>.</param>
        public StorageContext(DbContextOptions<StorageContext> options)
            : base(options)
        {
            // Создание базы данных при инициализации контекста, если она еще не существует.
            Database.EnsureCreated();
        }

        /// <summary>
        /// Коллекция записей меню.
        /// </summary>
        public DbSet<MenuItem> MenuItems { get; set; } = null!;

        /// <summary>
        /// Коллекция штрих-кодов, связанных с элементами меню.
        /// </summary>
        public DbSet<Barcode> Barcodes { get; set; } = null!;
    }
}
