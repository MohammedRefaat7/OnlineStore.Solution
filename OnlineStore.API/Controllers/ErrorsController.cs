using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.API.Errors;

namespace OnlineStore.API.Controllers
{
	[Route("errors/{Code}")]
	[ApiController]
	[ApiExplorerSettings(IgnoreApi = true)]
	public class ErrorsController : ControllerBase
	{
		public ActionResult Error(int Code)
		{
			return NotFound(new ApiErrorResponse(Code));
		}
	}
}
