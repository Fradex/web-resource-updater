using System;
using Microsoft.AspNetCore.Mvc;
using WebPackUpdater.Enums;
using WebPackUpdater.Generators.Interface;
using WebPackUpdater.Model;
using WebPackUpdater.Repositories.Interface;

namespace WebPackUpdater.Controllers
{
	[Route("api/[controller]")]
	public class WebpackController : ControllerBase
	{
		private IScriptsGenerator ScriptsGenerator { get; set; }
		private IFileRepository FileRepository { get; set; }
		private IBuildRepository BuildRepository { get; set; }

		public WebpackController(IScriptsGenerator scriptsGenerator, IFileRepository fileRepositor,
			IBuildRepository buildRepository)
		{
			ScriptsGenerator = scriptsGenerator;
			FileRepository = fileRepositor;
			BuildRepository = buildRepository;
		}

		/// <summary>
		/// Выполнить построение
		/// </summary>
		/// <returns></returns>
		[HttpPost, Route("Build")]
		public ActionResult<BuildResult> Build([FromBody] BuildRequestModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			BuildResult buildResult;
			var build = BuildRepository.Create(model.Name, model.Description);

			try
			{
				buildResult = ScriptsGenerator.Build(build);
				FileRepository.AutoMapFiles(build);
			}
			catch
			{
				build.BuildStatusType = BuildStatusType.ExitWithErrors;
				BuildRepository.Update(build);
				throw;
			}

			return Ok(buildResult);
		}
	}
}