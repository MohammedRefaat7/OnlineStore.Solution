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

		public OrdersController(IOrderService OrderService, IMapper Mapper)
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

			var Order = await _orderService.CreateOrderAsync(BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, MappedShippingAddress);
			if (Order is null) return BadRequest(new ApiErrorResponse(400, "There is a Problem with your Order"));
			return Ok(Order);
		}

		[ProducesResponseType(typeof(IReadOnlyList<Order>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpGet("All")]
		public async Task<ActionResult<IReadOnlyList<Order>>> GetAllOrdersForUser()
		{
			var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
			var Orders = await _orderService.GetOrdersForSpecificUserAsync(BuyerEmail);
			if (Orders.Count <= 0)
				return NotFound(new ApiErrorResponse(404, "There is no Orders for this User"));

			return Ok(Orders);
		}

		[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(Order) , StatusCodes.Status200OK)]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpGet("{id}")]
		public async Task<ActionResult<Order>> GetOrderByIdForSpecificUser( int id)
		{

			string BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
			var order = await _orderService.GetOrderByIdForSpecificUserAsync(BuyerEmail, id);
			if (order is null) return NotFound(new ApiErrorResponse(404, $"Order with ID {id} not found for this User."));
			return Ok(order);
		}
	}
}
