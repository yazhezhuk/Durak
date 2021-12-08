using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Durak.Client.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class GameController : ControllerBase
	{
		[HttpPost("create/")]
		public IActionResult Create()
		{


		}
	}
}
