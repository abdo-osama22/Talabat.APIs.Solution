using AutoMapper;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.OrderAggregate;
using TalabatAPIs.DTOs;
using TalabatAPIs.Helpers;
using OrderAddress = Talabat.Core.Entities.OrderAggregate.Address;
using IdentityAddress = Talabat.Core.Entities.Identity.Address;

namespace TalabatAPIs.Helper
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                   .ForMember(d => d.ProductBrand,o => o.MapFrom(s => s.ProductBrand.Name))
                   .ForMember(d=> d.ProductType,o => o.MapFrom(s => s.ProductType.Name))
                   .ForMember(d=> d.PictureUrl,o => o.MapFrom<ProductPictureUrlResolver>());

            CreateMap<IdentityAddress, AddressDto>().ReverseMap();

            CreateMap<AddressDto, OrderAddress>().ReverseMap();

            CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem>().ReverseMap();



            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, op => op.MapFrom( o => o.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCost, op => op.MapFrom( o => o.DeliveryMethod.Cost));


            CreateMap<OrderItem, OrderItemDto>().
               ForMember(d => d.ProductId, op => op.MapFrom(o => o.Product.ProductId)).
               ForMember(d => d.ProductName, op => op.MapFrom(o => o.Product.ProductName)).
               ForMember(d => d.PictureUrl, op => op.MapFrom(o => o.Product.PictureUrl)).
               ForMember(d => d.PictureUrl, op => op.MapFrom<OrderItemPictureUrlResolver>() );



        }

    }
}
