using Cake.Common.Tools.DotNet.NuGet.Push;

namespace Aero.Cake.Features.DotNet.Settings
{
    public static class NuGetPushSettings
    {
        public static DotNetNuGetPushSettings Default(string apiKey, string apiUrl)
        {
            return new DotNetNuGetPushSettings()
            {
                ApiKey = apiKey,
                Source = apiUrl
            };
        }
    }
}
