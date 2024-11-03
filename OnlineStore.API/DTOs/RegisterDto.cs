using System.ComponentModel.DataAnnotations;

namespace OnlineStore.API.DTOs
{
	public class RegisterDto
	{
		[Required]
		public string DisplayName { get; set; }

		[Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [RegularExpression("(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$" ,
                      ErrorMessage = "Password Must Contains 1 Uppercase, 1 Lowercase, 1 Digit, 1 Spicial Character")]
        public string Password{ get; set; }



    }
}
