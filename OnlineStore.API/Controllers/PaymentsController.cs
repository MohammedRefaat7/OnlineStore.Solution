using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.API.DTOs;
using OnlineStore.API.Errors;
using OnlineStore.Core.IServices;
using Stripe;

namespace OnlineStore.API.Controllers
{
	
	public class PaymentsController : APIBaseController
	{
		private readonly IPaymentService _paymentService;

		public PaymentsController(IPaymentService paymentService)
        {
			_paymentService = paymentService;
		}

        [HttpPost]
		public async Task<ActionResult<CustomerBasketDTO>> CreateOrUpdatePaymentIntent(string BasketId)
		{
			if (string.IsNullOrEmpty(BasketId))
				return BadRequest(new ApiErrorResponse(400, "Basket ID must be provided."));

			try
			{
				var basket = await _paymentService.CreateOrUpdatePaymentIntent(BasketId);

				if (basket is null)
					return NotFound(new ApiErrorResponse(404, "Basket not found or failed to create/update payment intent."));

				return Ok(basket);
			}
			catch (InvalidOperationException ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
			}
			catch (Exception ex) 
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
			}

		}
	}
}
