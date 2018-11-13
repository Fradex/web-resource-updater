using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebPackUpdater.Model;

namespace WebPackUpdater.Repositories.Interface
{
	public interface IBuildRepository
	{
		Build Create(string buildName, string description = null);
		Build Update(Build build);
	    Task<IEnumerable<Build>> GetAllAsync();
        Task<Build> GetAsync(Guid? id);
	}
}