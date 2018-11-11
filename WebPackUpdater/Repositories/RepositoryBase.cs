using Microsoft.Xrm.Sdk;
using WebPackUpdater.Domain.Interfaces;
using WebPackUpdater.Enums;

namespace WebPackUpdater.Repositories
{
    public class RepositoryBase
    {
        protected IOrganizationService OrganizationService { get; private set; }

        public RepositoryBase(IConnectionService connectionService)
        {
            OrganizationService = connectionService.GetService(ConnectionType.Administrator);
        }
    }
}
