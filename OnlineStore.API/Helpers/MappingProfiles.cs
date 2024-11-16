using AutoMapper;
using OnlineStore.API.DTOs;
using OnlineStore.Core.Models;
using OnlineStore.Core.Models.Identity;
using OnlineStore.Core.Models.Order_Aggregate;

namespace OnlineStore.API.Helpers
{
	public class MappingProfiles : Profile
	{
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDTO>()
                .ForMember(d => d.ProductTypeName, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.ProductBrandName, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.PictureUrl , o => o.MapFrom<ProductPictureUrlResolver>());

            CreateMap<Core.Models.Identity.Address, AddressDto>().ReverseMap();

            CreateMap<CustomerBasketDTO, CustomerBasket>();
            CreateMap<BasketItemDTO, BasketItem>();

            CreateMap<AddressDto, Core.Models.Order_Aggregate.Address>();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(s => s.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(S => S.Product.ProductName))
                .ForMember(d => d.PicturUrl, o => o.MapFrom(s => s.Product.PicturUrl))
                .ForMember(d => d.PicturUrl , o => o.MapFrom<OrderItemPictureUrlResolver>());
        }
    }
}
