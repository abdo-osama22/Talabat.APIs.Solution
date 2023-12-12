using AutoMapper;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OrderAggregate;
using TalabatAPIs.DTOs;

namespace TalabatAPIs.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            string url = !string.IsNullOrEmpty(source.PictureUrl) ? $"{_configuration["ApiBaseUrl"]}{source.PictureUrl}" : "";

            return url;


        }
    }


    public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            string url = !string.IsNullOrEmpty(source.Product.PictureUrl) ?
                $"{_configuration["ApiBaseUrl"]}{source.Product.PictureUrl}" : "";

            return url;


        }
    }
}
