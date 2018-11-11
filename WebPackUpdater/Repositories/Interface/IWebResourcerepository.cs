using System;
using Microsoft.Xrm.Sdk;
using WebPackUpdater.Enums;

namespace WebPackUpdater.Repositories.Interface
{
    /// <summary>
    /// Webresource repo
    /// </summary>
    public interface IWebResourceRepository
    {
        Entity RetrieveWebresource(string name);
        void Update(Guid id, string name, string filePath, WebResourceType type);
    }
}
