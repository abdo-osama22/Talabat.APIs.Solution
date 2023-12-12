using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Core.Service
{
    public interface IOrderService
    {

        Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress);

        Task<IReadOnlyList<Order>> GetOrdersForSpecUserAsync(string buyerEmail);

        Task<Order> GetOrderByIdForSpecUserAsync(string buyerEmail, int orderId);


        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync();
    }
}
