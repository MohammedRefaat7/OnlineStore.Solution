using OnlineStore.Core.Models.Order_Aggregate;

namespace OnlineStore.API.DTOs
{
	public class OrderToReturnDto
	{

		public string BuyerEmail { get; set; }
		public DateTimeOffset DateTimeOffset { get; set; } 
		public string status { get; set; } 
		public Address ShippingAddress { get; set; }
		public string DeliveryMethod { get; set; }
		public decimal DeliveryMethodCost { get; set; }
		public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();
		public decimal SubTotal { get; set; }
		public decimal Total { get; set; }
		public string PaymentIntentId { get; set; } 
	}
}
