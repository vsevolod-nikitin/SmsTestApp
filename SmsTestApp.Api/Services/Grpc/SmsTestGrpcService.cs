using Grpc.Core;
using Google.Protobuf.WellKnownTypes;
using Sms.Test;
using SmsTestApp.Contracts.Order;

namespace SmsTestApp.Api.Services.Grpc
{
    /// <summary>
    /// Реализация gRPC-сервиса.
    /// </summary>
    /// <param name="menuService">Сервис для работы с меню.</param>
    /// <param name="orderService">Сервис для работы с заказами.</param>
    public class SmsTestGrpcService(
        IMenuService menuService,
        IOrderService orderService)
        : SmsTestService.SmsTestServiceBase
    {
        /// <summary>
        /// Получить меню.
        /// </summary>
        public override async Task<GetMenuResponse> GetMenu(BoolValue request, ServerCallContext context)
        {
            var response = new GetMenuResponse();
            try
            {
                var withPrice = request.Value;
                var menuDtos = await menuService.GetMenuAsync(withPrice);

                response.Success = true;
                foreach (var dto in menuDtos)
                {
                    var item = new MenuItem
                    {
                        Id = dto.Id,
                        Article = dto.Article,
                        Name = dto.Name,
                        Price = dto.Price.HasValue ? (double)dto.Price.Value : 0.0,
                        IsWeighted = dto.IsWeighted,
                        FullPath = dto.FullPath,
                    };

                    item.Barcodes.AddRange(dto.Barcodes);

                    response.MenuItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Сформировать заказ.
        /// </summary>
        public override async Task<SendOrderResponse> SendOrder(Order request, ServerCallContext context)
        {
            var response = new SendOrderResponse();

            // Преобразование данных из gRPC-моделей в DTO для сервиса заказов.
            // В gRPC количество является числом, а в REST — строкой, поэтому конвертируем его.
            var orderItems = request.OrderItems.Select(item => new OrderItemDto
            {
                MenuItemId = item.Id,
                Quantity = item.Quantity.ToString(),
            }).ToList();

            try
            {
                await orderService.SendOrderAsync(request.Id, orderItems);
                response.Success = true;
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
    }
}