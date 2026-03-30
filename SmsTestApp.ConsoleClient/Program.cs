using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using SmsTestApp.ConsoleClient.Implementation;
using SmsTestApp.ConsoleClient.Orders;
using SmsTestApp.ConsoleClient.Orders.Implementation;
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

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console(outputTemplate: "{Message:lj}{NewLine}")
                .WriteTo.File($"Logs/test-sms-console-app-{DateTime.Now:yyyyMMdd}.log", outputTemplate: "{Message:lj}{NewLine}")
                .CreateLogger();

            var builder = new ServiceCollection();
            builder.AddLogging(lb =>
            {
                lb.ClearProviders();
                lb.AddSerilog(Log.Logger, dispose: true);
            });

            RegisterApp(builder, configuration);

            var serviceProvider = builder.BuildServiceProvider();
            var manager = serviceProvider.GetRequiredService<IProductsManager>();

            try
            {
                await manager.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }
            finally
            {
                // Гарантированно сбрасываем и закрываем Serilog.
                Log.CloseAndFlush();
            }

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
            services.AddTransient<IProductsManager, ProductsManager>();
            services.AddTransient<IUserInteractor, UserInteractor>();
            services.AddTransient<IOrderFactory, OrderFactory>();

            services.AddProductsApp(options =>
            {
                options.HttpEndpoint = configuration["HttpEndpoint"];
                options.HttpUsername = configuration["HttpUsername"];
                options.HttpPassword = configuration["HttpPassword"];

                options.GrpcEndpoint = configuration["GrpcEndpoint"];
            });

            services.AddRepositories(configuration.GetConnectionString("StorageDB"));
        }
    }
}
