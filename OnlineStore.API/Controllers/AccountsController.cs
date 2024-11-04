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
		private readonly SignInManager<AppUser> _signInManager;

		public AccountsController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager )
        {
			_userManager = userManager;
			_signInManager = signInManager;
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

		// Login 
		[HttpPost("Login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto model)
		{
			var User = await _userManager.FindByEmailAsync(model.Email);
			if (User is null) return Unauthorized(new ApiErrorResponse(401));

			var Result = await _signInManager.CheckPasswordSignInAsync(User, model.Password, false);
			if (!Result.Succeeded) return Unauthorized(new ApiErrorResponse(401));

			return Ok(new UserDto()
			{
				Email = User.Email , 
				DisplayName = User.DisplayName ,
				Token = "ThisWillBeToken"
			});
		}
	}
}
