using OnlineStore.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.API.DTOs
{
	public class CustomerBasketDTO
	{
		[Required]
		public string Id { get; set; }
		public List<BasketItemDTO> Items { get; set; }
	}
}
