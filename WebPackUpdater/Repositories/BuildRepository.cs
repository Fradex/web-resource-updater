using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebPackUpdater.Context;
using WebPackUpdater.Model;
using WebPackUpdater.Repositories.Interface;

namespace WebPackUpdater.Repositories
{
	// todo Сделать темплейтовый класс для репозиториев
	public class BuildRepository : IBuildRepository
	{
		private WebResourceContext Context;
		private IServiceProvider ServiceProvider { get; set; }

		public BuildRepository(IServiceProvider serviceProvider, WebResourceContext context)
		{
			ServiceProvider = serviceProvider;
			Context = context;
		}

		public Build Create(string buildName, string description = null)
		{
			var build = new Build
				{BuildName = $"Билд {buildName} от {DateTime.Now:dd-MM-yyyy HH:mm:ss}", BuildDescription = description};
			Context.Builds.Add(build);

			Context.SaveChanges();

			return build;
		}

		public Build Update(Build reqBuild)
		{
			var build = Context.Builds.Select(x => x).FirstOrDefault(x => x.Id == reqBuild.Id);

			if (build != null)
			{
				Context.Entry(build).CurrentValues.SetValues(reqBuild);
				Context.Entry(build).State = EntityState.Modified;
				Context.SaveChanges();
			}

			return build;
		}

		public async Task<Build> GetAsync(Guid? id)
		{
			if (id == null || id.Equals(Guid.Empty))
			{
				throw new ArgumentException("Идентификатор не должен быть пустым.");
			}

			return await Context.Set<Build>().FirstOrDefaultAsync(x => x.Id == id);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<Build>> GetAllAsync()
		{
			return await Context.Builds.Select(x => x).OrderByDescending(x => x.CreatedOn).ToListAsync();
		}
	}
}