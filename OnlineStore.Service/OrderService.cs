using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using OnlineStore.Core;
using OnlineStore.Core.IRepositories;
using OnlineStore.Core.IServices;
using OnlineStore.Core.Models;
using OnlineStore.Core.Models.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service
{
	public class OrderService : IOrderService
	{
		
		private readonly IBasketRepository _basketRepository;
		private readonly IUnitOfWork _unitOfWork;

		public OrderService(IBasketRepository BasketRepository, IUnitOfWork unitOfWork)
        {	
			_basketRepository = BasketRepository;
			_unitOfWork = unitOfWork;
		}
        public async Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress)
		{

			//1.Get Basket From Basket Repo
			var Basket = await _basketRepository.GetBasketAsync(BasketId);

		   //2.Get Selected Items at Basket From Product Repo
			var OrderItems = new List<OrderItem>();

			if(Basket?.Items.Count > 0)
			{
				foreach(var item in Basket.Items)
				{
					var Product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);

					var ProductItemOrdered = new ProductItemOrdered(Product.Id, Product.Name, Product.PictureUrl);
					var OrderItem = new OrderItem(ProductItemOrdered, item.Quantity, Product.Price);
					OrderItems.Add(OrderItem);
				}
			}

		   //3.Calculate SubTotal
			var SubTotal = OrderItems.Sum(I => I.Price * I.Quantity);

		   //4.Get Delivery Method From DeliveryMethod Repo
			var DeliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(DeliveryMethodId);

		   //5.Create Order
			var Order = new Order(BuyerEmail, ShippingAddress, DeliveryMethod, OrderItems, SubTotal);

		   //6.Add Order Locally
		    await _unitOfWork.Repository<Order>().AddAsync(Order);

			//7.Save Order To Database
			var Result = await _unitOfWork.CompleteAsync();

			if (Result <= 0) return null;

			return Order;
		}

		public Task<Order> GetOrderByIdForSpecificUserAsync(string BuyerEmail, int OrderId)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyList<Order>> GetOrdersForSpecificUserAsync(string BuyerEmail)
		{
			throw new NotImplementedException();
		}
	}
}
