using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmsTestApp.ConsoleClient.Implementation;
using SmsTestApp.ConsoleClient.Repository;

namespace SmsTestApp.ConsoleClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new ServiceCollection();
            RegisterApp(builder, configuration);

            var serviceProvider = builder.BuildServiceProvider();
            var manager = serviceProvider.GetRequiredService<ProductsManager>();

            await manager.RunAsync();

            // Блокируем консоль, чтобы увидеть результат.
            Console.ReadLine();
        }

        /// <summary>
        /// Сформировать приложение.
        /// </summary>
        /// <param name="services">Функционал построения.</param>
        /// <param name="configuration">Конфигурация функционала.</param>
        private static void RegisterApp(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ProductsManager>();
            services.AddTransient<IUserInteractor, UserInteractor>();

            services.AddProductsApp(options =>
            {
                options.HttpEndpoint = configuration["HttpEndpoint"];
            });

            services.AddRepositories(configuration.GetConnectionString("StorageDB"));
        }
    }
}
