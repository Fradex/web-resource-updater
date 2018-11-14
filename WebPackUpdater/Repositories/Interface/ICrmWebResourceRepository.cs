using System;
using Microsoft.Xrm.Sdk;
using WebPackUpdater.Enums;

namespace WebPackUpdater.Repositories.Interface
{
    /// <summary>
    /// Webresource repo - work only with CRM connection
    /// </summary>
    public interface ICrmWebResourceRepository
    {
        Entity RetrieveWebresource(string name);
        void Update(Guid id, string name, string filePath, WebResourceType type);
	    Entity CreateOrUpdate(string name, string filePath, WebResourceType type = WebResourceType.Auto);
    }
}
