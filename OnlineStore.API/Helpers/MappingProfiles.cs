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
        }
    }
}
