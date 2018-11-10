using Microsoft.AspNetCore.Mvc;
using WebPackUpdater.Enums;
using WebPackUpdater.Generators.Interface;

namespace WebPackUpdater.Controllers
{
	[Route("api/[controller]")]
	public class WebpackController : ControllerBase
	{
		private IScriptsGenerator ScriptsGenerator { get; set; }

		public WebpackController(IScriptsGenerator scriptsGenerator)
		{
			ScriptsGenerator = scriptsGenerator;
		}

		/// <summary>
		/// Выполнить построение
		/// </summary>
		/// <returns></returns>
		[HttpPost, Route("Build")]
		public ActionResult<BuildResult> Build()
		{
			return Ok(ScriptsGenerator.Build());
		}
	}
}