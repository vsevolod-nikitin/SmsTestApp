using SmsTestApp.Contracts;
using SmsTestApp.Contracts.Menu;
using SmsTestApp.Contracts.Order;
using System.Net.Http.Json;
using System.Text.Json;

namespace SmsTestApp.Rest
{
    /// <summary>
    /// Http -клиент для взаимодействия с API продуктов.
    /// </summary>
    /// <param name="clientFactory">Фабрика клиентов для выполнения запросов.</param>
    internal sealed class RestProductsApp(IHttpClientFactory clientFactory) : IProductsApp
    {
        private static readonly string Endpoint = "v1/execute-request";

        /// <inheritdoc/>
        public async Task<IEnumerable<MenuItem>> GetMenuAsync(bool withPrice)
        {
            var client = clientFactory.CreateClient(nameof(RestProductsApp));
            var payload = new Request
            {
                Command = "GetMenu",
                CommandParameters = new GetMenuRequest
                {
                    WithPrice = withPrice
                }
            };

            var response = await client.PostAsJsonAsync(Endpoint, payload);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadFromJsonAsync<Response>()
                ?? throw new InvalidOperationException("Empty response");

            if (!content.Success)
            {
                throw new InvalidOperationException(content.ErrorMessage);
            }

            var data = JsonSerializer.Deserialize<GetMenuResponse>(content.Data?.ToString() ?? string.Empty);
            return data?.Items ?? [];
        }

        /// <inheritdoc/>
        public Task<bool> SendOrderAsync(Guid orderId, IEnumerable<OrderItem> items)
        {
            throw new NotImplementedException();
        }
    }
}
