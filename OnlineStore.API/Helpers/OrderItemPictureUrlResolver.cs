using AutoMapper;
using OnlineStore.API.DTOs;
using OnlineStore.Core.Models.Order_Aggregate;

namespace OnlineStore.API.Helpers
{
	public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
	{
		private readonly IConfiguration _configuration;

		public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
			this._configuration = configuration;
		}
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.Product.PicturUrl))
			{
				return $"{_configuration["ApiBaseUrl"]}/{source.Product.PicturUrl}";
			}
			return string.Empty ;
		}
	}
}
