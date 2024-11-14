using System.ComponentModel.DataAnnotations;

namespace OnlineStore.API.DTOs
{
	public class OrderDTO
	{
		[Required]
        public string BasketId { get; set; }

        [Required(ErrorMessage = "Please select a Delivery Method")]

		public int DeliveryMethodId { get; set; }

        [Required]
        public AddressDto ShippingAddress { get; set; }
    }
}
