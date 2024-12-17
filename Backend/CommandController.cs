using Microsoft.AspNetCore.Mvc;
namespace SMOOWebController.Backend
{
	public class CommandRequest
	{
		public string api { get; set; }
		public string command { get; set; }
		public override string ToString()
		{
			return $"{api} - {command}";
		}
	}

	[ApiController]
[Route("api")]
	public class CommandController : ControllerBase
	{
		private Logger logger = new Logger() { name = "Commandcontroller" };
		[HttpPost("sendcommand")]
		public IActionResult SendCommand([FromBody] CommandRequest request)
		{
			logger.Log(request.ToString());
			if (string.IsNullOrEmpty(request?.api))
			{
				logger.Error("API key is missing");
				return Unauthorized("API key is missing");
			}
			JsonAPI jsonAPI = new(request.api);
			string result = jsonAPI.SendCommand(request.command);
			logger.Log($"Command executed: {request.command}");
			return Ok(result);
		}
		[HttpGet("testroute")]
		public IActionResult TestRoute()
		{
			return Ok("Route works");
		}
	}
}
