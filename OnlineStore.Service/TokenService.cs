using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.Core.IServices;
using OnlineStore.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace OnlineStore.Service
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _configuration;

		public TokenService(IConfiguration configuration)
        {
			_configuration = configuration;
		}
        public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
		{
			// PrivateClaims
			var AuthClaims = new List<Claim>()
			{
				new Claim(ClaimTypes.GivenName , user.DisplayName),
				new Claim(ClaimTypes.Email , user.Email),
				//new Claim(ClaimTypes.MobilePhone , user.PhoneNumber)
			};
			var UserRoles = await userManager.GetRolesAsync(user);
			foreach (var Role in UserRoles) { AuthClaims.Add(new Claim(ClaimTypes.Role, Role)); }

			var TokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

			var Token = new JwtSecurityToken(
				issuer: _configuration["JWT:ValidIssuer"],
				audience: _configuration["JWT:ValidAudience"],
				expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
				claims: AuthClaims,
				signingCredentials: new SigningCredentials(TokenKey , SecurityAlgorithms.HmacSha256Signature)
				);
			try
			{
				return new JwtSecurityTokenHandler().WriteToken(Token);
			}
			catch (Exception ex)
			{
				return $"Error generating token: {ex.Message}";
				
			}
		}
	}
}
