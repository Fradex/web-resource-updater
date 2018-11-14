using Microsoft.Xrm.Sdk;
using WebPackUpdater.Domain.Interfaces;
using WebPackUpdater.Enums;

namespace WebPackUpdater.Repositories.Base
{
    public class CrmRepositoryBase
    {
        protected IOrganizationService OrganizationService { get; private set; }

        public CrmRepositoryBase(IConnectionService connectionService)
        {
            OrganizationService = connectionService.GetService(ConnectionType.Administrator);
        }
    }
}
