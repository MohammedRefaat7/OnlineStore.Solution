using Microsoft.AspNetCore.Identity;
using OnlineStore.Core.Models.Identity;
using OnlineStore.Repository.Identity;

namespace OnlineStore.API.Extensions
{
	public static class IdentityServicesExtension
	{
		public static IServiceCollection AddIdentityServices(this IServiceCollection Services) 
		{

			Services.AddAuthentication();      //(UserManager - SigninManager - RoleManager ) ...

			Services.AddIdentity<AppUser, IdentityRole>()                           // to Uses UserManager's Interfaces (in AppIdentityDbContext) ...
							.AddEntityFrameworkStores<AppIdentityDbContext>();

			return Services;
		}
	}
}
