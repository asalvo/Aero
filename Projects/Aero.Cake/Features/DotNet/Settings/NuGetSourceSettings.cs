using Cake.Common.Tools.DotNetCore.NuGet.Source;

namespace Aero.Cake.Features.DotNet.Settings
{
    public static class NuGetSourceSettings
    {
        public static DotNetCoreNuGetSourceSettings Default(string apiKey, string apiUrl, string apiUsername = "UsingPAT")
        {
            return new DotNetCoreNuGetSourceSettings()
            {
                Password = apiKey,
                Source = apiUrl,
                UserName = apiUsername
            };
        }
    }
}
