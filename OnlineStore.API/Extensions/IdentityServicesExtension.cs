using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.Core.IServices;
using OnlineStore.Core.Models.Identity;
using OnlineStore.Repository.Identity;
using OnlineStore.Service;
using System.Text;

namespace OnlineStore.API.Extensions
{
	public static class IdentityServicesExtension
	{
		public static IServiceCollection AddIdentityServices(this IServiceCollection Services , IConfiguration configuration) 
		{
			Services.AddScoped<ITokenService, TokenService>();

			Services.AddAuthentication(Options =>              //(UserManager - SigninManager - RoleManager ) ...
			{
				Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
					.AddJwtBearer(optoins =>
					{
						optoins.TokenValidationParameters = new TokenValidationParameters()
						{
							ValidateIssuer = true,
							ValidIssuer = configuration["JWT:ValidIssuer"],
							ValidateAudience = true,
							ValidAudience = configuration["JWT:ValidAudience"],
							ValidateLifetime = true,
							ValidateIssuerSigningKey = true,
							IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
						};
					});      

			Services.AddIdentity<AppUser, IdentityRole>()                           // to Uses UserManager's Interfaces (in AppIdentityDbContext) ...
							.AddEntityFrameworkStores<AppIdentityDbContext>();

			return Services;
		}
	}
}
