using Aero.Cake.Features.DotNet.Services;
using Cake.Common.Tools.DotNetCore.MSBuild;

namespace Aero.Cake.Features.DotNet.Settings
{
    public static class MsBuildSettings
    {
        /// <summary>
        /// Sets the following properties: Additional Properties (Version, AssemblyVersion, FileVersion)
        /// </summary>
        public static DotNetCoreMSBuildSettings Default(VersionModel versionModel)
        {
            return new DotNetCoreMSBuildSettings()
                .WithProperty("Version", versionModel.Version)
                .WithProperty("AssemblyVersion", versionModel.AssemblyVersion.ToString())
                .WithProperty("FileVersion", versionModel.FileVersion.ToString());
        }
    }
}
