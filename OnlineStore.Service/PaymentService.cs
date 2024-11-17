using Microsoft.Extensions.Configuration;
using OnlineStore.Core;
using OnlineStore.Core.IRepositories;
using OnlineStore.Core.IServices;
using OnlineStore.Core.Models;
using OnlineStore.Core.Models.Order_Aggregate;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = OnlineStore.Core.Models.Product;

namespace OnlineStore.Service
{
	public class PaymentService : IPaymentService
	{
		private readonly IConfiguration _configuration;
		private readonly IBasketRepository _basketRepository;
		private readonly IUnitOfWork _unitOfWork;

		public PaymentService(IConfiguration configuration , IBasketRepository basketRepository , IUnitOfWork unitOfWork)
        {
			_configuration = configuration;
			_basketRepository = basketRepository;
			_unitOfWork = unitOfWork;
		}
        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string BasketId)
		{
			StripeConfiguration.ApiKey = _configuration["StripeSettings:Secretkey"];
			if (string.IsNullOrEmpty(StripeConfiguration.ApiKey))
			{
				throw new InvalidOperationException("Stripe API key is not configured.");
			}

			var Basket = await _basketRepository.GetBasketAsync(BasketId);

			if (Basket is null) return null;

			var ShippingAddress = 0M;
			if (Basket.DeliveryMethodId.HasValue)
			{
				var DeliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(Basket.DeliveryMethodId.Value);
				if (DeliveryMethod != null)
				{
					ShippingAddress = DeliveryMethod.Cost;
				}
			}
			if (Basket.Items.Count > 0)
			{
				foreach (var item in Basket.Items)
				{
					var Product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
					if (Product != null && item.Price != Product.Price)
						item.Price = Product.Price;
				}
			}

			var SubTotal = Basket.Items.Sum(item => item.Price * item.Quantity);

			var Service = new PaymentIntentService();
			PaymentIntent paymentIntent;

			try
			{

				//Create PaymentIntent
				if (string.IsNullOrEmpty(Basket.PaymentIntentId))
				{
					PaymentIntentCreateOptions options = new PaymentIntentCreateOptions()
					{
						Amount = (long)(SubTotal * 100) + (long)(ShippingAddress * 100),
						Currency = "usd",
						PaymentMethodTypes = new List<string>() { "card" }
					};

					paymentIntent = await Service.CreateAsync(options);
					Basket.PaymentIntentId = paymentIntent.Id;
					Basket.ClientSecret = paymentIntent.ClientSecret;
				}
				//Update PaymentIntent
				else
				{
					PaymentIntentUpdateOptions options = new PaymentIntentUpdateOptions()
					{
						Amount = (long)(SubTotal * 100) + (long)(ShippingAddress * 100)
					};

					paymentIntent = await Service.UpdateAsync(Basket.PaymentIntentId, options);
					Basket.PaymentIntentId = paymentIntent.Id;
					Basket.ClientSecret = paymentIntent.ClientSecret;
				}

				await _basketRepository.UpdateBasketAsync(Basket);

				return Basket;
			}
			catch(StripeException ex)
			{
                Console.WriteLine($"Stripe Error : {ex.Message}");
                return null ;
				
			}
		}
	}
}
