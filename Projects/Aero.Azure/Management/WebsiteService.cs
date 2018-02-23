using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aero.Azure.Management.Authentication;
using Aero.Azure.Management.Models;
using Aero.Infrastructure;
using Microsoft.Azure.Management.WebSites;
using Microsoft.Azure.Management.WebSites.Models;

namespace Aero.Azure.Management
{
    public interface IWebsiteService : IManagementService
    {
        Task<StringDictionary> ListApplicationSettingsAsync(string resourceGroup, string name);

        Task<Site[]> ListByResourceGroupAsync(string resourceGroupName);

        Task<ConnectionStringDictionary> ListConnectionStringsAsync(string resourceGroup, string name);

        Task<WebsiteSettings[]> ListSettingsForSubscriptionAsync(string subscriptionId);
    }

    public class WebsiteService : AbstractManagementService<IWebSiteManagementClient>, IWebsiteService
    {
        public WebsiteService(IAeroLogger<WebsiteService> logger):base(logger)
        {
            
        }

        public async Task<StringDictionary> ListApplicationSettingsAsync(string resourceGroup, string name)
        {
            var settings = await Client.WebApps.ListApplicationSettingsAsync(resourceGroup, name);
            return settings;
        }

        public async Task<ConnectionStringDictionary> ListConnectionStringsAsync(string resourceGroup, string name)
        {
            var settings = await Client.WebApps.ListConnectionStringsAsync(resourceGroup, name);
            return settings;
        }

        public async Task<WebsiteSettings[]> ListSettingsForSubscriptionAsync(string subscriptionId)
        {
            var webSiteSettings = new List<WebsiteSettings>();

            var sites = await GetPagedData(
                () => Client.WebApps.ListAsync(),
                (nextPageLink) => Client.WebApps.ListNextAsync(nextPageLink));

            foreach (var site in sites)
            {
                var settings = new WebsiteSettings
                {
                    Name = site.Name,
                    ResourceGroupName = site.Name
                };

                var applicationSettings = await ListApplicationSettingsAsync(site.ResourceGroup, site.Name);
                settings.ApplicationSettings = applicationSettings.Properties.ToDictionary(x => x.Key, x => x.Value);
                
                var connectionStrings = await ListConnectionStringsAsync(site.ResourceGroup, site.Name);
                settings.ConnectionStrings = connectionStrings.Properties.ToDictionary(x => x.Key, x => new ConnStringInfo(x.Key, x.Value.Value, x.Value.Type));

                webSiteSettings.Add(settings);
            }
              
            return webSiteSettings.ToArray();
        }

        public override void Initialize(AzureCredentials credentials)
        {
            Client = new WebSiteManagementClient(credentials) {SubscriptionId = credentials.DefaultSubscriptionId};
        }

        public async Task<Site[]> ListByResourceGroupAsync(string resourceGroupName)
        {
            var result = await GetPagedData(
                () => Client.WebApps.ListByResourceGroupAsync(resourceGroupName),
                (nextPageLink) => Client.WebApps.ListByResourceGroupNextAsync(nextPageLink));

            return result.ToArray();
        }
    }
}
