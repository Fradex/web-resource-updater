using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Обновить и опубликовать веб-ресурсы в CRM
        /// </summary>
        /// <param name="ids">Идентифкаторы веб-ресурсов</param>
        /// <returns>Результат выполнения операции</returns>
        [Route("UpdateWebresources")]
        [HttpGet]
        public IActionResult UpdateWebresources(Guid[] ids)
        {
            if (ids?.Any() == false)
            {
                return new BadRequestObjectResult("Не переданы идентификаторы веб-ресурсов.");
            }

            WebResourceRepository.UpdateAndPublish(ids);
            return new OkResult();
        }
    }
}