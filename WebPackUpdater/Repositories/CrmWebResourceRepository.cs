using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using WebPackUpdater.Domain.Interfaces;
using WebPackUpdater.Enums;
using WebPackUpdater.Mappers;
using WebPackUpdater.Repositories.Base;
using WebPackUpdater.Repositories.Interface;

namespace WebPackUpdater.Repositories
{
    /// <summary>
    /// Репозиторий веб-ресурсов CRM
    /// </summary>
    public class CrmWebResourceRepository : CrmRepositoryBase, ICrmWebResourceRepository
    {
        public CrmWebResourceRepository(IConnectionService connectionService) : base(connectionService)
        {
        }

        /// <summary>
        /// Обновить запись
        /// </summary>
        /// <param name="id">Идентификатор записи в CRM</param>
        /// <param name="name">Наименование веб-ресурса</param>
        /// <param name="filePath">Путь до файла</param>
        /// <param name="type">Тип веб-ресурса</param>
        public void Update(Guid id, string name, string filePath, WebResourceType type = WebResourceType.Auto)
        {
            var record = CreateRecord(name, filePath, type);
            record.Id = id;
            OrganizationService.Update(record);
        }

        /// <summary>
        /// Опубликовать веб-ресурсы
        /// </summary>
        /// <param name="webresourceIds">Идентификаторы веб-ресурсов</param>
        public void Publish(Guid[] webresourceIds)
        {
            if (webresourceIds?.Any() == false)
            {
                return;
            }

            var request = new OrganizationRequest
            {
                RequestName = "PublishXml",
                Parameters = new ParameterCollection
                {
                    new KeyValuePair<string, object>("ParameterXml",
                        $"<importexportxml><webresources>{string.Join("", webresourceIds.Select(id => $"<webresource>{id}</webresource>"))}</webresources></importexportxml>")
                }
            };
            OrganizationService.Execute(request);
        }

        /// <summary>
        /// Обновить или создать запись
        /// </summary>
        /// <param name="name">Наименование веб-ресурса</param>
        /// <param name="filePath">Путь до файла</param>
        /// <param name="type">Тип веб-ресурса</param>
        public Entity CreateOrUpdate(string name, string filePath, WebResourceType type = WebResourceType.Auto)
        {
            var idRecord = RetrieveWebresource(name);
            var record = CreateRecord(name, filePath, type);

            if (idRecord?.Id != null)
            {
                record.Id = idRecord.Id;
            }

            var upsertReq = new UpsertRequest
            {
                Target = record
            };

            var ret = OrganizationService.Execute(upsertReq) as UpsertResponse;
            record.Id = ret?.Target.Id ?? Guid.Empty;
            return record;
        }

        /// <summary>
        /// Создать запись
        /// </summary>
        /// <param name="name">Наименование веб-ресурса</param>
        /// <param name="filePath">Путь до файла</param>
        /// <param name="type">Тип веб-ресурса</param>
        public void Create(string name, string filePath, WebResourceType type = WebResourceType.Auto)
        {
            var record = CreateRecord(name, filePath, type);
            record.Id = OrganizationService.Create(record);
        }

        private Entity CreateRecord(string name, string filePath, WebResourceType type)
        {
            Map extMap = null;

            if (type == WebResourceType.Auto)
            {
                var extension = Path.GetExtension(filePath);

                extMap = WebresourceMapper.Instance.Items.FirstOrDefault(i => i.Extension == extension?.Remove(0, 1).ToLower());
                if (extMap != null)
                {
                    type = extMap.Type;
                }
            }

            var record = new Entity("webresource")
            {
                ["name"] = name,
                ["webresourcetype"] = new OptionSetValue((int)type),
            };

            if (filePath != null && File.Exists(filePath))
            {
                record["content"] = Convert.ToBase64String(File.ReadAllBytes(filePath));
            }

            var map = extMap ?? WebresourceMapper.Instance.Items.FirstOrDefault(i => i.CrmValue == (int)type);
            if (map != null)
            {
                record.FormattedValues["webresourcetype"] = map.Label;
            }
            return record;
        }

        /// <inheritdoc />
        public Entity RetrieveWebresource(string name)
        {
            try
            {
                var qba = new QueryByAttribute("webresource");
                qba.Attributes.Add("name");
                qba.Values.Add(name);
                qba.ColumnSet = new ColumnSet(true);

                EntityCollection collection = OrganizationService.RetrieveMultiple(qba);

                if (collection.Entities.Count > 1)
                {
                    throw new Exception($"there are more than one web resource with name '{name}'");
                }

                return collection.Entities.FirstOrDefault();
            }
            catch (Exception error)
            {
                throw new Exception($"An error occured while retrieving a webresource with name {name}: {error.Message}");
            }
        }
    }
}
