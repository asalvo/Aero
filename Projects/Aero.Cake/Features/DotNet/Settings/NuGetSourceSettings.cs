using Cake.Common.Tools.DotNet.NuGet.Source;

namespace Aero.Cake.Features.DotNet.Settings
{
    public static class NuGetSourceSettings
    {
        public static DotNetNuGetSourceSettings Default(string apiKey, string apiUrl, string apiUsername = "UsingPAT")
        {
            return new DotNetNuGetSourceSettings()
            {
                Password = apiKey,
                Source = apiUrl,
                UserName = apiUsername
            };
        }
    }
}
