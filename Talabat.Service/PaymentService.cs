using Microsoft.Extensions.Configuration;
using Stripe;
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
using Product = Talabat.Core.Entities.Product;

namespace Talabat.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IConfiguration configuration,IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeKey:Secretkey"];
            var Basket =await _basketRepository.GetBasketAsync(basketId);
            if (Basket is null) return null;


            decimal shippingPrice = 0m;
            if (Basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(Basket.DeliveryMethodId.Value);
                shippingPrice = deliveryMethod.Cost;
            }

            if(Basket.Items.Count > 0)
            {
                foreach (var item in Basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetAsync(item.Id);
                    if (item.Price != product.Price)
                        item.Price = product.Price;
                }
            }


            var supTotal = Basket.Items.Sum(i => i.Price * i.Quantity);

            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(Basket.PaymentIntentId))
            {
                var option = new PaymentIntentCreateOptions()
                {
                    Amount =(long) (supTotal * 100  + shippingPrice * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card"}
                };

                paymentIntent =await service.CreateAsync(option);
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)(supTotal * 100 + shippingPrice * 100),
                };
               
                 paymentIntent = await service.UpdateAsync(Basket.PaymentIntentId,options);
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;
            }

           await _basketRepository.UpdateBasketAsync(Basket);
            return Basket;
        }
    }
}
