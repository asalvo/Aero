using Cake.Common.Tools.DotNetCore.NuGet.Push;

namespace Aero.Cake.Features.DotNet.Settings
{
    public static class NuGetPushSettings
    {
        public static DotNetCoreNuGetPushSettings Default(string apiKey, string apiUrl)
        {
            return new DotNetCoreNuGetPushSettings()
            {
                ApiKey = apiKey,
                Source = apiUrl
            };
        }
    }
}
