using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aero.Azure.Management.Authentication;
using Aero.Infrastructure;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.ResourceManager.Models;

namespace Aero.Azure.Management
{
    public interface IResourceManagerService : IManagementService
    {
        Task<ResourceGroup> CreateOrUpdateResourceGroupAsync(string name, string location, IDictionary<string, string> tags = null);

        Task<bool> DeleteResourceGroupIfExistsAsync(string name);

        Task<DeploymentExtended> DeployArmTemplateAsync(string resourceGroupName, string deploymentName, DeploymentProperties deploymentProperties);

        Task<ResourceGroup> GetResourceGroupAsync(string name);

        Task<ResourceGroup[]> GetResourceGroupsAsync();

        Task<DeploymentValidateResult> ValidateArmTemplateAsync(string resourceGroupName, string deploymentName, DeploymentProperties deploymentProperties);
    }

    public class ResourceManagerService : AbstractManagementService<IResourceManagementClient>, IResourceManagerService
    {
        public ResourceManagerService(IAeroLogger<ResourceManagerService> logger) : base(logger)
        {
        }

        public async Task<ResourceGroup> CreateOrUpdateResourceGroupAsync(string name, string location, IDictionary<string, string> tags = null)
        {
            var resourceGroup = await Client.ResourceGroups.CreateOrUpdateAsync(name, new ResourceGroup(location, null, null, null, null, tags));
            return resourceGroup;
        }

        public async Task<bool> DeleteResourceGroupIfExistsAsync(string name)
        {
            var resourceGroups = await GetResourceGroupsAsync();
            var resourceGroup = resourceGroups.SingleOrDefault(x => x.Name == name);

            if (resourceGroup == null)
            {
                return true;
            }

            await Client.ResourceGroups.DeleteAsync(name);
            return true;
        }

        public async Task<DeploymentExtended> DeployArmTemplateAsync(string resourceGroupName, string deploymentName, DeploymentProperties deploymentProperties)
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

        public async Task<DeploymentValidateResult> ValidateArmTemplateAsync(string resourceGroupName, string deploymentName, DeploymentProperties deploymentProperties)
        {
            var result = await Client.Deployments.ValidateAsync(resourceGroupName, deploymentName, new Deployment(deploymentProperties));
            return result;
        }
    }
}
