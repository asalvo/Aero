using Aero.Azure.Management.Authentication;
using Aero.Infrastructure;

namespace Aero.Azure.Management
{
    public interface IAzureManagementService : IManagementService
    {
        IResourceManagerService ResourceManager { get; }
        IWebsiteService Website { get; }
    }

    public class AzureManagementService : AbstractManagementService<AzureManagementService>, IAzureManagementService
    {
        public AzureManagementService(IAeroLogger<AzureManagementService> logger, IResourceManagerService resourceManagerService, IWebsiteService websiteService):base(logger)
        {
            ResourceManager = resourceManagerService;
            Website = websiteService;
        }

        public IResourceManagerService ResourceManager { get; }

        public IWebsiteService Website { get; }

        public override void Initialize(AzureCredentials credentials)
        {
            ResourceManager.Initialize(credentials);
            Website.Initialize(credentials);
        }
    }
}
