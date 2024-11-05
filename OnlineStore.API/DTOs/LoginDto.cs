using System.ComponentModel.DataAnnotations;

namespace OnlineStore.API.DTOs
{
	public class LoginDto
	{
		[Required(ErrorMessage = "Email is Required")]
		[EmailAddress(ErrorMessage ="Invalid Email")]
		public string Email { get; set; }

		[Required(ErrorMessage ="Password is Required")]
		public string Password { get; set; }
	}
}
