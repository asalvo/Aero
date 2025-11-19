using Aero.Cake.Features.DotNet.Services;
using Cake.Common.Tools.DotNet.Build;

namespace Aero.Cake.Features.DotNet.Settings
{
    public class BuildSettings
    {
        /// <summary>
        /// Sets the following properties: Configuration, MsBuildSettings (Uses MsBuildSettings.Default),
        ///   Additional Properties (Version, Copyright)
        /// </summary>
        public static DotNetBuildSettings Default(VersionModel versionModel, string configuration = "Release")
        {
            var msBuildSettings = MsBuildSettings.Default(versionModel);

            return new DotNetBuildSettings
            {
                Configuration = configuration,
                MSBuildSettings = msBuildSettings,
            };
        }
    }
}
