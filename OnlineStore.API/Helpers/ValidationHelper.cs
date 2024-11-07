namespace OnlineStore.API.Helpers
{
	public static class ValidationHelper
	{
		public static bool AnyNullOrEmpty(params string[] values)
		{
			return values.Any(string.IsNullOrWhiteSpace);
		}
	}

}
