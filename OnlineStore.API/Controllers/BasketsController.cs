using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.API.Errors;
using OnlineStore.Core.IRepositories;
using OnlineStore.Core.Models;

namespace OnlineStore.API.Controllers
{
	
	public class BasketsController : APIBaseController
	{
		private readonly IBasketRepository _basketRepository;

		public BasketsController(IBasketRepository BasketRepository)
		{
			_basketRepository = BasketRepository;
		}

		// Get or Recreate new Basket
		[HttpGet]
		public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string BasketId)
		{
		 	var CustomerBasket = await _basketRepository.GetBasketAsync(BasketId);
			return CustomerBasket is null ? new CustomerBasket(BasketId) : CustomerBasket;
		}

		// Update Or Create new Basket
		[HttpPost]
		public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
		{
			var CreatedOrUpdatedBasket = await _basketRepository.UpdateBasketAsync(basket);

			if (CreatedOrUpdatedBasket is null) 
				return BadRequest(new ApiErrorResponse(400));
			else 
				return Ok(CreatedOrUpdatedBasket);	
		}

		//Delete Basket
		[HttpDelete]
		public async Task<ActionResult<bool>> DeleteBasket(string BasketId)
		{
			return await _basketRepository.DeleteBasketAsync(BasketId);
		}

		
	}
}
