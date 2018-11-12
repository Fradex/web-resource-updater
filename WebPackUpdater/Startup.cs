using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using WebPackUpdater.Context;
using WebPackUpdater.Domain;
using WebPackUpdater.Domain.Interfaces;
using WebPackUpdater.Generators;
using WebPackUpdater.Generators.Interface;
using WebPackUpdater.Repositories;
using WebPackUpdater.Repositories.Interface;


namespace WebPackUpdater
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Register the Swagger generator, defining 1 or more Swagger documents
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
				var filePath = Path.Combine(System.AppContext.BaseDirectory, "Norbit.Crm.Soglasie.WebApi.xml");
				c.IncludeXmlComments(filePath);
			});

			services.AddMvc();

            services.AddDbContext<WebResourceContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DbContext")));

            services.AddSingleton(Configuration);

			services.AddMvcCore(options =>
				{
					options.ReturnHttpNotAcceptable = true;
				})
				.AddApiExplorer()
				.AddFormatterMappings()
				.AddJsonFormatters();

			services.AddTransient<IConnectionService, ConnectionService>();
			services.AddTransient<IScriptsGenerator, ScriptsGenerator>();
			services.AddSingleton<IFileRepository, FilesRepository>();
			services.AddSingleton<IWebResourceRepository, WebResourceRepository>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
				{
					HotModuleReplacement = true
				});
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");

				routes.MapSpaFallbackRoute(
					name: "spa-fallback",
					defaults: new { controller = "Home", action = "Index" });
			});

			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
			// specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });


		}
	}
}
