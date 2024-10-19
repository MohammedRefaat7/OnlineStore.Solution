
namespace OnlineStore.API.Errors
{
	public class ApiErrorResponse
	{
		public int StatusCode { get; set; }
		public string? ErrorMsg { get; set; }

		public ApiErrorResponse(int statusCode , string? EMsg = null ) 
		{
			StatusCode = statusCode;
			ErrorMsg = EMsg?? GetDefaultMsgForStatusCode(statusCode);
		}

		private string? GetDefaultMsgForStatusCode(int statusCode)
		{
			return statusCode switch
			{
				400 => "Bad-Request :(",
				401 => "You are not Authorized",
				404 => "Resource Not Found !!",
				500 => "Sorry, Internal Server Error",
				_ => null
			};
		}
	}
}
