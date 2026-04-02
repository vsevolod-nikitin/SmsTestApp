using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using SmsTestApp.WpfClient.Core;
using SmsTestApp.WpfClient.Data;
using SmsTestApp.WpfClient.Data.Implementation;
using SmsTestApp.WpfClient.UI;
using System.Windows;

namespace SmsTestApp.WpfClient
{
    /// <summary>
    /// Загрузчик функционала.
    /// </summary>
    internal sealed class AppBootstrapper : Bootstrapper
    {
        /// <inheritdoc/>
        protected override void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            services.AddSingleton<IConfiguration>(configuration);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.File($"Logs/test-sms-wpf-app-{DateTime.Now:yyyyMMdd}.log")
                .CreateLogger();

            services.AddLogging(lb =>
            {
                lb.ClearProviders();
                lb.AddSerilog(Log.Logger, dispose: true);
            });

            services.AddSingleton<IVariablesProvider, VariablesProvider>();
        }

        /// <summary>
        /// Запуск приложения.
        /// </summary>
        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
            await DisplayRootViewForAsync<ShellViewModel>();
        }
    }
}
