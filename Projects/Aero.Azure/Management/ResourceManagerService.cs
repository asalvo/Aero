using System.Threading.Tasks;
using Aero.Azure.Management.Authentication;
using Aero.Infrastructure;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.ResourceManager.Models;

namespace Aero.Azure.Management
{
    public interface IResourceManagerService : IManagementService
    {
        Task<DeploymentExtended> DeployArmTemplate(string resourceGroupName, string deploymentName, DeploymentProperties deploymentProperties);

        Task<ResourceGroup> GetResourceGroupAsync(string name);

        Task<ResourceGroup[]> GetResourceGroupsAsync();

        Task<DeploymentValidateResult> ValidateArmTemplate(string resourceGroupName, string deploymentName, DeploymentProperties deploymentProperties);
    }

    public class ResourceManagerService : AbstractManagementService<IResourceManagementClient>, IResourceManagerService
    {
        public ResourceManagerService(IAeroLogger<ResourceManagerService> logger) : base(logger)
        {
        }

        public async Task<DeploymentExtended> DeployArmTemplate(string resourceGroupName, string deploymentName, DeploymentProperties deploymentProperties)
        {
            var result = await Client.Deployments.CreateOrUpdateAsync(resourceGroupName, deploymentName, new Deployment(deploymentProperties));
            return result;
        }

        public async Task<ResourceGroup> GetResourceGroupAsync(string name)
        {
            var result = await Client.ResourceGroups.GetAsync(name);
            return result;
        }

        public async Task<ResourceGroup[]> GetResourceGroupsAsync()
        {
            var resourceGroups = await GetPagedData(
                () => Client.ResourceGroups.ListAsync(), 
                nextPageLink => Client.ResourceGroups.ListNextAsync(nextPageLink));

            return resourceGroups;
        }

        public override void Initialize(AzureCredentials credentials)
        {
            Client = new ResourceManagementClient(credentials) {SubscriptionId = credentials.DefaultSubscriptionId};
        }

        public async Task<DeploymentValidateResult> ValidateArmTemplate(string resourceGroupName, string deploymentName, DeploymentProperties deploymentProperties)
        {
            var result = await Client.Deployments.ValidateAsync(resourceGroupName, deploymentName, new Deployment(deploymentProperties));
            return result;
        }
    }
}
