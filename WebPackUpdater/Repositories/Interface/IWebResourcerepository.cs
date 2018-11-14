using System.Collections.Generic;
using System.Threading.Tasks;
using WebPackUpdater.Model;

namespace WebPackUpdater.Repositories.Interface
{
	public interface IWebResourceRepository
	{
		Task<IEnumerable<WebResourceMap>> GetChangedWebResourcesAsync();
	}
}