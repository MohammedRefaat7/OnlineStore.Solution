using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.API.DTOs;
using OnlineStore.API.Errors;
using OnlineStore.Core.IServices;
using OnlineStore.Core.Models.Order_Aggregate;
using System.Security.Claims;

namespace OnlineStore.API.Controllers
{
	public class OrdersController : APIBaseController
	{
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;

		public OrdersController(IOrderService OrderService , IMapper Mapper)
        {
			_orderService = OrderService;
			_mapper = Mapper;
		}

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpPost]
		public async Task<ActionResult<Order>> CreateOrder(OrderDTO orderDto)
		{
			var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
			var MappedShippingAddress = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);

		 	var Order =  await _orderService.CreateOrderAsync(BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId , MappedShippingAddress);
			if (Order is null) return BadRequest(new ApiErrorResponse(400, "There is a Problem with your Order"));
			return Ok(Order);
		}
	}
}
