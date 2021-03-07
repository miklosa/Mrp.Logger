using Microsoft.AspNetCore.Mvc;
using Mrp.Logger;

namespace WebApplication1.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class LoggerController : Controller
	{
		[HttpGet]
		public ActionResult Get()
		{
			return Ok("Running....");
		}

		[HttpPost]
		public ActionResult Log(LoggerMessage message)
		{
			// Do nothing
			return new EmptyResult();
		}
	}
}