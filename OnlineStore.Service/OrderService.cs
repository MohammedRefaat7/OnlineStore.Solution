using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
		private readonly IGenericRepository<DeliveryMethod> _deliveryMethodRepo;
		private readonly IBasketRepository _basketRepository;
		private readonly IGenericRepository<Product> _productRepo;
		private readonly IGenericRepository<Order> _orderRepo;

		public OrderService(IGenericRepository<DeliveryMethod> DeliveryMethodRepo , IBasketRepository BasketRepository,
			                IGenericRepository<Product> ProductRepo , IGenericRepository<Order> OrderRepo)
        {
			_deliveryMethodRepo = DeliveryMethodRepo;
			_basketRepository = BasketRepository;
			_productRepo = ProductRepo;
			_orderRepo = OrderRepo;
		}
        public async Task<Order> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress)
		{

			//1.Get Basket From Basket Repo
			 //var Basket = await _basketRepository.GetBasketAsync(BasketId);

		    //2.Get Selected Items at Basket From Product Repo
			 //var OrderItems = new List<OrderItem>();
			 //if (Basket?.Items.Count > 0)
			//{
			//	foreach (var item in Basket.Items)
			//	{
			//		var product = await _productRepo.GetByIdAsync(item.Id);

			//		var productItemOrdered = new ProductItemOrdered(item.Id, product.Name, product.PictureUrl);
			//		var orderitem = new OrderItem(productItemOrdered, item.Quantity, product.Price);
			//		OrderItems.Add(orderitem);
			//	}
			//}

			
			//3.Calculate SubTotal
			 //var SubTotal = OrderItems.Sum(I => I.Price * I.Quantity);

			//4.Get Delivery Method From DeliveryMethod Repo
			 //var DeliveryMethod = await _deliveryMethodRepo.GetByIdAsync(DeliveryMethodId);

			//5.Create Order
			 //var Order = new Order(BuyerEmail, ShippingAddress, DeliveryMethod, OrderItems, SubTotal);
            
			//6.Add Order Locally
			 //await _orderRepo.AddAsync(Order);
			
			//7.Save Order To Database
			throw new NotImplementedException();
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
