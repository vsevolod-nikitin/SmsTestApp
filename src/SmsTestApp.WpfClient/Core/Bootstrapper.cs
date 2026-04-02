using System.Reflection;
using System.Windows;
using Caliburn.Micro;
using Microsoft.Extensions.DependencyInjection;
using SmsTestApp.WpfClient.UI;

namespace SmsTestApp.WpfClient.Core
{
    /// <summary>
    /// Базовая реализация загрузчика Caliburn.Micro.
    /// </summary>
    internal abstract class Bootstrapper : BootstrapperBase
    {
        private ServiceProvider? _serviceProvider;

        public Bootstrapper()
        {
            Initialize();
        }

        /// <summary>
        /// Конфигурация сервисов.
        /// </summary>
        protected override void Configure()
        {
            var services = new ServiceCollection();

            // Регистрация основных сервисов Caliburn.Micro
            services.AddSingleton<IWindowManager, WindowManager>();
            services.AddSingleton<IEventAggregator, EventAggregator>();

            // Регистрируем все модели представления
            var asm = Assembly.GetExecutingAssembly();
            var viewModelTypes = asm.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("ViewModel", StringComparison.Ordinal));

            foreach (var vm in viewModelTypes)
            {
                services.AddTransient(vm);
            }

            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        /// <summary>
        /// Произвести регистрацию сервисов.
        /// </summary>
        /// <param name="services">Функционал построения.</param>
        protected abstract void ConfigureServices(IServiceCollection services);

        /// <summary>
        /// Разрешение одного экземпляра сервиса.
        /// </summary>
        protected override object GetInstance(Type service, string? key)
        {
            if (_serviceProvider is null)
            {
                throw new InvalidOperationException("Service provider not configured.");
            }

            var instance = _serviceProvider.GetService(service);
            if (instance != null)
            {
                return instance;
            }

            throw new Exception($"Could not locate any instances of contract { (key ?? service.FullName) }.");
        }

        /// <summary>
        /// Разрешение всех зарегистрированных экземпляров сервиса.
        /// </summary>
        protected override IEnumerable<object?> GetAllInstances(Type service)
        {
            if (_serviceProvider is null)
            {
                throw new InvalidOperationException("Service provider not configured.");
            }

            return _serviceProvider.GetServices(service) ?? [];
        }

        /// <summary>
        /// Построение приложения.
        /// </summary>
        protected override void BuildUp(object instance)
        {
            if (_serviceProvider is null)
            {
                throw new InvalidOperationException("Service provider not configured.");
            }

            var props = instance.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanWrite && p.GetIndexParameters().Length == 0);

            foreach (var prop in props)
            {
                var service = _serviceProvider.GetService(prop.PropertyType);
                if (service is null)
                {
                    continue;
                }

                prop.SetValue(instance, service);
            }
        }

        /// <summary>
        /// Запуск приложения.
        /// </summary>
        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
            if (_serviceProvider is null)
            {
                throw new InvalidOperationException("Service provider not configured.");
            }

            await DisplayRootViewForAsync<ShellViewModel>();
        }
    }
}
