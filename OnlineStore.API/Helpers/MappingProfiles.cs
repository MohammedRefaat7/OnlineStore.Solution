using AutoMapper;
using OnlineStore.API.DTOs;
using OnlineStore.Core.Models;

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

        }
    }
}
