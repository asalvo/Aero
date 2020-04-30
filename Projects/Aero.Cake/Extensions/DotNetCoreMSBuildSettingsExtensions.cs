using Cake.Common.Tools.DotNetCore.MSBuild;

namespace Aero.Cake.Extensions
{
    public static class DotNetCoreMSBuildSettingsExtensions
    {
        /// <summary>
        /// Sets all three version properties, Version, AssemblyVersion and FileVersion.
        /// </summary>
        public static DotNetCoreMSBuildSettings SetAllVersions(this DotNetCoreMSBuildSettings settings, string version)
        {
            if(settings == null)
            {
                settings = new DotNetCoreMSBuildSettings();
            }

            version = version.ParseVersion();

            settings
                .WithProperty("Version", version)
                .WithProperty("AssemblyVersion", version)
                .WithProperty("FileVersion", version);

            return settings;
        }
    }
}
