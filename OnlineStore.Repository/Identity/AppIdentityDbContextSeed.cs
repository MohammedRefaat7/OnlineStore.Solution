using Microsoft.AspNetCore.Identity;
using OnlineStore.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Repository.Identity
{
	public static class AppIdentityDbContextSeed
	{
		public static async Task SeedUserAsync(UserManager<AppUser> userManager)
		{
			if (!userManager.Users.Any())
			{
				var user = new AppUser()
				{
					DisplayName = "Mohamed Refaat",
					Email = "MohamedRefaat@gmail.com",
					UserName = "Medo_M.Refaat",
					PhoneNumber = "010234567890"
				};
				await userManager.CreateAsync(user, "Pa$$w0rd");
			}
		}
	}
}
