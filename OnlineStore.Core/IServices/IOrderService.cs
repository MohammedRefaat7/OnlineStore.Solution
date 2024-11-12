using OnlineStore.Core.Models.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.IServices
{
	public interface IOrderService
	{
		// Create Order
		Task<Order> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress);

		// Get All Orders For Specific User
		Task<IReadOnlyList<Order>> GetOrdersForSpecificUserAsync(string BuyerEmail);

		// Get Order By ID For Specific User
		Task<Order> GetOrderByIdForSpecificUserAsync(string BuyerEmail, int OrderId);
	}
}
