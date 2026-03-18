using Microsoft.Extensions.DependencyInjection;
using SmsTestApp.Grpc;
using SmsTestApp.Rest;
using System.Net.Http.Headers;
using System.Text;

namespace SmsTestApp
{
    /// <summary>
    /// Расширения классов.
    /// </summary>
    public static class AppExtensions
    {
        /// <summary>
        /// Добавить реализацию <see cref="IProductsApp"/> для взаимодействия с продуктами.
        /// </summary>
        /// <param name="services">Функционал построения.</param>
        /// <param name="optionsFactory">Параметры взаимодействия.</param>
        public static void AddProductsApp(this IServiceCollection services, Action<ProductsAppOptions> optionsFactory)
        {
            var options = new ProductsAppOptions();
            optionsFactory(options);

            if (!string.IsNullOrEmpty(options.GrpcEndpoint))
            {
                services.AddGrpcProductsApp(options);
                return;
            }

            if (!string.IsNullOrEmpty(options.HttpEndpoint))
            {
                services.AddRestProductsApp(options);
                return;
            }

            throw new InvalidOperationException("Не указано ни одной из конечных точек для взаимодействия с продуктами.");
        }

        private static void AddGrpcProductsApp(this IServiceCollection services, ProductsAppOptions options)
        {
            services.AddTransient<IProductsApp, GrpcProductsApp>();
        }

        /// <summary>
        /// Регистрация Http-клиента для взаимодействия с продуктами через REST API.
        /// </summary>
        /// <param name="services">Функционал построения.</param>
        /// <param name="options">Конфигурация.</param>
        private static void AddRestProductsApp(this IServiceCollection services, ProductsAppOptions options)
        {
            services.AddTransient<IProductsApp, RestProductsApp>();
            services.AddHttpClient<RestProductsApp>(client =>
            {
                client.BaseAddress = new Uri(options.HttpEndpoint!);
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                if (!string.IsNullOrEmpty(options.HttpUsername) && !string.IsNullOrEmpty(options.HttpPassword))
                {
                    var byteArray = Encoding.ASCII.GetBytes($"{options.HttpUsername}:{options.HttpPassword}");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                }
            });
        }
    }
}
