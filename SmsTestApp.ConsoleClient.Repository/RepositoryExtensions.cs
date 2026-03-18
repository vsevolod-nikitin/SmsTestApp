using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmsTestApp.ConsoleClient.Repository.Implementation;

namespace SmsTestApp.ConsoleClient.Repository
{
    /// <summary>
    /// Расширения классов.
    /// </summary>
    public static class RepositoryExtensions
    {
        /// <summary>
        /// Добавить репозитории в коллекцию сервисов для внедрения зависимостей.
        /// </summary>
        /// <param name="services">Функционал построения.</param>
        /// <param name="dbConnectionString">Строка соединения с БД.</param>
        /// <remarks>Добавляет реализацию <see cref="IMenuStorage"/>.</remarks>
        public static void AddRepositories(this IServiceCollection services, string dbConnectionString)
        {
            services.AddSingleton<IMenuStorage, MenuStorage>();

            services.AddDbContextFactory<StorageContext>(options =>
            {
                options.UseNpgsql(dbConnectionString);
            });
        }
    }
}
