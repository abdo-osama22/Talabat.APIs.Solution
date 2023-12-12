using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Repository;
using Talabat.Core.Service;
using Talabat.Core.Specifications.OrderSpec;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _untiOfWork;
        

        public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork)
        {
            _basketRepo = basketRepo;
            _untiOfWork = unitOfWork;
            //_productRepo = productRepo;
            //_deliveryRepo = deliveryRepo;
            //_orderRepo = orderRepo;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            //1. Get Baskets From Bsket Repo
            var basket = await _basketRepo.GetBasketAsync(basketId);


            //2.Get Selected Items at Basket From Products Repo
            var orderItems = new List<OrderItem>();

            if (basket?.Items.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                   // var productsRepo = _untiOfWork.Repository<Product>();

                    var product =await _untiOfWork.Repository<Product>().GetAsync(item.Id);
                    var productItemsOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItem(productItemsOrdered, product.Price, item.Quantity);
                    orderItems.Add(orderItem);
                }
            }


            //Calculate SubTotal
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

            //4.Get Delivery Method
            //var deliverysRepo = _untiOfWork.Repository<DeliveryMethod>();

            var deliveryMethods =await _untiOfWork.Repository<DeliveryMethod>().GetAsync(deliveryMethodId);

            //5. Create Order
            var order = new Order(buyerEmail, shippingAddress, deliveryMethods, subTotal, orderItems);

            //6. Add Order Locally
            //var OrdersRepo = _untiOfWork.Repository<Order>();

            await _untiOfWork.Repository<Order>().AddAsync(order);

            //7 Save Change
            var result = await _untiOfWork.CompleteAsync();

            if (result <= 0) return null;

            return order;
        }   


        public Task<Order> GetOrderByIdForSpecUserAsync(string buyerEmail, int orderId)
        => _untiOfWork.Repository<Order>().GetWithSpecAsync(new OrderSpecifications(buyerEmail, orderId));
        

        public Task<IReadOnlyList<Order>> GetOrdersForSpecUserAsync(string buyerEmail)
        => _untiOfWork.Repository<Order>().GetAllWithSpecAsync(new OrderSpecifications(buyerEmail));

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        => await _untiOfWork.Repository<DeliveryMethod>().GetAllAsync(); 
        
    }
}
