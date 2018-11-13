using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebPackUpdater.Context;
using WebPackUpdater.Model;
using WebPackUpdater.Repositories.Interface;

namespace WebPackUpdater.Repositories
{
	public class BuildRepository : IBuildRepository
	{
		private IServiceProvider ServiceProvider { get; set; }

		public BuildRepository(IServiceProvider serviceProvider)
		{
			ServiceProvider = serviceProvider;
		}

		public Build Create(string buildName, string description = null)
		{
			var context = ServiceProvider.GetService<WebResourceContext>();
			var build = new Build
				{BuildName = $"Билд {buildName} от {DateTime.Now:dd-MM-yyyy HH:mm:ss}", BuildDescription = description};
			context.Builds.Add(build);

			context.SaveChanges();

			return build;
		}

		public Build Update(Build reqBuild)
		{
			var context = ServiceProvider.GetService<WebResourceContext>();
			var build = context.Builds.Select(x => x).FirstOrDefault(x => x.Id == reqBuild.Id);

			if (build != null)
			{
				context.Entry(build).CurrentValues.SetValues(reqBuild);
				context.Entry(build).State = EntityState.Modified;
				context.SaveChanges();
			}

			return build;
		}

		public IEnumerable<Build> GetAll()
		{
			var context = ServiceProvider.GetService<WebResourceContext>();
			return context.Builds.Select(x => x).OrderByDescending(x => x.CreatedOn).ToList();
		}
	}
}