using SmsTestApp.Api.Services.Implementation;

namespace SmsTestApp.Api.Services
{
    /// <summary>
    /// Расширения классов.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Произвести конфигурацию функционала.
        /// </summary>
        /// <param name="services">Функционал построения.</param>
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}
