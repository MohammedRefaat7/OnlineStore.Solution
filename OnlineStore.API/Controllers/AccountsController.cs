using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.API.DTOs;
using OnlineStore.API.Errors;
using OnlineStore.Core.Models.Identity;

namespace OnlineStore.API.Controllers
{
	public class AccountsController : APIBaseController
	{
		private readonly UserManager<AppUser> _userManager;

		public AccountsController(UserManager<AppUser> userManager )
        {
			_userManager = userManager;
		}


        // Register
        [HttpPost("Register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto model)
		{
			var User = new AppUser()
			{
				Email = model.Email,
				UserName = model.Email.Split('@')[0],
				DisplayName = model.DisplayName,
				PhoneNumber = model.PhoneNumber,
			};
			var Result = await _userManager.CreateAsync(User, model.Password);

			if (!Result.Succeeded)
				return BadRequest(new ApiErrorResponse(400));
			else
			{
				var ReturnedUser = new UserDto()
				{
					Email = User.Email,
					DisplayName = User.DisplayName,
					Token = "ThisWillBeToken!!"
				};
				return Ok(ReturnedUser);
			}
		}
	}
}
