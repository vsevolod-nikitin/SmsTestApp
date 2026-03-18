using SmsTestApp.Contracts.Menu;
using SmsTestApp.Contracts.Order;

namespace SmsTestApp.Grpc
{
    internal sealed class GrpcProductsApp : IProductsApp
    {
        public Task<IEnumerable<MenuItemDto>> GetMenuAsync(bool withPrices)
        {
            throw new NotImplementedException();
        }

        public Task SendOrderAsync(Guid orderId, IEnumerable<OrderItemDto> items)
        {
            throw new NotImplementedException();
        }
    }
}
