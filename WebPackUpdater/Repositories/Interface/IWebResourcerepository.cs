using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebPackUpdater.Model;

namespace WebPackUpdater.Repositories.Interface
{
	public interface IWebResourceRepository
	{
		Task<IEnumerable<WebResourceMap>> GetChangedWebResourcesAsync();

	    /// <summary>
	    /// Обновить и опубликовать выбранные веб-ресурсы в CRM
	    /// </summary>
	    /// <param name="ids">Идентификаторы веб-ресурсов</param>
	    void UpdateAndPublish(Guid[] ids);
	}
}