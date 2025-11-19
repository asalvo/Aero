using Aero.Cake.Features.DotNet.Services;
using Cake.Common.Tools.DotNet.MSBuild;

namespace Aero.Cake.Features.DotNet.Settings
{
    public static class MsBuildSettings
    {
        /// <summary>
        /// Sets the following properties: Additional Properties (Version, AssemblyVersion, FileVersion)
        /// </summary>
        public static DotNetMSBuildSettings Default(VersionModel versionModel)
        {
            return new DotNetMSBuildSettings()
                .WithProperty("Version", versionModel.Version)
                .WithProperty("AssemblyVersion", versionModel.AssemblyVersion.ToString())
                .WithProperty("FileVersion", versionModel.FileVersion.ToString());
        }
    }
}
