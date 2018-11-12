using Microsoft.AspNetCore.Mvc;
using WebPackUpdater.Enums;
using WebPackUpdater.Generators.Interface;
using WebPackUpdater.Repositories.Interface;

namespace WebPackUpdater.Controllers
{
	[Route("api/[controller]")]
	public class WebpackController : ControllerBase
	{
		private IScriptsGenerator ScriptsGenerator { get; set; }

	    private IFileRepository FileRepository { get; set; }

	    public WebpackController(IScriptsGenerator scriptsGenerator, IFileRepository fileRepositor)
		{
			ScriptsGenerator = scriptsGenerator;
		    FileRepository = fileRepositor;
		}

		/// <summary>
		/// Выполнить построение
		/// </summary>
		/// <returns></returns>
		[HttpPost, Route("Build")]
		public ActionResult<BuildResult> Build()
		{
		    var buildResult = ScriptsGenerator.Build();
            FileRepository.AutoMapFiles();

            return Ok(buildResult);
		}
	}
}