namespace OnlineStore.API.Errors
{
	public class ApiExceptionResponse : ApiErrorResponse
	{
        public string? Details { get; set; }

        public ApiExceptionResponse(int SCode , string? Msg = null , string? details = null):base(SCode , Msg)
        {
            Details = details;
        }
    }
}
