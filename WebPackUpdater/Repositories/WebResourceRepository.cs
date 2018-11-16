using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebPackUpdater.Context;
using WebPackUpdater.Model;
using WebPackUpdater.Repositories.Base;
using WebPackUpdater.Repositories.Interface;

namespace WebPackUpdater.Repositories
{
    public class WebResourceRepository : RepositoryBase<WebResourceContext, WebResourceMap>, IWebResourceRepository
    {
        private ICrmWebResourceRepository _crmWebResourceRepository;

        /// <inheritdoc />
        public WebResourceRepository(WebResourceContext context, ICrmWebResourceRepository crmWebResourceRepository) : base(context)
        {
            _crmWebResourceRepository = crmWebResourceRepository;
        }

        /// <summary>
        /// Обновить и опубликовать выбранные веб-ресурсы в CRM
        /// </summary>
        /// <param name="ids">Идентификаторы веб-ресурсов</param>
        public void UpdateAndPublish(Guid[] ids)
        {
            var webResources = Context.WebResourceMaps
                .Where(x => ids.Contains(x.Id) && x.CrmWebResourceId != null)
                .Select(entity => entity)
                .ToList();

            if (!webResources.Any())
            {
                return;
            }

            foreach (var webresource in webResources)
            {
                _crmWebResourceRepository.Update(webresource.CrmWebResourceId, webresource.CrmFileName,
                    webresource.FileSystemPath);
            }

            var crmIds = webResources.Select(x => x.CrmWebResourceId).ToArray();

            _crmWebResourceRepository.Publish(crmIds);
        }

        /// <summary>
        /// Получить измененные файлы
        /// </summary>
        /// <returns>Коллекция веб-ресурсов</returns>
        public async Task<IEnumerable<WebResourceMap>> GetChangedWebResourcesAsync()
        {
            return await Context.Set<ChangedWebResource>()
                .Select(x => x.WebResourceMap)
                .OrderByDescending(x => x.CrmFileName)
                .Distinct()
                .ToListAsync();
        }
    }
}