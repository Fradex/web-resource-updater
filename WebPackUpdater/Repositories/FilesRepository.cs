using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebPackUpdater.Extensions;
using WebPackUpdater.Helpers;
using WebPackUpdater.Model;
using WebPackUpdater.Repositories.Interface;

namespace WebPackUpdater.Repositories
{
    public class FilesRepository : IFileRepository
    {
        private IConfiguration Configuration { get; set; }

        private IWebResourceRepository WebResourceRepository { get; set; }

        private string ScriptsPath { get; set; }

        public FilesRepository(IConfiguration configuration, IWebResourceRepository webResourceRepository)
        {
            Configuration = configuration;
            WebResourceRepository = webResourceRepository;

            ScriptsPath = Configuration.GetSection("AppSettings")["WebPackFolder"];
            if (string.IsNullOrEmpty(ScriptsPath))
            {
                throw new ArgumentException("Не задан путь до директории  со скриптами.");
            }
        }

        public void AutoMapFiles()
        {
            var buildDirectory = Path.Combine(ScriptsPath, Configuration.GetSection("AppSettings")["BuildDirectory"]);
            var fileNames = Directory.GetFiles(buildDirectory, "*.js", SearchOption.AllDirectories);
            var webResourcesList = new List<WebResourceMap>();

            foreach (var fileName in fileNames)
            {
                var index = fileName.IndexOf("new_", StringComparison.Ordinal);
                var crmFileName = fileName.Substring(index, fileName.Length - index).Replace("\\", "/").Replace(".bundle", "");

                var webResource = new WebResourceMap
                {
                    CrmFileName = crmFileName,
                    ChangedOn = DateTime.Now,
                    LocalFileMd5Hash = CryptographyHelper.GetMd5Hash(fileName),
                    FileSystemPath = fileName,
                };
                var crmWebResource = WebResourceRepository.RetrieveWebresource(crmFileName);

                if (crmWebResource != null)
                {
                    webResource.CrmWebResourceId = crmWebResource.Id;
                    byte[] binary = Encoding.ASCII.GetBytes(crmWebResource.GetPlainText());
                    webResource.CrmFileMd5Hash = CryptographyHelper.GetMd5Hash(binary);
                }
            }
        }
    }
}
