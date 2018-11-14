using Microsoft.Extensions.DependencyInjection;
using WebPackUpdater.Domain;
using WebPackUpdater.Domain.Interfaces;
using WebPackUpdater.Generators;
using WebPackUpdater.Generators.Interface;
using WebPackUpdater.Repositories;
using WebPackUpdater.Repositories.Interface;

namespace WebPackUpdater
{
    public static class DiMiddleware
    {
	    public static void AddDiDependencies(this IServiceCollection services)
	    {
		    services.AddTransient<IConnectionService, ConnectionService>();
		    services.AddTransient<IScriptsGenerator, ScriptsGenerator>();
		    services.AddTransient<IFileRepository, FilesRepository>();
		    services.AddTransient<ICrmWebResourceRepository, CrmWebResourceRepository>();
		    services.AddTransient<IBuildRepository, BuildRepository>();
		    services.AddTransient<IWebResourceRepository, WebResourceRepository>();
		}
    }
}
