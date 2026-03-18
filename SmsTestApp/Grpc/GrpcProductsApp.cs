using SmsTestApp.Contracts.Menu;
using SmsTestApp.Contracts.Order;

namespace SmsTestApp.Grpc
{
    internal sealed class GrpcProductsApp : IProductsApp
    {
        public Task<IEnumerable<MenuItem>> GetMenuAsync(bool withPrices)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendOrderAsync(Guid orderId, IEnumerable<OrderItem> items)
        {
            throw new NotImplementedException();
        }
    }
}
