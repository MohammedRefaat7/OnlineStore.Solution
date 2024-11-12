using System.Text.RegularExpressions;

namespace OnlineStore.API.Helpers
{
	public static class ValidationHelper
	{
		public static bool AnyNullOrEmpty(params string[] values)
		{
			return values.Any(string.IsNullOrWhiteSpace);
		}

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
