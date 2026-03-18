using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Sms.Test;
using SmsTestApp.Contracts.Menu;
using SmsTestApp.Contracts.Order;
using System.Globalization;

namespace SmsTestApp.Grpc
{
    /// <summary>
    /// Клиент для взаимодействия с gRPC API тестового сервиса SMS.
    /// </summary>
    /// <param name="client">gRPC-клиент для вызова методов сервиса.</param>
    internal sealed class GrpcProductsApp(SmsTestService.SmsTestServiceClient client) : IProductsApp
    {
        /// <inheritdoc/>
        public async Task<IEnumerable<MenuItemDto>> GetMenuAsync(bool withPrices)
        {
            var request = new BoolValue { Value = withPrices };

            Sms.Test.GetMenuResponse response;
            try
            {
                response = await client.GetMenuAsync(request);
            }
            catch (RpcException ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }

            if (!response.Success)
            {
                throw new InvalidOperationException(response.ErrorMessage);
            }

            // Преобразуем полученные из gRPC данных в DTO.
            var result = response.MenuItems.Select(item => new MenuItemDto
            {
                Id = item.Id,
                Article = item.Article,
                Name = item.Name,
                // При запросе без цен поле Price трактуется как отсутствующее в контракте;
                // на сервере при отсутствии цены возвращается 0.0, поэтому учитываем флаг withPrices.
                Price = withPrices ? (decimal?)item.Price : null,
                IsWeighted = item.IsWeighted,
                FullPath = item.FullPath,
                Barcodes = [..item.Barcodes]
            }).ToList();

            return result;
        }

        /// <inheritdoc/>
        public async Task SendOrderAsync(string orderId, IEnumerable<OrderItemDto> items)
        {
            var order = new Order
            {
                Id = orderId,
            };

            foreach (var dto in items)
            {
                // В gRPC контракте количество представлено в виде double, тогда как в REST варианте это строка.
                double.TryParse(dto.Quantity, NumberStyles.Number, CultureInfo.InvariantCulture, out double quantity);

                order.OrderItems.Add(new OrderItem
                {
                    Id = dto.MenuItemId,
                    Quantity = quantity,
                });
            }

            SendOrderResponse response;
            try
            {
                response = await client.SendOrderAsync(order);
            }
            catch (RpcException ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }

            if (!response.Success)
            {
                throw new InvalidOperationException(response.ErrorMessage);
            }
        }
    }
}
