using Microsoft.AspNetCore.Identity;
using OnlineStore.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.IServices
{
	public interface ITokenService
	{
		Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager);
	}
}
