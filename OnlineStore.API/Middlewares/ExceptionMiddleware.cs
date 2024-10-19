using Azure;
using OnlineStore.API.Errors;
using System.Net;
using System.Text.Json;

namespace OnlineStore.API.Middlewares
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddleware> _logger;
		private readonly IHostEnvironment _env;

		public ExceptionMiddleware(RequestDelegate Next , ILogger<ExceptionMiddleware> Logger , IHostEnvironment Env)
		{
			_next = Next;
			_logger = Logger;
			_env = Env;
		}

		public async Task InvokeAsync(HttpContext context)  
		{
			try
			{
				await _next.Invoke(context);
			}
			catch (Exception ex) 
			{
				_logger.LogError(ex, ex.Message);
				context.Response.ContentType = "application/json";
				context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

				//if (_env.IsDevelopment())
				//{
				//	var Response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace);
				//}
				//else
				//{
				//	var Response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
				//}
				var Respnse = _env.IsDevelopment() ? new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString()) : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);


				// SerializeOpt To CamelCase For JS(Front-end)
				var options = new JsonSerializerOptions()
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase
				};
				// Convert an Obj to Json Representation as string in C#  
				var JsonStrResponse = JsonSerializer.Serialize(Respnse , options);

				await context.Response.WriteAsync(JsonStrResponse);
			}
		}
	}
}
