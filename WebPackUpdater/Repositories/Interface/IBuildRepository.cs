using System.Collections.Generic;
using WebPackUpdater.Model;

namespace WebPackUpdater.Repositories.Interface
{
	public interface IBuildRepository
	{
		Build Create(string buildName, string description = null);
		Build Update(Build build);
		IEnumerable<Build> GetAll();
	}
}