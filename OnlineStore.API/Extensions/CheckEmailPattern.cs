using OnlineStore.API.Errors;
using System.Text.RegularExpressions;

namespace OnlineStore.API.Extensions
{
	public static class CheckEmailPattern
	{
		public static bool IsEmailPattern(this string Email)
		{
			string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

			// Check if the email matches the pattern
			if (!Regex.IsMatch(Email, emailPattern))
			{
				// Return false if the email structure is invalid
				return false;
			}
			return true;
		}
	}
}
