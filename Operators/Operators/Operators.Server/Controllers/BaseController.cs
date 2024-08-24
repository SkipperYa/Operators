using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;

namespace Operators.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	[RequestTimeout("DefaultTimeout10s")]
	public class BaseController : ControllerBase
	{

	}
}
