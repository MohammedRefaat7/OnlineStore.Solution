using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Models.Identity;
using System.Security.Claims;

namespace OnlineStore.API.Extensions
{
	public static class UserManagerExtension
	{
		public static async Task<AppUser?> FindUserWithAddressAsync(this UserManager<AppUser> userManager ,ClaimsPrincipal User )
		{
			try
			{
				var Email = User.FindFirstValue(ClaimTypes.Email);
				var user = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == Email);
				return user;

			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
	}
}
