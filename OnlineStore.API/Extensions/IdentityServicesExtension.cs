using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using OnlineStore.Core.IServices;
using OnlineStore.Core.Models.Identity;
using OnlineStore.Repository.Identity;
using OnlineStore.Service;

namespace OnlineStore.API.Extensions
{
	public static class IdentityServicesExtension
	{
		public static IServiceCollection AddIdentityServices(this IServiceCollection Services) 
		{
			Services.AddScoped<ITokenService, TokenService>();

			Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				    .AddJwtBearer();      //(UserManager - SigninManager - RoleManager ) ...

			Services.AddIdentity<AppUser, IdentityRole>()                           // to Uses UserManager's Interfaces (in AppIdentityDbContext) ...
							.AddEntityFrameworkStores<AppIdentityDbContext>();

			return Services;
		}
	}
}
