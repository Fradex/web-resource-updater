using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebPackUpdater.Context;
using WebPackUpdater.Extensions;
using WebPackUpdater.Helpers;
using WebPackUpdater.Model;
using WebPackUpdater.Repositories.Interface;

namespace WebPackUpdater.Repositories
{
	public class FilesRepository : IFileRepository
	{
		private IConfiguration Configuration { get; set; }
		private IServiceProvider ServiceProvider { get; set; }

		private IWebResourceRepository WebResourceRepository { get; set; }

		private string ScriptsPath { get; set; }

		public FilesRepository(IConfiguration configuration,
			IWebResourceRepository webResourceRepository, IServiceProvider serviceProvider)
		{
			Configuration = configuration;
			WebResourceRepository = webResourceRepository;
			ServiceProvider = serviceProvider;

			ScriptsPath = Configuration.GetSection("AppSettings")["WebPackFolder"];
			if (string.IsNullOrEmpty(ScriptsPath))
			{
				throw new ArgumentException("Не задан путь до директории  со скриптами.");
			}
		}

		public void AutoMapFiles()
		{
			var buildDirectory = Path.Combine(ScriptsPath, Configuration.GetSection("AppSettings")["BuildDirectory"]);

			if (!Directory.Exists(buildDirectory))
			{
				throw new DirectoryNotFoundException("Не найдена директория со скриптами!.");
			}

			var fileNames = Directory.GetFiles(buildDirectory, "*.js", SearchOption.AllDirectories);

			using (var context = ServiceProvider.GetService<WebResourceContext>())
			{
				var savedWebResources = context.WebResourceMaps.Select(x => x).ToList();

				foreach (var fileName in fileNames)
				{
					var webResourceHash = CryptographyHelper.GetMd5Hash(fileName);

					var index = fileName.IndexOf("new_", StringComparison.Ordinal);
					var crmFileName = fileName.Substring(index, fileName.Length - index).Replace("\\", "/")
						.Replace(".bundle", "");

					var savedWebResource = savedWebResources.FirstOrDefault(x => x.CrmFileName == crmFileName);

					if (savedWebResource?.LocalFileMd5Hash == webResourceHash)
					{
						continue;
					}

					savedWebResource = savedWebResource ?? new WebResourceMap();

					savedWebResource.IsAutoUpdate = crmFileName.Contains("/ribbon") || crmFileName.Contains("/form");
					savedWebResource.CrmFileName = crmFileName;
					savedWebResource.ChangedOn = DateTime.Now;
					savedWebResource.LocalFileMd5Hash = webResourceHash;
					savedWebResource.FileSystemPath = fileName;

					var crmWebResource = WebResourceRepository.RetrieveWebresource(crmFileName);
					if (crmWebResource != null)
					{
						savedWebResource.CrmWebResourceId = crmWebResource.Id;
					}

					if (savedWebResource.Id == Guid.Empty)
					{
						context.WebResourceMaps.Add(savedWebResource);
					}
					else
					{
						context.Entry(savedWebResource).State = EntityState.Modified;
					}

					context.SaveChanges();
				}
			}
		}
	}
}