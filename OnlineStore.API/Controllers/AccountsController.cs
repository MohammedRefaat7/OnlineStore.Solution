using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.API.DTOs;
using OnlineStore.API.Errors;
using OnlineStore.API.Extensions;
using OnlineStore.Core.IServices;
using OnlineStore.Core.Models.Identity;
using System.Security.Claims;

namespace OnlineStore.API.Controllers
{
	public class AccountsController : APIBaseController
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly ITokenService _tokenService;
		private readonly IMapper _mapper;

		public AccountsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, 
			                      ITokenService tokenService , IMapper mapper)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenService = tokenService;
			_mapper = mapper;
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
					Token = await _tokenService.CreateTokenAsync(User, _userManager)
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
				Email = User.Email,
				DisplayName = User.DisplayName,
				Token = await _tokenService.CreateTokenAsync(User, _userManager)
			});
		}

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpGet("CurrentUser")]
		public async Task<ActionResult<UserDto>> GetCurrentUser()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var user = await _userManager.FindByEmailAsync(email);

			var ReturnedUser = new UserDto()
			{
				Email = user.Email,
				DisplayName = user.DisplayName,
				Token = await _tokenService.CreateTokenAsync(user, _userManager)
			};
			return Ok(ReturnedUser);
		}

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpGet("Address")]
		public async Task<ActionResult<AddressDto?>> GetCurrentUserAddress()
		{
			var user = await _userManager.FindUserWithAddressAsync(User);

			if(user?.Address is null)
			{
				return NotFound(new ApiErrorResponse(404, "The current User doesn't have an associated Address."));
			}
			var MappedAdrress = _mapper.Map<Address, AddressDto>(user.Address);

			return Ok(MappedAdrress);

		}
	}
}
