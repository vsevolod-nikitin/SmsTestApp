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
        /// <inheritdoc/>
        public async Task<IEnumerable<MenuItemDto>> GetMenuAsync(bool withPrice)
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

            var response = await client.PostAsJsonAsync(string.Empty, payload);
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
        public async Task SendOrderAsync(string orderId, IEnumerable<OrderItemDto> items)
        {
            var client = clientFactory.CreateClient(nameof(RestProductsApp));
            var payload = new Request
            {
                Command = "SendOrder",
                CommandParameters = new SendOrderCommand
                {
                    OrderId = orderId,
                    Items = [.. items],
                }
            };

            var response = await client.PostAsJsonAsync(string.Empty, payload);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadFromJsonAsync<Response>()
                ?? throw new InvalidOperationException("Empty response");

            if (!content.Success)
            {
                throw new InvalidOperationException(content.ErrorMessage);
            }
        }
    }
}
