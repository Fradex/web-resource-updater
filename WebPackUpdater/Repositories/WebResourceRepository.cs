using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebPackUpdater.Context;
using WebPackUpdater.Model;
using WebPackUpdater.Repositories.Base;
using WebPackUpdater.Repositories.Interface;

namespace WebPackUpdater.Repositories
{
	public class WebResourceRepository : RepositoryBase<WebResourceContext, WebResourceMap>, IWebResourceRepository
	{
		/// <inheritdoc />
		public WebResourceRepository(WebResourceContext context) : base(context)
		{
		}

		/// <summary>
		/// Получить измененные файлы
		/// </summary>
		/// <returns>Коллекция веб-ресурсов</returns>
		public async Task<IEnumerable<WebResourceMap>> GetChangedWebResourcesAsync()
		{
			return await Context.Set<ChangedWebResource>()
				.Select(x => x.WebResourceMap)
				.OrderByDescending(x => x.CrmFileName)
				.Distinct()
				.ToListAsync();
		}
	}
}