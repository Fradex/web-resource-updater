using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebPackUpdater.Model;
using WebPackUpdater.Repositories.Interface;

namespace WebPackUpdater.Controllers
{
	[Route("api/[controller]")]
	public class WebResourcesController
	{
		private IWebResourceRepository WebResourceRepository { get; set; }

		public WebResourcesController(IWebResourceRepository webResourceRepository)
		{
			WebResourceRepository = webResourceRepository;
		}

		[HttpGet]
		public Task<IEnumerable<WebResourceMap>> Get()
		{
			return WebResourceRepository.GetChangedWebResourcesAsync();
		}
	}
}