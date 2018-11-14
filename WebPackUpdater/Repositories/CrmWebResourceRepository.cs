using System;
using System.IO;
using System.Linq;
using System.Text;
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
    public class CrmWebResourceRepository : CrmRepositoryBase, ICrmWebResourceRepository
    {
        public CrmWebResourceRepository(IConnectionService connectionService) : base(connectionService)
        {
        }

        public void Update(Guid id, string name, string filePath, WebResourceType type = WebResourceType.Auto)
        {
            var record = CreateRecord(name, filePath, type);
            record.Id = id;
            OrganizationService.Update(record);
        }

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

            if (filePath != null)
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
