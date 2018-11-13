using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddDbContext<WebResourceContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DbContext")));


            services.AddMvcCore(options =>
                {
                    options.ReturnHttpNotAcceptable = true;
                })
                .AddApiExplorer()
                .AddFormatterMappings()
                .AddJsonFormatters();


            services.AddSingleton(Configuration);
            services.AddTransient<IConnectionService, ConnectionService>();
            services.AddTransient<IScriptsGenerator, ScriptsGenerator>();
            services.AddTransient<IFileRepository, FilesRepository>();
            services.AddTransient<IWebResourceRepository, WebResourceRepository>();
            services.AddTransient<IBuildRepository, BuildRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
           {
               routes.MapRoute(
                   name: "default",
                   template: "{controller}/{action=Index}/{id?}");
           });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
            DbInitializer.Seed(serviceProvider);
        }
    }
}
